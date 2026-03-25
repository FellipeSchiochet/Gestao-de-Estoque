using System.ComponentModel.DataAnnotations;

namespace Estoque.Web.Models;

public class RegisterFormModel
{
    [Required(ErrorMessage = "O nome e obrigatorio.")]
    [StringLength(150, ErrorMessage = "O nome deve ter no maximo 150 caracteres.")]
    public string Nome { get; set; } = string.Empty;

    [Required(ErrorMessage = "O e-mail e obrigatorio.")]
    [EmailAddress(ErrorMessage = "Informe um e-mail valido.")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "A senha e obrigatoria.")]
    [MinLength(6, ErrorMessage = "A senha deve ter pelo menos 6 caracteres.")]
    public string Senha { get; set; } = string.Empty;

    [Required(ErrorMessage = "Confirme a senha.")]
    [Compare(nameof(Senha), ErrorMessage = "As senhas nao coincidem.")]
    public string ConfirmacaoSenha { get; set; } = string.Empty;
}
