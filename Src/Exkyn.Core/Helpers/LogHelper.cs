namespace Exkyn.Core.Helpers;

public static class LogHelper
{
    #region Métodos Privados

    private static void WriteLogFile(string directory, string file, List<string> messages)
    {
        directory = DirectoryHelper.AddSlashEnd(directory);

        using (var f = new StreamWriter(directory + file, true))
        {
            foreach (string item in messages)
            {
                if (!string.IsNullOrEmpty(item))
                    f.WriteLine(item);
            }

            f.WriteLine("\n\n");
        }
    }

    private static List<string> CreateMessageList(Exception exception)
    {
        var messages = new List<string>
        {
            $"{DateTime.Now} - {exception.Message}"
        };

        if (!string.IsNullOrEmpty(exception.Source))
            messages.Add($"Source: {exception.Source}");

        if (!string.IsNullOrEmpty(exception.HelpLink))
            messages.Add($"HelpLink: {exception.HelpLink}");

        if (exception.InnerException != null && !string.IsNullOrEmpty(exception.InnerException.Message))
        {
            messages.Add($"InnerException: {exception.InnerException.Message}");

            if (exception.InnerException.InnerException != null && !string.IsNullOrEmpty(exception.InnerException.InnerException.Message))
                messages.Add($"InnerException Inline: {exception.InnerException.InnerException.Message}");
        }

        if (!string.IsNullOrEmpty(exception.StackTrace))
            messages.Add($"StackTrace: {exception.StackTrace}");

        return messages;
    }

    #endregion

    #region Métodos Públicos

    /// <summary>
    /// Salva as informações em um arquivo para ser usado como log
    /// </summary>
    /// <param name="directory">Recebe uma string</param>
    /// <param name="file">>Recebe uma string</param>
    /// <param name="exception">>Recebe uma Exception</param>
    public static void Save(string directory, string file, Exception exception)
    {
        DirectoryHelper.Create(directory);

        FileHelper.Create(directory, file);

        WriteLogFile(directory, file, CreateMessageList(exception));
    }

    /// <summary>
    /// Salva as informações em um arquivo para ser usado como log
    /// </summary>
    /// <param name="directory">>Recebe uma string</param>
    /// <param name="file">>Recebe uma string</param>
    /// <param name="message">>Recebe uma string</param>
    public static void Save(string directory, string file, string message)
    {
        DirectoryHelper.Create(directory);

        FileHelper.Create(directory, file);

        WriteLogFile(directory, file, new List<string>() { message });
    }

    #endregion
}
