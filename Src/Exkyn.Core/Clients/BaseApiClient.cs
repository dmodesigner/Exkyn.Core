using System.Net.Http.Headers;

namespace Exkyn.Core.Clients;

public abstract class BaseApiClient
{
    protected readonly HttpClient _httClient;

    protected string Url { get; private set; } = string.Empty;

    protected BaseApiClient()
    {
        _httClient = new HttpClient();

        _httClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }

    protected void SetUrl(string url)
    {
        if (string.IsNullOrWhiteSpace(url))
            throw new ArgumentException("A URL não pode ser nula ou vazia.");

        Url = url.Trim();
    }
}
