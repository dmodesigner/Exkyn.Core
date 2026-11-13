namespace Exkyn.Core.Helpers;

/// <summary>
/// Classe utilitária para manipulação e consulta de diretórios.
/// </summary>
public static class DirectoryHelper
{
    #region Métodos Privados

    private static void ValidateDirectory(string directory)
    {
        if (string.IsNullOrWhiteSpace(directory))
            throw new ArgumentException("O diretório não pode ser vazio ou nulo.", nameof(directory));
    }

    #endregion

    #region Métodos Públicos

    /// <summary>
    /// Adiciona uma barra invertida (<c>\</c>) ao final do caminho, caso ainda não exista.
    /// </summary>
    /// <param name="directory">Caminho do diretório.</param>
    /// <returns>Caminho do diretório finalizado com <c>\</c>.</returns>
    public static string AddSlashEnd(string directory)
    {
        ValidateDirectory(directory);

        directory = directory.Trim();

        return directory.EndsWith('\\') ? directory : $"{directory}\\";
    }

    /// <summary>
    /// Cria o diretório informado (e subdiretórios necessários) caso ainda não exista.
    /// </summary>
    /// <param name="directory">Caminho do diretório a ser criado.</param>
    public static void Create(string directory)
    {
        ValidateDirectory(directory);

        Directory.CreateDirectory(directory.Trim());
    }

    /// <summary>
    /// Retorna uma lista com os nomes dos arquivos encontrados no diretório informado e em seus subdiretórios.
    /// </summary>
    /// <param name="directory">Caminho do diretório a ser pesquisado.</param>
    /// <returns>Lista contendo os nomes dos arquivos encontrados.</returns>
    public static List<string> ListDirectoryFile(string directory)
    {
        ValidateDirectory(directory);

        var dir = new DirectoryInfo(directory.Trim());

        if (!dir.Exists)
            throw new DirectoryNotFoundException($"O diretório '{directory}' não foi encontrado.");

        return dir
            .EnumerateFiles("*", SearchOption.AllDirectories)
            .Select(f => f.Name)
            .ToList();
    }

    #endregion
}