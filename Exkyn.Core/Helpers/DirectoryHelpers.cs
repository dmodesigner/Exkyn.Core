using System;
using System.Collections.Generic;
using System.IO;

namespace Exkyn.Core.Helpers
{
    public static class DirectoryHelpers
    {
        #region Métodos Privados

        private static void ValidateDirectory(string directory)
        {
            if (string.IsNullOrEmpty(directory))
                throw new ArgumentException("O diretório não pode ser vazio ou nulo.");
        }

        #endregion

        #region Métodos Públicos

        public static void AddSlashEnd(ref string directory)
        {
            ValidateDirectory(directory);

            if (!directory.EndsWith("\\"))
                directory += "\\";
        }

        public static void Create(string directory)
        {
            ValidateDirectory(directory);

            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);
        }

        public static List<string> ListDirectoryFile(string directory)
        {
            ValidateDirectory(directory);

            var dir = new DirectoryInfo(directory);

            FileInfo[] Files = dir.GetFiles("*", SearchOption.AllDirectories);

            var files = new List<string>();

            foreach (FileInfo File in Files)
            {
                files.Add(File.FullName.Replace(directory, "").Replace("\\", ""));
            }

            return files;
        }

        #endregion
    }
}