using System.Text.RegularExpressions;

namespace Exkyn.Core.Extensions;

public static class NoFormattingExtension
{
    /// <summary>
    /// Retorna a variável sem caracteres especiais
    /// </summary>
    /// <param name="value">Recebe uma string</param>
    /// <returns>Retorna uma string</returns>
    public static string? NoFormatting(this string? value) => string.IsNullOrWhiteSpace(value)
        ? null
        : Regex.Replace(value.Trim(), "[^0-9a-zA-Z]+", "");
}
