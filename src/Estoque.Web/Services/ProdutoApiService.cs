using System.Net.Http.Headers;
using System.Net.Http.Json;
using Estoque.Web.Models;

namespace Estoque.Web.Services;

public class ProdutoApiService : IProdutoApiService
{
    private readonly HttpClient _httpClient;

    public ProdutoApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IReadOnlyList<ProdutoViewModel>> ObterTodosAsync(string? token, CancellationToken cancellationToken = default)
    {
        using var request = new HttpRequestMessage(HttpMethod.Get, "api/produtos");
        AdicionarToken(request, token);

        using var response = await _httpClient.SendAsync(request, cancellationToken);
        response.EnsureSuccessStatusCode();

        var produtos = await response.Content.ReadFromJsonAsync<List<ProdutoViewModel>>(cancellationToken: cancellationToken);

        return produtos ?? [];
    }

    public async Task<ProdutoViewModel?> ObterPorIdAsync(string? token, int id, CancellationToken cancellationToken = default)
    {
        using var request = new HttpRequestMessage(HttpMethod.Get, $"api/produtos/{id}");
        AdicionarToken(request, token);

        using var response = await _httpClient.SendAsync(request, cancellationToken);

        if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return null;
        }

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<ProdutoViewModel>(cancellationToken: cancellationToken);
    }

    public async Task<ProdutoViewModel> CriarAsync(string? token, ProdutoFormModel produto, CancellationToken cancellationToken = default)
    {
        using var request = new HttpRequestMessage(HttpMethod.Post, "api/produtos")
        {
            Content = JsonContent.Create(produto)
        };
        AdicionarToken(request, token);

        using var response = await _httpClient.SendAsync(request, cancellationToken);
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<ProdutoViewModel>(cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("A API não retornou o produto criado.");
    }

    public async Task<ProdutoViewModel> AtualizarAsync(string? token, ProdutoFormModel produto, CancellationToken cancellationToken = default)
    {
        if (produto.Id is null)
        {
            throw new InvalidOperationException("O id do produto é obrigatório para atualização.");
        }

        using var request = new HttpRequestMessage(HttpMethod.Put, $"api/produtos/{produto.Id}")
        {
            Content = JsonContent.Create(produto)
        };
        AdicionarToken(request, token);

        using var response = await _httpClient.SendAsync(request, cancellationToken);
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<ProdutoViewModel>(cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("A API não retornou o produto atualizado.");
    }

    public async Task RemoverAsync(string? token, int id, CancellationToken cancellationToken = default)
    {
        using var request = new HttpRequestMessage(HttpMethod.Delete, $"api/produtos/{id}");
        AdicionarToken(request, token);

        using var response = await _httpClient.SendAsync(request, cancellationToken);
        response.EnsureSuccessStatusCode();
    }

    private static void AdicionarToken(HttpRequestMessage request, string? token)
    {
        if (!string.IsNullOrWhiteSpace(token))
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
    }
}
