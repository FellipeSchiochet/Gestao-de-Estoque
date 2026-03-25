using Microsoft.JSInterop;

namespace Estoque.Web.Services;

public class TokenStorageService : ITokenStorageService
{
    private readonly IJSRuntime _jsRuntime;

    public TokenStorageService(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }

    public async Task<string?> ObterTokenAsync()
    {
        return await _jsRuntime.InvokeAsync<string?>("localStorage.getItem", "authToken");
    }

    public async Task SalvarTokenAsync(string token)
    {
        await _jsRuntime.InvokeVoidAsync("localStorage.setItem", "authToken", token);
    }

    public async Task RemoverTokenAsync()
    {
        await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", "authToken");
    }
}
