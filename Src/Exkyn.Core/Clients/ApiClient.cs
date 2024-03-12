using System.Net.Http.Json;

namespace Exkyn.Core.Clients;

public class ApiClient<TSaida> : BaseApiClient where TSaida : class
{
    /// <summary>
    /// Consome a API usando o método GET
    /// </summary>
    /// <param name="url">Recebe uma string</param>
    /// <returns>Retorna uma tarefa de objeto</returns>
    public async Task<TSaida?> GetAsync(string url)
    {
        SetUrl(url);

        var response = await _httClient.GetAsync(Url);

        if (response.IsSuccessStatusCode)
            return await response.Content.ReadFromJsonAsync<TSaida>();

        return null;
    }

    /// <summary>
    /// Consome a API usando o método GET
    /// </summary>
    /// <param name="url">Recebe uma string</param>
    /// <returns>Retorna uma tarefa de lista de objeto</returns>
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
    /// <summary>
    /// Consome a API usando o método POST
    /// </summary>
    /// <param name="url">Recebe uma string</param>
    /// <param name="obj">Recebe um objeto</param>
    /// <returns>Retorna uma tarefa de objeto</returns>
    public async Task<TSaida?> PostAsync(string url, TEntrada obj)
    {
        SetUrl(url);

        var response = await _httClient.PostAsJsonAsync(Url, obj);

        if (response.IsSuccessStatusCode)
            return await response.Content.ReadFromJsonAsync<TSaida>();

        return null;
    }

    /// <summary>
    /// Consome a API usando o método POST
    /// </summary>
    /// <param name="url">Recebe uma string</param>
    /// <param name="obj">Recebe um objeto</param>
    /// <returns>Retorna uma tarefa de lista de objeto</returns>
    public async Task<List<TSaida>?> PostListAsync(string url, TEntrada obj)
    {
        SetUrl(url);

        var response = await _httClient.PostAsJsonAsync(Url, obj);

        if (response.IsSuccessStatusCode)
            return await response.Content.ReadFromJsonAsync<List<TSaida>>();

        return null;
    }
}

