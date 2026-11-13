using System.Net.Http.Headers;

namespace Exkyn.Core.Clients;

public abstract class BaseApiClient
{
    protected readonly HttpClient _httClient;

    protected string Url { get; private set; } = string.Empty;

    protected BaseApiClient(HttpClient httpClient)
    {
        ArgumentNullException.ThrowIfNull(httpClient);

        _httClient = httpClient;

        if (!_httClient.DefaultRequestHeaders.Accept.Any(h => h.MediaType == "application/json"))
        {
            _httClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }

    protected void SetUrl(string url)
    {
        if (string.IsNullOrWhiteSpace(url))
            throw new ArgumentException("A URL não pode ser nula ou vazia.", nameof(url));

        Url = url.Trim();
    }
}