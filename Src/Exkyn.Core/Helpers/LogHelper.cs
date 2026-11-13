using System.Text;

namespace Exkyn.Core.Helpers;

/// <summary>
/// Classe utilitária para gravação de logs de mensagens e exceções em arquivos de texto.
/// </summary>
public static class LogHelper
{
    #region Métodos Privados

    private static void ValidateParameters(string directory, string file)
    {
        if (string.IsNullOrWhiteSpace(directory))
            throw new ArgumentException("Você deve informar um caminho de diretório.", nameof(directory));

        if (string.IsNullOrWhiteSpace(file))
            throw new ArgumentException("Você deve informar o nome do arquivo.", nameof(file));
    }

    private static void WriteLogFile(string directory, string file, IEnumerable<string> messages)
    {
        ValidateParameters(directory, file);

        DirectoryHelper.Create(directory);

        var fullPath = Path.Combine(directory.Trim(), file.Trim());

        using var writer = new StreamWriter(fullPath, append: true, Encoding.UTF8);

        foreach (string item in messages)
        {
            if (!string.IsNullOrWhiteSpace(item))
                writer.WriteLine(item);
        }

        writer.WriteLine();
    }

    private static List<string> CreateMessageList(Exception exception)
    {
        var messages = new List<string>
        {
            $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {exception.Message}"
        };

        if (!string.IsNullOrWhiteSpace(exception.Source))
            messages.Add($"Source: {exception.Source}");

        if (!string.IsNullOrWhiteSpace(exception.HelpLink))
            messages.Add($"HelpLink: {exception.HelpLink}");

        if (exception.InnerException is not null && !string.IsNullOrWhiteSpace(exception.InnerException.Message))
        {
            messages.Add($"InnerException: {exception.InnerException.Message}");

            if (exception.InnerException.InnerException is not null &&
                !string.IsNullOrWhiteSpace(exception.InnerException.InnerException.Message))
            {
                messages.Add($"InnerException Inline: {exception.InnerException.InnerException.Message}");
            }
        }

        if (!string.IsNullOrWhiteSpace(exception.StackTrace))
            messages.Add($"StackTrace: {exception.StackTrace}");

        return messages;
    }

    #endregion

    #region Métodos Públicos

    /// <summary>
    /// Salva os detalhes completos de uma <see cref="Exception"/> (incluindo InnerExceptions e StackTrace) em um arquivo de log.
    /// </summary>
    /// <param name="directory">Caminho do diretório onde o log será salvo.</param>
    /// <param name="file">Nome do arquivo de log.</param>
    /// <param name="exception">Exceção a ser registrada.</param>
    public static void Save(string directory, string file, Exception exception)
    {
        ArgumentNullException.ThrowIfNull(exception);

        WriteLogFile(directory, file, CreateMessageList(exception));
    }

    /// <summary>
    /// Salva uma mensagem de texto em um arquivo de log.
    /// </summary>
    /// <param name="directory">Caminho do diretório onde o log será salvo.</param>
    /// <param name="file">Nome do arquivo de log.</param>
    /// <param name="message">Mensagem a ser registrada.</param>
    public static void Save(string directory, string file, string message)
    {
        if (string.IsNullOrWhiteSpace(message))
            throw new ArgumentException("A mensagem de log não pode ser nula ou vazia.", nameof(message));

        WriteLogFile(directory, file, [message]);
    }

    #endregion
}