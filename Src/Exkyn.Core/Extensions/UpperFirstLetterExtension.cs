namespace Exkyn.Core.Extensions;

public static class UpperFirstLetterExtension
{
    /// <summary>
    /// Retorna a primeira letra da palavra em maiúsculo
    /// </summary>
    /// <param name="value">Recebe uma string</param>
    /// <returns>Retorna uma string</returns>
    public static string? UpperFirstLetter(this string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return null;

        value = value.Trim();

        return char.ToUpper(value[0]) + value.Substring(1);
    }
}
