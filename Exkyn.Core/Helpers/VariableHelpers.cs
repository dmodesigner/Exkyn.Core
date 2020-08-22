using System.Text.RegularExpressions;

namespace Exkyn.Core.Helpers
{
    public static class VariableHelpers
    {
        public static string NoFormatting(string input)
        {
            if (string.IsNullOrEmpty(input))
                return null;

            input = input.Trim();

            input = Regex.Replace(input, "[^0-9a-zA-Z]+", "");

            return input;
        }

        public static string UpperFirstLetter(string input)
        {
            if (string.IsNullOrEmpty(input))
                return null;

            input = input.Trim();

            input = char.ToUpper(input[0]) + input.Substring(1);

            return input;
        }
    }
}