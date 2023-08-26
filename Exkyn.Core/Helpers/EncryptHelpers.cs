using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

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

        public static byte[] AesEncryptor(string input, byte[] key, byte[] iv)
        {
            Validate(input);

            byte[] inputBytes;

            using (Aes aes = Aes.Create())
            {
                aes.KeySize = 256;
                aes.Mode = CipherMode.CBC;
                aes.Key = key;
                aes.IV = iv;
                
                using (ICryptoTransform encryptor = aes.CreateEncryptor())
                {
                    var inputBuffer = Encoding.UTF8.GetBytes(input);
                    inputBytes = encryptor.TransformFinalBlock(inputBuffer, 0, inputBuffer.Length);
                }
            }
            
            return inputBytes;
        }

        public static string AesDecryptor(byte[] input, byte[] key, byte[] iv)
        {
            var r = string.Empty;

            using (Aes aes = Aes.Create())
            {
                aes.KeySize = 256;
                aes.Mode = CipherMode.CBC;

                using (ICryptoTransform decryptor = aes.CreateDecryptor(key, iv))
                {

                    using (MemoryStream msDecrypt = new MemoryStream(input))
                    {

                        using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                        {

                            using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                            {
                                r = srDecrypt.ReadToEnd();
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