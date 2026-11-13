using System.Globalization;
using Exkyn.Core.Extensions;

namespace Exkyn.Core.Tests.Extensions;

public class MoneyExtensionTests
{
    private static string NormalizeSpaces(string input) => input.Replace('\u00A0', ' ');

    [Fact]
    public void Money_Decimal_DefaultCulture_ShouldFormatAsBrazilianCurrency()
    {
        // Arrange
        decimal value = 1234.56m;

        // Act
        var result = NormalizeSpaces(value.Money());

        // Assert
        Assert.Equal("R$ 1.234,56", result);
    }

    [Fact]
    public void Money_Decimal_WithCustomCulture_ShouldFormatAsUsDollar()
    {
        // Arrange
        decimal value = 1234.56m;

        // Act
        var result = NormalizeSpaces(value.Money("en-US"));

        // Assert
        Assert.Equal("$1,234.56", result);
    }

    [Fact]
    public void Money_Double_DefaultCulture_ShouldFormatAsBrazilianCurrency()
    {
        // Arrange
        double value = 9876.54;

        // Act
        var result = NormalizeSpaces(value.Money());

        // Assert
        Assert.Equal("R$ 9.876,54", result);
    }

    [Fact]
    public void Money_Double_WithCustomCulture_ShouldFormatAsUsDollar()
    {
        // Arrange
        double value = 9876.54;

        // Act
        var result = NormalizeSpaces(value.Money("en-US"));

        // Assert
        Assert.Equal("$9,876.54", result);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Money_Decimal_ShouldThrowArgumentException_WhenCultureIsNullOrWhiteSpace(string? invalidCulture)
    {
        // Arrange
        decimal value = 100m;

        // Act & Assert
        Assert.Throws<ArgumentException>(() => value.Money(invalidCulture!));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Money_Double_ShouldThrowArgumentException_WhenCultureIsNullOrWhiteSpace(string? invalidCulture)
    {
        // Arrange
        double value = 100.0;

        // Act & Assert
        Assert.Throws<ArgumentException>(() => value.Money(invalidCulture!));
    }

    [Fact]
    public void Money_ShouldThrowCultureNotFoundException_WhenCultureCodeIsInvalid()
    {
        // Arrange
        decimal value = 50m;

        // Act & Assert
        Assert.Throws<CultureNotFoundException>(() => value.Money("cultura-invalida-xyz"));
    }
}