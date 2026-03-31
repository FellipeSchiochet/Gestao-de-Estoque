using Microsoft.JSInterop;

namespace Estoque.Web.Services;

public class TokenStorageService : ITokenStorageService
{
    private const string TokenKey = "authToken";
    private readonly IJSRuntime _jsRuntime;

    public TokenStorageService(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }

    public async Task<string?> ObterTokenAsync()
    {
        return await _jsRuntime.InvokeAsync<string?>("codexAuth.getToken", TokenKey);
    }

    public async Task SalvarTokenAsync(string token)
    {
        await _jsRuntime.InvokeVoidAsync("codexAuth.saveToken", TokenKey, token);
    }

    public async Task RemoverTokenAsync()
    {
        await _jsRuntime.InvokeVoidAsync("codexAuth.removeToken", TokenKey);
    }

    public async Task EncerrarSessaoAsync(string redirectUrl = "/")
    {
        await _jsRuntime.InvokeVoidAsync("codexAuth.clearSessionAndRedirect", TokenKey, redirectUrl);
    }
}
