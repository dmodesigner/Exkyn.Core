using Exkyn.Core.Helpers;

namespace Exkyn.Core.Tests.Helpers;

public class EncryptHelperTests
{
    // Chave de 32 bytes (256 bits) e IV de 16 bytes (128 bits) válidos em Base64 para testes
    private static readonly string ValidBase64Key = Convert.ToBase64String(new byte[32]
    {
        1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16,
        17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32
    });

    private static readonly string ValidBase64Vector = Convert.ToBase64String(new byte[16]
    {
        101, 102, 103, 104, 105, 106, 107, 108, 109, 110, 111, 112, 113, 114, 115, 116
    });

    #region SHA Tests

    [Fact]
    public void SHA_ShouldReturnExpectedHexHash_WhenInputIsValid()
    {
        // Arrange: Vetor de teste padrão oficial do NIST para SHA-256 ("abc")
        var input = "abc";
        var expected = "BA7816BF8F01CFEA414140DE5DAE2223B00361A396177A9CB410FF61F20015AD";

        // Act
        var result = EncryptHelper.SHA(input);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(64, result.Length);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void SHA_ShouldThrowArgumentException_WhenInputIsNullOrWhiteSpace(string? invalidInput)
    {
        // Act & Assert
        var ex = Assert.Throws<ArgumentException>(() => EncryptHelper.SHA(invalidInput!));
        Assert.Contains("Não é possível criptografar / descriptografar uma variável vazia ou nula.", ex.Message);
    }

    #endregion

    #region AES With Explicit Key and Vector Tests

    [Theory]
    [InlineData("SenhaSuperSecreta123!")]
    [InlineData("Texto com acentuação: São Paulo, Conceição, R$ 1.234,56")]
    public void AesEncryptor_And_AesDecryptor_ShouldRoundTripSuccessfully(string originalText)
    {
        // Act
        var encrypted = EncryptHelper.AesEncryptor(originalText, ValidBase64Key, ValidBase64Vector);
        var decrypted = EncryptHelper.AesDecryptor(encrypted, ValidBase64Key, ValidBase64Vector);

        // Assert
        Assert.False(string.IsNullOrWhiteSpace(encrypted));
        Assert.NotEqual(originalText, encrypted);
        Assert.Equal(originalText, decrypted);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void AesEncryptor_ShouldThrowArgumentException_WhenInputIsInvalid(string? invalidInput)
    {
        Assert.Throws<ArgumentException>(() =>
            EncryptHelper.AesEncryptor(invalidInput!, ValidBase64Key, ValidBase64Vector));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void AesDecryptor_ShouldThrowArgumentException_WhenInputIsInvalid(string? invalidInput)
    {
        Assert.Throws<ArgumentException>(() =>
            EncryptHelper.AesDecryptor(invalidInput!, ValidBase64Key, ValidBase64Vector));
    }

    [Theory]
    [InlineData(null, "validVector")]
    [InlineData("", "validVector")]
    [InlineData("validKey", null)]
    [InlineData("validKey", "")]
    public void AesEncryptor_ShouldThrowArgumentException_WhenKeyOrVectorIsMissing(string? key, string? vector)
    {
        Assert.Throws<ArgumentException>(() =>
            EncryptHelper.AesEncryptor("Texto", key!, vector!));
    }

    #endregion

    #region AES With Environment Variables Tests

    [Fact]
    public void AesEncryptor_And_AesDecryptor_UsingEnvironmentVariables_ShouldRoundTripSuccessfully()
    {
        // Arrange
        var originalText = "Teste Variavel de Ambiente";
        Environment.SetEnvironmentVariable("Base64Key", ValidBase64Key);
        Environment.SetEnvironmentVariable("Base64Vector", ValidBase64Vector);

        try
        {
            // Act
            var encrypted = EncryptHelper.AesEncryptor(originalText);
            var decrypted = EncryptHelper.AesDecryptor(encrypted);

            // Assert
            Assert.Equal(originalText, decrypted);
        }
        finally
        {
            // Cleanup para não afetar outros testes
            Environment.SetEnvironmentVariable("Base64Key", null);
            Environment.SetEnvironmentVariable("Base64Vector", null);
        }
    }

    [Fact]
    public void AesEncryptor_WithoutEnvironmentVariables_ShouldThrowArgumentException()
    {
        // Arrange
        Environment.SetEnvironmentVariable("Base64Key", null);
        Environment.SetEnvironmentVariable("Base64Vector", null);

        // Act & Assert
        Assert.Throws<ArgumentException>(() => EncryptHelper.AesEncryptor("Texto"));
    }

    #endregion
}