namespace Exkyn.Core.Helpers;

public static class DirectoryHelper
{
    #region Métodos Privados

    private static void ValidateDirectory(string directory)
    {
        if (string.IsNullOrWhiteSpace(directory))
            throw new ArgumentException("O diretório não pode ser vazio ou nulo.");
    }

    #endregion

    #region Métodos Públicos

    /// <summary>
    /// Adiciona uma \ ao final do caminho se não existir
    /// </summary>
    /// <param name="directory">Recebe uma string</param>
    /// <returns>Retorna uma string</returns>
    public static string AddSlashEnd(string directory)
    {
        ValidateDirectory(directory);

        if (!directory.EndsWith("\\"))
            directory += "\\";

        return directory;
    }

    /// <summary>
    /// Cria o diretório informado se não existir
    /// </summary>
    /// <param name="directory">Recebe uma string</param>
    public static void Create(string directory)
    {
        ValidateDirectory(directory);

        if (!Directory.Exists(directory))
            Directory.CreateDirectory(directory);
    }

    /// <summary>
    /// Retorna uma lista com os arquivos encontrado no diretório informado
    /// </summary>
    /// <param name="directory">Recebe uma string</param>
    /// <returns>Retorna uma lista de string</returns>
    public static List<string> ListDirectoryFile(string directory)
    {
        ValidateDirectory(directory);

        var dir = new DirectoryInfo(directory);

        FileInfo[] Files = dir.GetFiles("*", SearchOption.AllDirectories);

        var files = new List<string>();

        foreach (FileInfo File in Files)
        {
            files.Add(File.FullName.Replace(directory, "").Replace("\\", ""));
        }

        return files;
    }

    #endregion
}
