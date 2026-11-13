using System.Globalization;

namespace Exkyn.Core.Extensions;

/// <summary>
/// Métodos de extensão para formatação de valores numéricos em moeda.
/// </summary>
public static class MoneyExtension
{
    private const string DefaultCulture = "pt-BR";

    /// <summary>
    /// Retorna o valor monetário formatado no padrão de moeda brasileiro (pt-BR).
    /// </summary>
    /// <param name="value">Valor decimal a ser formatado.</param>
    /// <returns>String formatada como moeda (ex: R$ 1.234,56).</returns>
    public static string Money(this decimal value) => value.Money(DefaultCulture);

    /// <summary>
    /// Retorna o valor monetário formatado de acordo com a cultura informada.
    /// </summary>
    /// <param name="value">Valor decimal a ser formatado.</param>
    /// <param name="culture">Código da cultura (ex: "pt-BR", "en-US", "es-ES").</param>
    /// <returns>String formatada como moeda na cultura especificada.</returns>
    public static string Money(this decimal value, string culture)
    {
        if (string.IsNullOrWhiteSpace(culture))
            throw new ArgumentException("A cultura informada não pode ser nula ou vazia.", nameof(culture));

        return value.ToString("C", CultureInfo.GetCultureInfo(culture, predefinedOnly: true));
    }

    /// <summary>
    /// Retorna o valor monetário formatado no padrão de moeda brasileiro (pt-BR).
    /// </summary>
    /// <param name="value">Valor double a ser formatado.</param>
    /// <returns>String formatada como moeda (ex: R$ 1.234,56).</returns>
    public static string Money(this double value) => value.Money(DefaultCulture);

    /// <summary>
    /// Retorna o valor monetário formatado de acordo com a cultura informada.
    /// </summary>
    /// <param name="value">Valor double a ser formatado.</param>
    /// <param name="culture">Código da cultura (ex: "pt-BR", "en-US", "es-ES").</param>
    /// <returns>String formatada como moeda na cultura especificada.</returns>
    public static string Money(this double value, string culture)
    {
        if (string.IsNullOrWhiteSpace(culture))
            throw new ArgumentException("A cultura informada não pode ser nula ou vazia.", nameof(culture));

        return value.ToString("C", CultureInfo.GetCultureInfo(culture, predefinedOnly: true));
    }
}