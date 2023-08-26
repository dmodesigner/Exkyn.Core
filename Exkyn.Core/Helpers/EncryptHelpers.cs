using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Exkyn.Core.Helpers
{
    public static class EncryptHelpers
    {
        #region Métodos Privados

        private static void Validate(string input)
        {
            if (string.IsNullOrEmpty(input))
                throw new ArgumentException("Não é possível criptografar / descriptografar uma variável vazia ou nula.");
        }

        private static void ValidateKeyAndVector(string key, string vector)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentException("Informe a chave KEY.");

            if (string.IsNullOrEmpty(vector))
                throw new ArgumentException("Informe o Vetor.");
        }

        #endregion

        #region Métodos Públicos

        public static string MD5(string input)
        {
            Validate(input);

            MD5 md5 = System.Security.Cryptography.MD5.Create();

            byte[] inputBytes = Encoding.ASCII.GetBytes(input);
            byte[] hash = md5.ComputeHash(inputBytes);

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < hash.Length; i++)
                sb.Append(hash[i].ToString("X2"));

            return sb.ToString();
        }

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
}