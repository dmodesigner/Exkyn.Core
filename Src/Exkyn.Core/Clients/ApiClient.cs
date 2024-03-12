using System.Net.Http.Json;

namespace Exkyn.Core.Clients;

public class ApiClient<TSaida> : BaseApiClient where TSaida : class
{
    public async Task<TSaida?> GetAsync(string url)
    {
        SetUrl(url);

        var response = await _httClient.GetAsync(Url);

        if (response.IsSuccessStatusCode)
            return await response.Content.ReadFromJsonAsync<TSaida>();

        return null;
    }

    public async Task<List<TSaida>?> GetListAsync(string url)
    {
        SetUrl(url);

        var response = await _httClient.GetAsync(Url);

        if (response.IsSuccessStatusCode)
            return await response.Content.ReadFromJsonAsync<List<TSaida>>();

        return null;
    }
}

public class ApiClient<TEntrada, TSaida> : BaseApiClient where TSaida : class
{
    public async Task<TSaida?> PostAsync(string url, TEntrada obj)
    {
        SetUrl(url);

        var response = await _httClient.PostAsJsonAsync(Url, obj);

        if (response.IsSuccessStatusCode)
            return await response.Content.ReadFromJsonAsync<TSaida>();

        return null;
    }

    public async Task<List<TSaida>?> PostListAsync(string url, TEntrada obj)
    {
        SetUrl(url);

        var response = await _httClient.PostAsJsonAsync(Url, obj);

        if (response.IsSuccessStatusCode)
            return await response.Content.ReadFromJsonAsync<List<TSaida>>();

        return null;
    }
}

