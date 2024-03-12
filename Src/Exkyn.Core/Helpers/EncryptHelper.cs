using System.Security.Cryptography;
using System.Text;

namespace Exkyn.Core.Helpers;

public class EncryptHelper
{
    #region Métodos Privados

    private static void Validate(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            throw new ArgumentException("Não é possível criptografar / descriptografar uma variável vazia ou nula.");
    }

    private static void ValidateKeyAndVector(string key, string vector)
    {
        if (string.IsNullOrWhiteSpace(key))
            throw new ArgumentException("Informe a chave KEY.");

        if (string.IsNullOrWhiteSpace(vector))
            throw new ArgumentException("Informe o Vetor.");
    }

    #endregion

    #region Métodos Públicos

    /// <summary>
    /// Criptografa em SHA 256
    /// </summary>
    /// <param name="input">Recebe uma string</param>
    /// <returns>Retorna uma string</returns>
    public static string SHA(string input)
    {
        Validate(input);

        string hashString = string.Empty;

        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] bytes = Encoding.UTF8.GetBytes(input);

            byte[] hash = sha256.ComputeHash(bytes);

            hashString = BitConverter.ToString(hash).Replace("-", string.Empty);
        }

        return hashString;
    }

    /// <summary>
    /// Criptografa usando a AES 256
    /// </summary>
    /// <param name="input">Recebe uma string</param>
    /// <param name="base64Key">Recebe uma string base 64</param>
    /// <param name="base64Vector">Recebe uma string base 64</param>
    /// <returns>Retorna uma string</returns>
    public static string AesEncryptor(string input, string base64Key, string base64Vector)
    {
        Validate(input);

        ValidateKeyAndVector(base64Key, base64Vector);

        using (Aes aes = Aes.Create())
        {
            aes.KeySize = 256;
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;
            aes.Key = Convert.FromBase64String(base64Key);
            aes.IV = Convert.FromBase64String(base64Vector);

            byte[] encryptedData;

            using (ICryptoTransform encryptor = aes.CreateEncryptor())
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter sw = new StreamWriter(cs))
                        {
                            sw.Write(input);
                        }
                        encryptedData = ms.ToArray();
                    }
                }
            }

            return Convert.ToBase64String(encryptedData);
        }
    }

    /// <summary>
    /// Descriptografa usando o AES 256
    /// </summary>
    /// <param name="input">Recebe uma string</param>
    /// <param name="base64Key">Recebe uma string base 64</param>
    /// <param name="base64Vector">Recebe uma string base 64</param>
    /// <returns>Retorna uma string</returns>
    /// <exception cref="ArgumentException">Pode retornar um ArgumentException</exception>
    public static string AesDecryptor(string input, string base64Key, string base64Vector)
    {
        if (input == null || input.Length == 0)
            throw new ArgumentException("Informe o texto a ser descriptografado.");

        ValidateKeyAndVector(base64Key, base64Vector);

        var r = string.Empty;

        using (Aes aes = Aes.Create())
        {
            aes.KeySize = 256;
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;
            aes.Key = Convert.FromBase64String(base64Key);
            aes.IV = Convert.FromBase64String(base64Vector);

            using (ICryptoTransform decryptor = aes.CreateDecryptor())
            {
                byte[] cipher = Convert.FromBase64String(input);

                using (MemoryStream ms = new MemoryStream(cipher))
                {
                    using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader sr = new StreamReader(cs))
                        {
                            r = sr.ReadToEnd();
                        }
                    }
                }
            }
        }

        return r;
    }

    #endregion
}
