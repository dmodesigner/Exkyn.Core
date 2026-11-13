using Exkyn.Core.Extensions;

namespace Exkyn.Core.Tests.Extensions;

public class NoFormattingExtensionTests
{
    [Theory]
    [InlineData("123.456.789-01", "12345678901")] // CPF
    [InlineData("12.345.678/0001-99", "12345678000199")] // CNPJ
    [InlineData("04267-000", "04267000")] // CEP
    [InlineData("(11) 98765-4321", "11987654321")] // Telefone
    [InlineData("  ABC-1D23  ", "ABC1D23")] // Placa com espaços
    [InlineData("Exkyn_Core@2026!", "ExkynCore2026")] // Alfanumérico misto
    public void NoFormatting_ShouldRemoveSpecialCharactersAndSpaces_WhenValidStringIsProvided(
        string input,
        string expected)
    {
        // Act
        var result = input.NoFormatting();

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData("\t\r\n")]
    public void NoFormatting_ShouldReturnNull_WhenInputIsNullOrWhiteSpace(string? invalidInput)
    {
        // Act
        var result = invalidInput.NoFormatting();

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void NoFormatting_ShouldReturnEmptyString_WhenInputContainsOnlySpecialCharacters()
    {
        // Arrange
        var input = ".-/()@#$%*!";

        // Act
        var result = input.NoFormatting();

        // Assert
        Assert.Equal(string.Empty, result);
    }
}