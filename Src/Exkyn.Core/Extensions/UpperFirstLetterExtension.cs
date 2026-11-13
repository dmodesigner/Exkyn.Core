namespace Exkyn.Core.Extensions;

/// <summary>
/// Métodos de extensão para formatação de letras maiúsculas em strings.
/// </summary>
public static class UpperFirstLetterExtension
{
    /// <summary>
    /// Retorna o texto com a primeira letra em maiúsculo, removendo espaços nas extremidades.
    /// </summary>
    /// <param name="value">Texto a ser formatado.</param>
    /// <returns>Texto com a inicial maiúscula ou <c>null</c> se a entrada for nula ou vazia.</returns>
    public static string? UpperFirstLetter(this string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return null;

        ReadOnlySpan<char> trimmed = value.AsSpan().Trim();

        Span<char> firstChar = [char.ToUpper(trimmed[0])];

        return string.Concat(firstChar, trimmed[1..]);
    }
}