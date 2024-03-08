using System.Text.RegularExpressions;

namespace Exkyn.Core.Extensions;

public static class NoFormattingExtension
{
    /// <summary>
    /// Retorna a variável sem caracteres especiais
    /// </summary>
    /// <param name="value">Recebe a variável ou texto como parâmetro de entrada</param>
    /// <returns>Se for vazia, nula ou tenha apenas espaço em brancos retornara "null", caso contrário retornara a mesma informação sem formatações e caracteres especiais</returns>
    public static string NoFormatting(this string value) => string.IsNullOrWhiteSpace(value) 
        ? null 
        : Regex.Replace(value.Trim(), "[^0-9a-zA-Z]+", "");
}
