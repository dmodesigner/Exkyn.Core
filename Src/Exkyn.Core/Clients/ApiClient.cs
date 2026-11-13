using System.Net;

namespace Exkyn.Core.Clients;

/// <summary>
/// Cliente HTTP genérico para requisições GET com retorno tipado.
/// </summary>
/// <typeparam name="TSaida">Tipo do objeto de saída esperado.</typeparam>
public class ApiClient<TSaida>(HttpClient httpClient) : BaseApiClient(httpClient) where TSaida : class
{
    /// <summary>
    /// Consome a API usando o método GET para retornar um único objeto.
    /// </summary>
    /// <param name="url">Endpoint ou URL completa da requisição.</param>
    /// <param name="cancellationToken">Token de cancelamento da operação assíncrona.</param>
    /// <returns>O objeto desserializado ou <c>null</c> em caso de falha/sem conteúdo.</returns>
    public async Task<TSaida?> GetAsync(string url, CancellationToken cancellationToken = default)
    {
        SetUrl(url);

        using var response = await _httClient.GetAsync(Url, cancellationToken).ConfigureAwait(false);

        if (!response.IsSuccessStatusCode || response.StatusCode == HttpStatusCode.NoContent)
            return null;

        return await response.Content.ReadFromJsonAsync<TSaida>(cancellationToken: cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Consome a API usando o método GET para retornar uma lista de objetos.
    /// </summary>
    /// <param name="url">Endpoint ou URL completa da requisição.</param>
    /// <param name="cancellationToken">Token de cancelamento da operação assíncrona.</param>
    /// <returns>A lista de objetos desserializada ou <c>null</c> em caso de falha/sem conteúdo.</returns>
    public async Task<List<TSaida>?> GetListAsync(string url, CancellationToken cancellationToken = default)
    {
        SetUrl(url);

        using var response = await _httClient.GetAsync(Url, cancellationToken).ConfigureAwait(false);

        if (!response.IsSuccessStatusCode || response.StatusCode == HttpStatusCode.NoContent)
            return null;

        return await response.Content.ReadFromJsonAsync<List<TSaida>>(cancellationToken: cancellationToken).ConfigureAwait(false);
    }
}

/// <summary>
/// Cliente HTTP genérico para requisições POST com envio e retorno tipados.
/// </summary>
/// <typeparam name="TEntrada">Tipo do payload enviado no corpo da requisição.</typeparam>
/// <typeparam name="TSaida">Tipo do objeto de saída esperado.</typeparam>
public class ApiClient<TEntrada, TSaida>(HttpClient httpClient) : BaseApiClient(httpClient) where TSaida : class
{

    /// <summary>
    /// Consome a API usando o método POST e retorna um único objeto.
    /// </summary>
    /// <param name="url">Endpoint ou URL completa da requisição.</param>
    /// <param name="obj">Objeto a ser serializado em JSON no corpo da requisição.</param>
    /// <param name="cancellationToken">Token de cancelamento da operação assíncrona.</param>
    /// <returns>O objeto desserializado ou <c>null</c> em caso de falha/sem conteúdo.</returns>
    public async Task<TSaida?> PostAsync(string url, TEntrada obj, CancellationToken cancellationToken = default)
    {
        SetUrl(url);

        using var response = await _httClient.PostAsJsonAsync(Url, obj, cancellationToken).ConfigureAwait(false);

        if (!response.IsSuccessStatusCode || response.StatusCode == HttpStatusCode.NoContent)
            return null;

        return await response.Content.ReadFromJsonAsync<TSaida>(cancellationToken: cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Consome a API usando o método POST e retorna uma lista de objetos.
    /// </summary>
    /// <param name="url">Endpoint ou URL completa da requisição.</param>
    /// <param name="obj">Objeto a ser serializado em JSON no corpo da requisição.</param>
    /// <param name="cancellationToken">Token de cancelamento da operação assíncrona.</param>
    /// <returns>A lista de objetos desserializada ou <c>null</c> em caso de falha/sem conteúdo.</returns>
    public async Task<List<TSaida>?> PostListAsync(string url, TEntrada obj, CancellationToken cancellationToken = default)
    {
        SetUrl(url);

        using var response = await _httClient.PostAsJsonAsync(Url, obj, cancellationToken).ConfigureAwait(false);

        if (!response.IsSuccessStatusCode || response.StatusCode == HttpStatusCode.NoContent)
            return null;

        return await response.Content.ReadFromJsonAsync<List<TSaida>>(cancellationToken: cancellationToken).ConfigureAwait(false);
    }
}