using System.ComponentModel.DataAnnotations;

namespace Estoque.Application.DTOs.Auth;

public class RegisterRequest
{
    [Required]
    [StringLength(150)]
    public string Nome { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    [StringLength(200)]
    public string Email { get; set; } = string.Empty;

    [Required]
    [MinLength(6)]
    public string Senha { get; set; } = string.Empty;
}
