using System.Net;
using System.Text;
using System.Text.Json;
using Exkyn.Core.Clients;

namespace Exkyn.Core.Tests.Clients;

public class ApiClientTests
{
    private record DummyRequest(string Name);
    private record DummyResponse(int Id, string Name);

    private class FakeHttpMessageHandler(Func<HttpRequestMessage, HttpResponseMessage> responder) : HttpMessageHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
            => Task.FromResult(responder(request));
    }

    private static HttpClient CreateMockHttpClient(HttpStatusCode statusCode, object? responseBody = null)
    {
        var handler = new FakeHttpMessageHandler(_ =>
        {
            var response = new HttpResponseMessage(statusCode);
            if (responseBody is not null)
            {
                var json = JsonSerializer.Serialize(responseBody);
                response.Content = new StringContent(json, Encoding.UTF8, "application/json");
            }
            return response;
        });

        return new HttpClient(handler);
    }

    [Fact]
    public async Task GetAsync_ShouldReturnObject_WhenResponseIsSuccess()
    {
        // Arrange
        var expected = new DummyResponse(1, "Exkyn");
        using var httpClient = CreateMockHttpClient(HttpStatusCode.OK, expected);
        var sut = new ApiClient<DummyResponse>(httpClient);

        // Act
        var result = await sut.GetAsync("https://api.exkyn.com/items/1");

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expected.Id, result.Id);
        Assert.Equal(expected.Name, result.Name);
    }

    [Theory]
    [InlineData(HttpStatusCode.BadRequest)]
    [InlineData(HttpStatusCode.NotFound)]
    [InlineData(HttpStatusCode.InternalServerError)]
    [InlineData(HttpStatusCode.NoContent)]
    public async Task GetAsync_ShouldReturnNull_WhenResponseIsNotSuccessOrNoContent(HttpStatusCode statusCode)
    {
        // Arrange
        using var httpClient = CreateMockHttpClient(statusCode);
        var sut = new ApiClient<DummyResponse>(httpClient);

        // Act
        var result = await sut.GetAsync("https://api.exkyn.com/items/1");

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task GetListAsync_ShouldReturnList_WhenResponseIsSuccess()
    {
        // Arrange
        var expectedList = new List<DummyResponse>
        {
            new(1, "Item 1"),
            new(2, "Item 2")
        };
        using var httpClient = CreateMockHttpClient(HttpStatusCode.OK, expectedList);
        var sut = new ApiClient<DummyResponse>(httpClient);

        // Act
        var result = await sut.GetListAsync("https://api.exkyn.com/items");

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count);
        Assert.Equal("Item 1", result[0].Name);
    }

    [Fact]
    public async Task GetListAsync_ShouldReturnNull_WhenResponseFails()
    {
        // Arrange
        using var httpClient = CreateMockHttpClient(HttpStatusCode.InternalServerError);
        var sut = new ApiClient<DummyResponse>(httpClient);

        // Act
        var result = await sut.GetListAsync("https://api.exkyn.com/items");

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task PostAsync_ShouldReturnObject_WhenResponseIsSuccess()
    {
        // Arrange
        var requestPayload = new DummyRequest("Novo Item");
        var expectedResponse = new DummyResponse(10, "Novo Item");
        using var httpClient = CreateMockHttpClient(HttpStatusCode.Created, expectedResponse);
        var sut = new ApiClient<DummyRequest, DummyResponse>(httpClient);

        // Act
        var result = await sut.PostAsync("https://api.exkyn.com/items", requestPayload);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(10, result.Id);
        Assert.Equal("Novo Item", result.Name);
    }

    [Fact]
    public async Task PostAsync_ShouldReturnNull_WhenResponseFails()
    {
        // Arrange
        var requestPayload = new DummyRequest("Novo Item");
        using var httpClient = CreateMockHttpClient(HttpStatusCode.BadRequest);
        var sut = new ApiClient<DummyRequest, DummyResponse>(httpClient);

        // Act
        var result = await sut.PostAsync("https://api.exkyn.com/items", requestPayload);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task PostListAsync_ShouldReturnList_WhenResponseIsSuccess()
    {
        // Arrange
        var requestPayload = new DummyRequest("Filtro");
        var expectedList = new List<DummyResponse> { new(1, "Resultado 1") };
        using var httpClient = CreateMockHttpClient(HttpStatusCode.OK, expectedList);
        var sut = new ApiClient<DummyRequest, DummyResponse>(httpClient);

        // Act
        var result = await sut.PostListAsync("https://api.exkyn.com/items/search", requestPayload);

        // Assert
        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Equal("Resultado 1", result[0].Name);
    }

    [Fact]
    public async Task PostListAsync_ShouldReturnNull_WhenResponseFails()
    {
        // Arrange
        var requestPayload = new DummyRequest("Filtro");
        using var httpClient = CreateMockHttpClient(HttpStatusCode.Unauthorized);
        var sut = new ApiClient<DummyRequest, DummyResponse>(httpClient);

        // Act
        var result = await sut.PostListAsync("https://api.exkyn.com/items/search", requestPayload);

        // Assert
        Assert.Null(result);
    }
}