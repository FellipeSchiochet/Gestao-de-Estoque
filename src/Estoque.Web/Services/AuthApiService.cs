using System.Net.Http.Json;
using Estoque.Web.Models;

namespace Estoque.Web.Services;

public class AuthApiService : IAuthApiService
{
    private readonly HttpClient _httpClient;

    public AuthApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<AuthResponseModel> LoginAsync(LoginFormModel login, CancellationToken cancellationToken = default)
    {
        using var response = await _httpClient.PostAsJsonAsync("api/auth/login", login, cancellationToken);
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<AuthResponseModel>(cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("A API nao retornou os dados de autenticacao.");
    }

    public async Task<AuthResponseModel> RegisterAsync(RegisterFormModel register, CancellationToken cancellationToken = default)
    {
        var payload = new
        {
            register.Nome,
            register.Email,
            register.Senha
        };

        using var response = await _httpClient.PostAsJsonAsync("api/auth/register", payload, cancellationToken);
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<AuthResponseModel>(cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("A API nao retornou os dados do cadastro.");
    }
}
