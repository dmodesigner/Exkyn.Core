using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Exkyn.Core.Helpers
{
    public static class FileHelpers
    {
        #region Métodos Privados

        private static void ValidateFileDirectory(string directory, string file)
        {
            if (string.IsNullOrEmpty(directory))
                throw new ArgumentException("Você deve informar um caminho de diretório.");

            if (string.IsNullOrEmpty(file))
                throw new ArgumentException("Você deve informar o nome do arquivo.");
        }

        private static void WriteFile(string directory, string file, string text, Encoding encoding)
        {
            ValidateFileDirectory(directory, file);

            DirectoryHelpers.AddSlashEnd(ref directory);

            Create(directory, file);

            using (var f = new StreamWriter(directory + file, true, encoding))
            {
                f.WriteLine(text);
            }
        }

        #endregion

        #region Métodos Públicos

        public static bool Exist(string directory, string file)
        {
            ValidateFileDirectory(directory, file);

            DirectoryHelpers.AddSlashEnd(ref directory);

            return File.Exists(directory + file);
        }

        public static void Create(string directory, string file)
        {
            ValidateFileDirectory(directory, file);

            DirectoryHelpers.AddSlashEnd(ref directory);

            if (!File.Exists(directory + file))
                File.Create(directory + file).Close();
        }

        public static void Write(string directory, string file, string text) => WriteFile(directory, file, text, Encoding.UTF8);

        public static void Write(string directory, string file, string text, Encoding encoding) => WriteFile(directory, file, text, encoding);

        public static string ConvertToString(string directory, string file)
        {
            ValidateFileDirectory(directory, file);

            if (!File.Exists(directory + file))
                throw new ArgumentException($"O arquivo ({directory}{file}) não foi encontrado."); 
            
            DirectoryHelpers.AddSlashEnd(ref directory);

            return File.ReadAllText(directory + file);
        }

        #endregion
    }
}