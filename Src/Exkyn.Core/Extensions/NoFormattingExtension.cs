using System.Text.RegularExpressions;

namespace Exkyn.Core.Extensions;

/// <summary>
/// Métodos de extensão para remoção de formatação e caracteres especiais de textos.
/// </summary>
public static partial class NoFormattingExtension
{
    [GeneratedRegex("[^0-9a-zA-Z]+")]
    private static partial Regex NonAlphanumericRegex();

    /// <summary>
    /// Remove todos os caracteres especiais, pontuações e espaços, mantendo apenas letras (a-z, A-Z) e números (0-9).
    /// </summary>
    /// <param name="value">String com formatação (ex: CPF, CNPJ, CEP, telefone).</param>
    /// <returns>String contendo apenas caracteres alfanuméricos ou <c>null</c> se a entrada for nula ou vazia.</returns>
    public static string? NoFormatting(this string? value) => string.IsNullOrWhiteSpace(value)
        ? null
        : NonAlphanumericRegex().Replace(value, string.Empty);
}