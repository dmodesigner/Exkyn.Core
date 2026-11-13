using System.ComponentModel;
using System.Data;
using System.Xml.Serialization;

namespace Exkyn.Core.Extensions;

/// <summary>
/// Métodos de extensão para conversão entre objetos, XML e DataTable.
/// </summary>
public static class ConvertExtension
{
    #region Métodos Privados

    private static DataTable ListDataTable<T>(IList<T> list)
    {
        if (list is null || list.Count == 0)
            throw new ArgumentException("Informe uma lista com conteúdo para converter a um DataTable.", nameof(list));

        PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));

        var table = new DataTable(typeof(T).Name);

        foreach (PropertyDescriptor prop in properties)
        {
            table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
        }

        foreach (T item in list)
        {
            DataRow row = table.NewRow();

            foreach (PropertyDescriptor prop in properties)
            {
                row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
            }

            table.Rows.Add(row);
        }

        return table;
    }

    #endregion

    #region Métodos Públicos

    /// <summary>
    /// Serializa um objeto para uma string no formato XML.
    /// </summary>
    /// <typeparam name="T">Tipo do objeto.</typeparam>
    /// <param name="obj">Instância do objeto a ser serializado.</param>
    /// <returns>Representação em string XML do objeto.</returns>
    public static string ObjectToString<T>(this T obj)
    {
        if (obj is null)
            throw new ArgumentException("Não é possível converter um objeto nulo para uma variável de texto.", nameof(obj));

        var xml = new XmlSerializer(obj.GetType());

        using var text = new StringWriter();
        xml.Serialize(text, obj);

        return text.ToString();
    }

    /// <summary>
    /// Desserializa uma string em formato XML para um objeto tipado.
    /// </summary>
    /// <typeparam name="T">Tipo do objeto de destino.</typeparam>
    /// <param name="input">String contendo o XML válido.</param>
    /// <returns>Instância do objeto desserializado.</returns>
    public static T? StringToObject<T>(this string input) where T : new()
    {
        if (string.IsNullOrWhiteSpace(input))
            throw new ArgumentException("Informe uma variável que não seja vazia ou nula para converter a um objeto.", nameof(input));

        var xml = new XmlSerializer(typeof(T));

        using var reader = new StringReader(input);
        return (T?)xml.Deserialize(reader);
    }

    /// <summary>
    /// Converte uma <see cref="List{T}"/> preenchida em um <see cref="DataTable"/>.
    /// </summary>
    public static DataTable ListToDataTable<T>(this List<T> list) => ListDataTable(list);

    /// <summary>
    /// Converte uma <see cref="IList{T}"/> preenchida em um <see cref="DataTable"/>.
    /// </summary>
    public static DataTable ListToDataTable<T>(this IList<T> list) => ListDataTable(list);

    #endregion
}