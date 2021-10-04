using System;
using System.IO;

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

        #endregion

        #region Métodos Públicos

        public static void Create(string directory, string file)
        {
            ValidateFileDirectory(directory, file);

            DirectoryHelpers.AddSlashEnd(ref directory);

            if (!File.Exists(directory + file))
                File.Create(directory + file).Close();
        }

        public static bool Exist(string directory, string file)
        {
            ValidateFileDirectory(directory, file);

            DirectoryHelpers.AddSlashEnd(ref directory);

            return File.Exists(directory + file);
        }

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