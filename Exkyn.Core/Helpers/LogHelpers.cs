using System;
using System.Collections.Generic;
using System.IO;

namespace Exkyn.Core.Helpers
{
    public static class LogHelpers
    {
        #region Métodos Privados

        private static void WriteLogFile(string directory, string file, List<string> messages)
        {
            DirectoryHelpers.AddSlashEnd(ref directory);

            using (var f = new StreamWriter(directory + file, true))
            {
                foreach (string item in messages)
                {
                    if (!string.IsNullOrEmpty(item))
                        f.WriteLine(item);
                }

                f.WriteLine("\n\n");
            }
        }

        private static List<string> CreateMessageList(Exception exception)
        {
            var messages = new List<string>();

            messages.Add($"{DateTime.Now} - {exception.Message}");

            if (!string.IsNullOrEmpty(exception.Source))
                messages.Add($"Source: {exception.Source}");

            if (!string.IsNullOrEmpty(exception.HelpLink))
                messages.Add($"HelpLink: {exception.HelpLink}");

            if (exception.InnerException != null && !string.IsNullOrEmpty(exception.InnerException.Message))
            {
                messages.Add($"InnerException: {exception.InnerException.Message}");

                if (exception.InnerException.InnerException != null && !string.IsNullOrEmpty(exception.InnerException.InnerException.Message))
                    messages.Add($"InnerException Inline: {exception.InnerException.InnerException.Message}");
            }

            if (!string.IsNullOrEmpty(exception.StackTrace))
                messages.Add($"StackTrace: {exception.StackTrace}");

            return messages;
        }

        #endregion

        #region Métodos Públicos

        public static void Save(string directory, string file, Exception exception)
        {
            DirectoryHelpers.Create(directory);

            FileHelpers.Create(directory, file);

            WriteLogFile(directory, file, CreateMessageList(exception));
        }

        public static void Save(string directory, string file, string message)
        {
            DirectoryHelpers.Create(directory);

            FileHelpers.Create(directory, file);

            WriteLogFile(directory, file, new List<string>() { message });
        }

        #endregion
    }
}