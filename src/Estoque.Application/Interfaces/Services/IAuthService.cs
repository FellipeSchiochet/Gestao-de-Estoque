using Estoque.Application.DTOs.Auth;

namespace Estoque.Application.Interfaces.Services;

public interface IAuthService
{
    Task<AuthResponse> RegistrarAsync(RegisterRequest request, CancellationToken cancellationToken = default);
    Task<AuthResponse> LoginAsync(LoginRequest request, CancellationToken cancellationToken = default);
}
