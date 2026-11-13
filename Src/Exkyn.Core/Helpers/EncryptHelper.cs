using System.Security.Cryptography;
using System.Text;

namespace Exkyn.Core.Helpers;

/// <summary>
/// Classe utilitária para geração de hash SHA-256 e criptografia simétrica AES-256.
/// </summary>
public static class EncryptHelper
{
    private const string EnvKeyName = "Base64Key";
    private const string EnvVectorName = "Base64Vector";

    #region Métodos Privados

    private static void Validate(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            throw new ArgumentException("Não é possível criptografar / descriptografar uma variável vazia ou nula.", nameof(input));
    }

    private static void ValidateKeyAndVector(string key, string vector)
    {
        if (string.IsNullOrWhiteSpace(key))
            throw new ArgumentException($"Você deve informar o Key através de uma variável de ambiente {EnvKeyName} ou como parâmetro de entrada do método.", nameof(key));

        if (string.IsNullOrWhiteSpace(vector))
            throw new ArgumentException($"Você deve informar o Vetor através de uma variável de ambiente {EnvVectorName} ou como parâmetro de entrada do método.", nameof(vector));
    }

    #endregion

    #region Métodos Públicos

    /// <summary>
    /// Gera o hash SHA-256 em formato hexadecimal maiúsculo a partir da string informada.
    /// </summary>
    /// <param name="input">Texto de entrada.</param>
    /// <returns>Hash SHA-256 hexadecimal de 64 caracteres.</returns>
    public static string SHA(string input)
    {
        Validate(input);

        byte[] bytes = Encoding.UTF8.GetBytes(input);
        byte[] hash = SHA256.HashData(bytes);

        return Convert.ToHexString(hash);
    }

    /// <summary>
    /// Criptografa o texto usando AES-256 lendo a chave e o IV das variáveis de ambiente <c>Base64Key</c> e <c>Base64Vector</c>.
    /// </summary>
    /// <param name="input">Texto plano a ser criptografado.</param>
    /// <returns>Texto criptografado em Base64.</returns>
    public static string AesEncryptor(string input) =>
        AesEncryptor(
            input,
            Environment.GetEnvironmentVariable(EnvKeyName) ?? string.Empty,
            Environment.GetEnvironmentVariable(EnvVectorName) ?? string.Empty);

    /// <summary>
    /// Criptografa o texto usando AES-256 (CBC / PKCS7) com a chave e o vetor de inicialização informados.
    /// </summary>
    /// <param name="input">Texto plano a ser criptografado.</param>
    /// <param name="base64Key">Chave de 256 bits (32 bytes) codificada em Base64.</param>
    /// <param name="base64Vector">Vetor de inicialização (IV de 16 bytes) codificado em Base64.</param>
    /// <returns>Texto criptografado em Base64.</returns>
    public static string AesEncryptor(string input, string base64Key, string base64Vector)
    {
        Validate(input);
        ValidateKeyAndVector(base64Key, base64Vector);

        using Aes aes = Aes.Create();
        aes.KeySize = 256;
        aes.Key = Convert.FromBase64String(base64Key);
        byte[] iv = Convert.FromBase64String(base64Vector);

        byte[] plainBytes = Encoding.UTF8.GetBytes(input);
        byte[] encryptedBytes = aes.EncryptCbc(plainBytes, iv, PaddingMode.PKCS7);

        return Convert.ToBase64String(encryptedBytes);
    }

    /// <summary>
    /// Descriptografa o texto em Base64 usando AES-256 lendo a chave e o IV das variáveis de ambiente <c>Base64Key</c> e <c>Base64Vector</c>.
    /// </summary>
    /// <param name="input">Texto criptografado em Base64.</param>
    /// <returns>Texto original descriptografado.</returns>
    public static string AesDecryptor(string input) =>
        AesDecryptor(
            input,
            Environment.GetEnvironmentVariable(EnvKeyName) ?? string.Empty,
            Environment.GetEnvironmentVariable(EnvVectorName) ?? string.Empty);

    /// <summary>
    /// Descriptografa o texto em Base64 usando AES-256 (CBC / PKCS7) com a chave e o vetor de inicialização informados.
    /// </summary>
    /// <param name="input">Texto criptografado em Base64.</param>
    /// <param name="base64Key">Chave de 256 bits (32 bytes) codificada em Base64.</param>
    /// <param name="base64Vector">Vetor de inicialização (IV de 16 bytes) codificado em Base64.</param>
    /// <returns>Texto original descriptografado.</returns>
    public static string AesDecryptor(string input, string base64Key, string base64Vector)
    {
        Validate(input);
        ValidateKeyAndVector(base64Key, base64Vector);

        using Aes aes = Aes.Create();
        aes.KeySize = 256;
        aes.Key = Convert.FromBase64String(base64Key);
        byte[] iv = Convert.FromBase64String(base64Vector);

        byte[] cipherBytes = Convert.FromBase64String(input);
        byte[] decryptedBytes = aes.DecryptCbc(cipherBytes, iv, PaddingMode.PKCS7);

        return Encoding.UTF8.GetString(decryptedBytes);
    }

    #endregion
}