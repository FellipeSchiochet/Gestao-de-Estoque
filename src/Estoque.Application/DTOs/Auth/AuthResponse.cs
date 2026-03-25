namespace Estoque.Application.DTOs.Auth;

public class AuthResponse
{
    public string Token { get; set; } = string.Empty;
    public DateTime Expiracao { get; set; }
    public UsuarioResponse Usuario { get; set; } = new();
}
