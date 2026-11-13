using Exkyn.Core.Clients;

namespace Exkyn.Core.Tests.Clients;

public class BaseApiClientTests
{
    private class TestableBaseApiClient(HttpClient httpClient) : BaseApiClient(httpClient)
    {
        public string ExposedUrl => Url;
        public void CallSetUrl(string url) => SetUrl(url);
    }

    [Fact]
    public void Constructor_ShouldThrowArgumentNullException_WhenHttpClientIsNull()
    {
        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => new TestableBaseApiClient(null!));
    }

    [Fact]
    public void Constructor_ShouldAddAcceptApplicationJsonHeader()
    {
        // Arrange
        using var httpClient = new HttpClient();

        // Act
        _ = new TestableBaseApiClient(httpClient);

        // Assert
        Assert.Contains(
            httpClient.DefaultRequestHeaders.Accept,
            h => h.MediaType == "application/json");
    }

    [Fact]
    public void SetUrl_ShouldTrimAndAssignUrl_WhenValidUrlIsProvided()
    {
        // Arrange
        using var httpClient = new HttpClient();
        var sut = new TestableBaseApiClient(httpClient);

        // Act
        sut.CallSetUrl("  https://api.exkyn.com/v1/resource  ");

        // Assert
        Assert.Equal("https://api.exkyn.com/v1/resource", sut.ExposedUrl);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void SetUrl_ShouldThrowArgumentException_WhenUrlIsNullOrWhiteSpace(string? invalidUrl)
    {
        // Arrange
        using var httpClient = new HttpClient();
        var sut = new TestableBaseApiClient(httpClient);

        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() => sut.CallSetUrl(invalidUrl!));
        Assert.Contains("A URL não pode ser nula ou vazia.", exception.Message);
    }
}