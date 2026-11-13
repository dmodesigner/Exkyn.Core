using Exkyn.Core.Extensions;
using Xunit;

namespace Exkyn.Core.Tests.Extensions;

public class UpperFirstLetterExtensionTests
{
    [Theory]
    [InlineData("daniel", "Daniel")]
    [InlineData("exkyn core", "Exkyn core")]
    [InlineData("   espaço nas bordas  ", "Espaço nas bordas")]
    [InlineData("a", "A")] // Caso de borda: 1 único caractere
    [InlineData("água", "Água")] // Caractere acentuado
    [InlineData("Já Maiúsculo", "Já Maiúsculo")]
    [InlineData("123teste", "123teste")] // Começando com número
    public void UpperFirstLetter_ShouldCapitalizeFirstCharacterAndTrim_WhenValidStringIsProvided(
        string input,
        string expected)
    {
        // Act
        var result = input.UpperFirstLetter();

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData("\t\r\n")]
    public void UpperFirstLetter_ShouldReturnNull_WhenInputIsNullOrWhiteSpace(string? invalidInput)
    {
        // Act
        var result = invalidInput.UpperFirstLetter();

        // Assert
        Assert.Null(result);
    }
}