using System.Text.RegularExpressions;

namespace Exkyn.Core.Extensions;

public static class UpperFirstLetterExtension
{
    /// <summary>
    /// Retorna a primeira letra de cada palavra em maiúsculo
    /// </summary>
    /// <param name="value">Recebe a variável ou texto como parâmetro de entrada</param>
    /// <returns>Se for vazia, nula ou contenha apenas espaço em brancos retornara "null", caso contrário retornara a mesma informação com as primeiras letras de cada palavra em maiúsculo</returns>
    public static string UpperFirstLetter(this string value) => string.IsNullOrWhiteSpace(value) 
        ? null
        : Regex.Replace(value, "[^0-9a-zA-Z]+", "");
}
