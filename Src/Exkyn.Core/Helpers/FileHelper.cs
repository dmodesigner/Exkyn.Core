using System.Text;

namespace Exkyn.Core.Helpers;

/// <summary>
/// Classe utilitária para verificação, criação, escrita e leitura de arquivos.
/// </summary>
public static class FileHelper
{
    #region Métodos Privados

    private static string GetValidatedFilePath(string directory, string file)
    {
        if (string.IsNullOrWhiteSpace(directory))
            throw new ArgumentException("Você deve informar um caminho de diretório.", nameof(directory));

        if (string.IsNullOrWhiteSpace(file))
            throw new ArgumentException("Você deve informar o nome do arquivo.", nameof(file));

        return Path.Combine(directory.Trim(), file.Trim());
    }

    private static void WriteFile(string directory, string file, string text, Encoding encoding)
    {
        ArgumentNullException.ThrowIfNull(encoding);

        var fullPath = GetValidatedFilePath(directory, file);

        DirectoryHelper.Create(directory);

        using var writer = new StreamWriter(fullPath, append: true, encoding);
        writer.WriteLine(text);
    }

    #endregion

    #region Métodos Públicos

    /// <summary>
    /// Verifica se o arquivo existe no diretório informado.
    /// </summary>
    /// <param name="directory">Caminho do diretório.</param>
    /// <param name="file">Nome do arquivo.</param>
    /// <returns><c>true</c> se o arquivo existir; caso contrário, <c>false</c>.</returns>
    public static bool Exist(string directory, string file)
    {
        var fullPath = GetValidatedFilePath(directory, file);
        return File.Exists(fullPath);
    }

    /// <summary>
    /// Cria um arquivo vazio no diretório informado caso ainda não exista.
    /// </summary>
    /// <param name="directory">Caminho do diretório.</param>
    /// <param name="file">Nome do arquivo.</param>
    public static void Create(string directory, string file)
    {
        var fullPath = GetValidatedFilePath(directory, file);

        DirectoryHelper.Create(directory);

        if (!File.Exists(fullPath))
        {
            using var _ = File.Create(fullPath);
        }
    }

    /// <summary>
    /// Escreve uma nova linha de texto no arquivo informado utilizando codificação UTF-8.
    /// </summary>
    /// <param name="directory">Caminho do diretório.</param>
    /// <param name="file">Nome do arquivo.</param>
    /// <param name="text">Conteúdo de texto a ser adicionado.</param>
    public static void Write(string directory, string file, string text)
        => WriteFile(directory, file, text, Encoding.UTF8);

    /// <summary>
    /// Escreve uma nova linha de texto no arquivo informado utilizando a codificação especificada.
    /// </summary>
    /// <param name="directory">Caminho do diretório.</param>
    /// <param name="file">Nome do arquivo.</param>
    /// <param name="text">Conteúdo de texto a ser adicionado.</param>
    /// <param name="encoding">Codificação de caracteres a ser utilizada.</param>
    public static void Write(string directory, string file, string text, Encoding encoding)
        => WriteFile(directory, file, text, encoding);

    /// <summary>
    /// Lê todo o conteúdo do arquivo informado e retorna em uma string.
    /// </summary>
    /// <param name="directory">Caminho do diretório.</param>
    /// <param name="file">Nome do arquivo.</param>
    /// <returns>Conteúdo textual do arquivo.</returns>
    /// <exception cref="ArgumentException">Lançada quando os parâmetros são vazios ou o arquivo não é encontrado.</exception>
    public static string ReadFile(string directory, string file)
    {
        var fullPath = GetValidatedFilePath(directory, file);

        if (!File.Exists(fullPath))
            throw new ArgumentException($"O arquivo ({fullPath}) não foi encontrado.");

        return File.ReadAllText(fullPath);
    }

    #endregion
}