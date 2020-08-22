using System.Globalization;

namespace Exkyn.Core.Helpers
{
    public static class MoneyHelpers
    {
        public static string Coin(decimal value) => string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", value);

        public static string Coin(double value) => string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", value);

        public static string Coin(decimal value, string culture) => string.Format(CultureInfo.GetCultureInfo(culture), "{0:C}", value);

        public static string Coin(double value, string culture) => string.Format(CultureInfo.GetCultureInfo(culture), "{0:C}", value);
    }
}