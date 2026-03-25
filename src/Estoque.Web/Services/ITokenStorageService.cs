namespace Estoque.Web.Services;

public interface ITokenStorageService
{
    Task<string?> ObterTokenAsync();
    Task SalvarTokenAsync(string token);
    Task RemoverTokenAsync();
}
