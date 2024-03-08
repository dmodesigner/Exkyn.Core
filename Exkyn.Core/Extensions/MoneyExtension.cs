using System.Globalization;

namespace Exkyn.Core.Extensions;

public static class MoneyExtension
{
    /// <summary>
    /// Retorna o valor monetário formatado no padrão de moeda brasileiro
    /// </summary>
    /// <param name="value">Recebe um decimal</param>
    /// <returns>Retorna uma string</returns>
    public static string Money(this decimal value) => string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", value);

    /// <summary>
    /// Retorna o valor monetário formatado no padrão de moeda brasileiro
    /// </summary>
    /// <param name="value">>Recebe um decimal</param>
    /// <param name="culture">Recebe uma string</param>
    /// <returns>Retorna uma string</returns>
    public static string Money(this decimal value, string culture) => string.Format(CultureInfo.GetCultureInfo(culture), "{0:C}", value);

    /// <summary>
    /// Retorna o valor monetário formatado no padrão de moeda brasileiro
    /// </summary>
    /// <param name="value">Recebe um double</param>
    /// <returns>Retorna uma string</returns>
    public static string Money(this double value) => string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", value);

    /// <summary>
    /// Retorna o valor monetário formatado no padrão de moeda brasileiro
    /// </summary>
    /// <param name="value">>Recebe um double</param>
    /// <param name="culture">Recebe uma string</param>
    /// <returns>Retorna uma string</returns>
    public static string Money(this double value, string culture) => string.Format(CultureInfo.GetCultureInfo(culture), "{0:C}", value);
}
