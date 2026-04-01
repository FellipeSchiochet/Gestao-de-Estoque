using System.Net.Http.Headers;
using System.Net.Http.Json;
using Estoque.Web.Models;

namespace Estoque.Web.Services;

public class MovimentacaoApiService : IMovimentacaoApiService
{
    private readonly HttpClient _httpClient;

    public MovimentacaoApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<MovimentacaoViewModel> RegistrarAsync(string? token, MovimentacaoFormModel movimentacao, CancellationToken cancellationToken = default)
    {
        using var request = new HttpRequestMessage(HttpMethod.Post, "api/movimentacoesestoque")
        {
            Content = JsonContent.Create(movimentacao)
        };
        AdicionarToken(request, token);

        using var response = await _httpClient.SendAsync(request, cancellationToken);
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<MovimentacaoViewModel>(cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("A API não retornou a movimentação registrada.");
    }

    public async Task<IReadOnlyList<MovimentacaoViewModel>> ObterTodosAsync(string? token, CancellationToken cancellationToken = default)
    {
        using var request = new HttpRequestMessage(HttpMethod.Get, "api/movimentacoesestoque");
        AdicionarToken(request, token);

        using var response = await _httpClient.SendAsync(request, cancellationToken);
        response.EnsureSuccessStatusCode();

        var historico = await response.Content.ReadFromJsonAsync<List<MovimentacaoViewModel>>(cancellationToken: cancellationToken);
        return historico ?? [];
    }

    public async Task<IReadOnlyList<MovimentacaoViewModel>> ObterHistoricoAsync(string? token, int produtoId, CancellationToken cancellationToken = default)
    {
        using var request = new HttpRequestMessage(HttpMethod.Get, $"api/movimentacoesestoque/produto/{produtoId}");
        AdicionarToken(request, token);

        using var response = await _httpClient.SendAsync(request, cancellationToken);
        response.EnsureSuccessStatusCode();

        var historico = await response.Content.ReadFromJsonAsync<List<MovimentacaoViewModel>>(cancellationToken: cancellationToken);
        return historico ?? [];
    }

    private static void AdicionarToken(HttpRequestMessage request, string? token)
    {
        if (!string.IsNullOrWhiteSpace(token))
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
    }
}
