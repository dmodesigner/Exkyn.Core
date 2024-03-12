using System.Text;

namespace Exkyn.Core.Helpers;

public static class FileHelper
{
    #region Métodos Privados

    private static void ValidateFileDirectory(string directory, string file)
    {
        if (string.IsNullOrWhiteSpace(directory))
            throw new ArgumentException("Você deve informar um caminho de diretório.");

        if (string.IsNullOrWhiteSpace(file))
            throw new ArgumentException("Você deve informar o nome do arquivo.");
    }

    private static void WriteFile(string directory, string file, string text, Encoding encoding)
    {
        ValidateFileDirectory(directory, file);

        directory = DirectoryHelper.AddSlashEnd(directory);

        Create(directory, file);

        using (var f = new StreamWriter(directory + file, true, encoding))
        {
            f.WriteLine(text);
        }
    }

    #endregion

    #region Métodos Públicos

    /// <summary>
    /// Verifica se o arquivo existe no diretório
    /// </summary>
    /// <param name="directory">Recebe uma string</param>
    /// <param name="file">Recebe uma string</param>
    /// <returns>Retorna um bool</returns>
    public static bool Exist(string directory, string file)
    {
        ValidateFileDirectory(directory, file);

        directory = DirectoryHelper.AddSlashEnd(directory);

        return File.Exists(directory + file);
    }

    /// <summary>
    /// Cria um arquivo no diretório
    /// </summary>
    /// <param name="directory">Recebe uma string</param>
    /// <param name="file">Recebe uma string</param>
    public static void Create(string directory, string file)
    {
        ValidateFileDirectory(directory, file);

        directory = DirectoryHelper.AddSlashEnd(directory);

        if (!File.Exists(directory + file))
            File.Create(directory + file).Close();
    }


    /// <summary>
    /// Escreve no arquivo informado
    /// </summary>
    /// <param name="directory">Recebe uma string</param>
    /// <param name="file">Recebe uma string</param>
    /// <param name="text">Recebe uma string</param>
    public static void Write(string directory, string file, string text) => WriteFile(directory, file, text, Encoding.UTF8);

    /// <summary>
    /// Escreve no arquivo informado
    /// </summary>
    /// <param name="directory">Recebe uma string</param>
    /// <param name="file">Recebe uma string</param>
    /// <param name="text">Recebe uma string</param>
    /// <param name="encoding">Recebe um Encoding</param>
    public static void Write(string directory, string file, string text, Encoding encoding) => WriteFile(directory, file, text, encoding);

    /// <summary>
    /// Le os dados do arquivo e retorna em uma string o conteúdo
    /// </summary>
    /// <param name="directory">Recebe uma string</param>
    /// <param name="file">Recebe uma string</param>
    /// <returns>Retorna uma string</returns>
    /// <exception cref="ArgumentException">Pode retornar um ArgumentException</exception>
    public static string ReadFile(string directory, string file)
    {
        ValidateFileDirectory(directory, file);

        if (!File.Exists(directory + file))
            throw new ArgumentException($"O arquivo ({directory}{file}) não foi encontrado.");

        directory = DirectoryHelper.AddSlashEnd(directory);

        return File.ReadAllText(directory + file);
    }

    #endregion
}
