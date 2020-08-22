﻿using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Exkyn.Core.Helpers
{
    public static class EncryptHelpers
    {
        #region Tokens

        private static byte[] bIV = { 0x50, 0x08, 0xF1, 0xDD, 0xDE, 0x3C, 0xF2, 0x18, 0x44, 0x74, 0x19, 0x2C, 0x53, 0x49, 0xAB, 0xBC };
        private const string tokenCriptografiaBase64 = "Dj3pCz3yk9bsd5f9d54f8h1i5jb2gUm8sRhZwgLyBrV=";

        #endregion

        #region Métodos Privados

        private static void Validate(string input)
        {
            if (string.IsNullOrEmpty(input))
                throw new ArgumentException("Não é possível criptografar uma variável vazia ou nula.");
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

        public static string EncryptBase64(string input)
        {
            Validate(input);

            byte[] bKey = Convert.FromBase64String(tokenCriptografiaBase64);
            byte[] bText = new UTF8Encoding().GetBytes(input);

            // Instancia a classe de criptografia Rijndael
            Rijndael rijndael = new RijndaelManaged();

            // Define o tamanho da chave "256 = 8 * 32"                          
            // 128 (16 caracteres), 192 (24 caracteres) e 256 (32 caracteres)                
            rijndael.KeySize = 256;

            // Cria o espaço de memória para guardar o valor criptografado:                
            MemoryStream mStream = new MemoryStream();

            // Instancia o encriptador                 
            CryptoStream encryptor = new CryptoStream(mStream, rijndael.CreateEncryptor(bKey, bIV), CryptoStreamMode.Write);

            // Faz a escrita dos dados criptografados no espaço de memória
            encryptor.Write(bText, 0, bText.Length);

            // Despeja toda a memória.                
            encryptor.FlushFinalBlock();

            // Pega o vetor de bytes da memória e gera a string criptografada                
            return Convert.ToBase64String(mStream.ToArray());
        }

        public static string DecryptBase64(string input)
        {
            Validate(input);

            // Cria instancias de vetores de bytes com as chaves                
            byte[] bKey = Convert.FromBase64String(tokenCriptografiaBase64);
            byte[] bText = Convert.FromBase64String(input);

            // Instancia a classe de criptografia Rijndael                
            Rijndael rijndael = new RijndaelManaged();

            // Define o tamanho da chave "256 = 8 * 32"                         
            // 128 (16 caracteres), 192 (24 caracteres) e 256 (32 caracteres)                
            rijndael.KeySize = 256;

            // Cria o espaço de memória para guardar o valor DEScriptografado:               
            MemoryStream mStream = new MemoryStream();

            // Instancia o Decriptador                 
            CryptoStream decryptor = new CryptoStream(mStream, rijndael.CreateDecryptor(bKey, bIV), CryptoStreamMode.Write);

            // Faz a escrita dos dados criptografados no espaço de memória   
            decryptor.Write(bText, 0, bText.Length);

            // Despeja toda a memória.                
            decryptor.FlushFinalBlock();

            // Instancia a classe de codificação para que a string venha de forma correta         
            UTF8Encoding utf8 = new UTF8Encoding();

            // Com o vetor de bytes da memória, gera a string descritografada em UTF8       
            return utf8.GetString(mStream.ToArray());
        }

        #endregion
    }
}