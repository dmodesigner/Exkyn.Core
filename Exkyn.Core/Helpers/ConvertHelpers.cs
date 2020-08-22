using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace Exkyn.Core.Helpers
{
    public static class ConvertHelpers
    {
        #region Métodos Privados

        private static DataTable ListDataTable<T>(IList<T> list)
        {
            if (list == null || list.Count() == 0)
                throw new ArgumentException("Informe uma lista com conteúdo para converter a um DataTable.");

            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));

            DataTable table = new DataTable();

            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);

            foreach (T item in list)
            {
                DataRow row = table.NewRow();

                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;

                table.Rows.Add(row);
            }

            return table;
        }

        #endregion

        #region Métodos Públicos

        public static int Int(string input)
        {
            int number = 0;

            int.TryParse(input, out number);

            return number;
        }

        public static string ObjectToString<T>(T obj)
        {
            if (obj == null)
                throw new ArgumentException("Não é possível converter um objeto nulo para uma variável de texto.");

            var xml = new XmlSerializer(obj.GetType());

            var text = new StringWriter();

            xml.Serialize(text, obj);

            return text.ToString();
        }

        public static T StringToObject<T>(string input) where T : new()
        {
            if (string.IsNullOrEmpty(input))
                throw new ArgumentException("Informe uma variável que não seja vazia ou nula para converter a um objeto.");

            var obj = new T();

            var type = obj.GetType();

            var xml = new XmlSerializer(type);

            var result = new StringReader(input);

            return (T)xml.Deserialize(result);
        }

        public static DataTable ListToDataTable<T>(List<T> list) => ListDataTable<T>(list);

        public static DataTable ListToDataTable<T>(IList<T> list) => ListDataTable<T>(list);

        #endregion
    }
}