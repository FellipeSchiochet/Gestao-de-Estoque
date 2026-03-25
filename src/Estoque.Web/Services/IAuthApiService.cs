using Estoque.Web.Models;

namespace Estoque.Web.Services;

public interface IAuthApiService
{
    Task<AuthResponseModel> LoginAsync(LoginFormModel login, CancellationToken cancellationToken = default);
    Task<AuthResponseModel> RegisterAsync(RegisterFormModel register, CancellationToken cancellationToken = default);
}
