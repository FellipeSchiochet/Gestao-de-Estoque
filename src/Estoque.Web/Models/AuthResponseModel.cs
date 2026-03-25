namespace Estoque.Web.Models;

public class AuthResponseModel
{
    public string Token { get; set; } = string.Empty;
    public DateTime Expiracao { get; set; }
    public UsuarioViewModel Usuario { get; set; } = new();
}
