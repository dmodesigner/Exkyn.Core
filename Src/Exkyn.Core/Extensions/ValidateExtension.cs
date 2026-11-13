using System.Text.RegularExpressions;

namespace Exkyn.Core.Extensions;

/// <summary>
/// Métodos de extensão para validação de documentos brasileiros (CPF, CNPJ, PIS) e e-mail.
/// </summary>
public static partial class ValidateExtension
{
    private static readonly int[] CnpjMultiplier1 = [5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2];
    private static readonly int[] CnpjMultiplier2 = [6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2];
    private static readonly int[] PisMultiplier = [3, 2, 9, 8, 7, 6, 5, 4, 3, 2];

    [GeneratedRegex(@"^[A-Za-z0-9]+([._+-][A-Za-z0-9]+)*@[A-Za-z0-9]+([.-][A-Za-z0-9]+)*\.[A-Za-z]{2,}$", RegexOptions.CultureInvariant)]
    private static partial Regex EmailRegex();

    /// <summary>
    /// Valida se a string informada é um CPF válido.
    /// </summary>
    /// <param name="cpf">CPF com ou sem máscara.</param>
    /// <returns><c>true</c> se o CPF for válido; caso contrário, <c>false</c>.</returns>
    public static bool ValidateCPF(this string? cpf)
    {
        cpf = cpf.NoFormatting();

        if (cpf is null || cpf.Length != 11 || !IsValidNumericSequence(cpf) || cpf == "12345678909")
            return false;

        int sum1 = 0;
        for (int i = 0; i < 9; i++)
        {
            sum1 += (cpf[i] - '0') * (10 - i);
        }

        int remainder1 = sum1 % 11;
        int digit1 = remainder1 < 2 ? 0 : 11 - remainder1;

        if ((cpf[9] - '0') != digit1)
            return false;

        int sum2 = 0;
        for (int i = 0; i < 10; i++)
        {
            sum2 += (cpf[i] - '0') * (11 - i);
        }

        int remainder2 = sum2 % 11;
        int digit2 = remainder2 < 2 ? 0 : 11 - remainder2;

        return (cpf[10] - '0') == digit2;
    }

    /// <summary>
    /// Valida se a string informada é um CNPJ válido.
    /// </summary>
    /// <param name="cnpj">CNPJ com ou sem máscara.</param>
    /// <returns><c>true</c> se o CNPJ for válido; caso contrário, <c>false</c>.</returns>
    public static bool ValidateCNPJ(this string? cnpj)
    {
        cnpj = cnpj.NoFormatting();

        if (cnpj is null || cnpj.Length != 14 || !IsValidNumericSequence(cnpj))
            return false;

        int sum1 = 0;
        for (int i = 0; i < 12; i++)
        {
            sum1 += (cnpj[i] - '0') * CnpjMultiplier1[i];
        }

        int remainder1 = sum1 % 11;
        int digit1 = remainder1 < 2 ? 0 : 11 - remainder1;

        if ((cnpj[12] - '0') != digit1)
            return false;

        int sum2 = 0;
        for (int i = 0; i < 13; i++)
        {
            sum2 += (cnpj[i] - '0') * CnpjMultiplier2[i];
        }

        int remainder2 = sum2 % 11;
        int digit2 = remainder2 < 2 ? 0 : 11 - remainder2;

        return (cnpj[13] - '0') == digit2;
    }

    /// <summary>
    /// Valida se a string informada é um PIS/PASEP/NIT válido.
    /// </summary>
    /// <param name="pis">PIS com ou sem máscara.</param>
    /// <returns><c>true</c> se o PIS for válido; caso contrário, <c>false</c>.</returns>
    public static bool ValidatePIS(this string? pis)
    {
        pis = pis.NoFormatting();

        if (pis is null || pis.Length != 11 || !IsValidNumericSequence(pis))
            return false;

        int sum = 0;
        for (int i = 0; i < 10; i++)
        {
            sum += (pis[i] - '0') * PisMultiplier[i];
        }

        int remainder = sum % 11;
        int expectedDigit = remainder < 2 ? 0 : 11 - remainder;

        return (pis[10] - '0') == expectedDigit;
    }

    /// <summary>
    /// Valida se a string informada possui formato de e-mail válido.
    /// </summary>
    /// <param name="email">Endereço de e-mail a ser validado.</param>
    /// <returns><c>true</c> se o e-mail for válido; caso contrário, <c>false</c>.</returns>
    public static bool ValidateEmail(this string? email)
    {
        if (string.IsNullOrWhiteSpace(email))
            return false;

        return EmailRegex().IsMatch(email.Trim());
    }

    /// <summary>
    /// Verifica se todos os caracteres são dígitos numéricos e se não são todos repetidos (ex: 00000000000).
    /// </summary>
    private static bool IsValidNumericSequence(string value)
    {
        bool allSame = true;

        for (int i = 0; i < value.Length; i++)
        {
            if (!char.IsAsciiDigit(value[i]))
                return false;

            if (value[i] != value[0])
                allSame = false;
        }

        return !allSame;
    }
}