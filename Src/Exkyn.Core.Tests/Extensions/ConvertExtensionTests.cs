using System.Data;
using Exkyn.Core.Extensions;
using Xunit;

namespace Exkyn.Core.Tests.Extensions;

public class ConvertExtensionTests
{
    public class SampleModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal? Price { get; set; }
    }

    [Fact]
    public void ObjectToString_ShouldSerializeObjectToXml_WhenObjectIsValid()
    {
        // Arrange
        var model = new SampleModel { Id = 10, Name = "Exkyn Core", Price = 99.90m };

        // Act
        var xml = model.ObjectToString();

        // Assert
        Assert.False(string.IsNullOrWhiteSpace(xml));
        Assert.Contains("<SampleModel", xml);
        Assert.Contains("<Id>10</Id>", xml);
        Assert.Contains("<Name>Exkyn Core</Name>", xml);
        Assert.Contains("<Price>99.90</Price>", xml);
    }

    [Fact]
    public void ObjectToString_ShouldThrowArgumentException_WhenObjectIsNull()
    {
        // Arrange
        SampleModel? model = null;

        // Act & Assert
        var ex = Assert.Throws<ArgumentException>(() => model.ObjectToString());
        Assert.Contains("Não é possível converter um objeto nulo para uma variável de texto.", ex.Message);
    }

    [Fact]
    public void StringToObject_ShouldDeserializeXmlToObject_WhenXmlIsValid()
    {
        // Arrange
        var original = new SampleModel { Id = 42, Name = "Teste XML", Price = 15.5m };
        var xml = original.ObjectToString();

        // Act
        var result = xml.StringToObject<SampleModel>();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(original.Id, result.Id);
        Assert.Equal(original.Name, result.Name);
        Assert.Equal(original.Price, result.Price);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void StringToObject_ShouldThrowArgumentException_WhenInputIsNullOrWhiteSpace(string? invalidInput)
    {
        // Act & Assert
        var ex = Assert.Throws<ArgumentException>(() => invalidInput!.StringToObject<SampleModel>());
        Assert.Contains("Informe uma variável que não seja vazia ou nula para converter a um objeto.", ex.Message);
    }

    [Fact]
    public void ListToDataTable_FromList_ShouldCreateColumnsAndRowsCorrectly_IncludingNullableProperties()
    {
        // Arrange
        var list = new List<SampleModel>
        {
            new() { Id = 1, Name = "Item A", Price = 10.5m },
            new() { Id = 2, Name = "Item B", Price = null }
        };

        // Act
        DataTable table = list.ListToDataTable();

        // Assert
        Assert.NotNull(table);
        Assert.Equal(3, table.Columns.Count);
        Assert.Equal(typeof(int), table.Columns["Id"]!.DataType);
        Assert.Equal(typeof(string), table.Columns["Name"]!.DataType);
        Assert.Equal(typeof(decimal), table.Columns["Price"]!.DataType); // Underlying type de decimal?

        Assert.Equal(2, table.Rows.Count);
        Assert.Equal(1, table.Rows[0]["Id"]);
        Assert.Equal("Item A", table.Rows[0]["Name"]);
        Assert.Equal(10.5m, table.Rows[0]["Price"]);

        Assert.Equal(2, table.Rows[1]["Id"]);
        Assert.Equal("Item B", table.Rows[1]["Name"]);
        Assert.Equal(DBNull.Value, table.Rows[1]["Price"]);
    }

    [Fact]
    public void ListToDataTable_FromIList_ShouldConvertSuccessfully()
    {
        // Arrange
        IList<SampleModel> list = new List<SampleModel>
        {
            new() { Id = 99, Name = "Interface List", Price = 1.99m }
        };

        // Act
        DataTable table = list.ListToDataTable();

        // Assert
        Assert.Single(table.Rows);
        Assert.Equal(99, table.Rows[0]["Id"]);
    }

    [Fact]
    public void ListToDataTable_ShouldThrowArgumentException_WhenListIsNull()
    {
        // Arrange
        List<SampleModel>? nullList = null;

        // Act & Assert
        var ex = Assert.Throws<ArgumentException>(() => nullList!.ListToDataTable());
        Assert.Contains("Informe uma lista com conteúdo para converter a um DataTable.", ex.Message);
    }

    [Fact]
    public void ListToDataTable_ShouldThrowArgumentException_WhenListIsEmpty()
    {
        // Arrange
        var emptyList = new List<SampleModel>();

        // Act & Assert
        var ex = Assert.Throws<ArgumentException>(() => emptyList.ListToDataTable());
        Assert.Contains("Informe uma lista com conteúdo para converter a um DataTable.", ex.Message);
    }
}