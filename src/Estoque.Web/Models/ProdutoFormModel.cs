using System.ComponentModel.DataAnnotations;

namespace Estoque.Web.Models;

public class ProdutoFormModel
{
    public int? Id { get; set; }

    [Required(ErrorMessage = "O nome é obrigatório.")]
    [StringLength(150, ErrorMessage = "O nome deve ter no máximo 150 caracteres.")]
    public string Nome { get; set; } = string.Empty;

    [StringLength(500, ErrorMessage = "A descrição deve ter no máximo 500 caracteres.")]
    public string? Descricao { get; set; }

    [Range(typeof(decimal), "0", "9999999999999999", ErrorMessage = "O preço deve ser maior ou igual a zero.")]
    public decimal Preco { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "O estoque deve ser maior ou igual a zero.")]
    public int QuantidadeEmEstoque { get; set; }
}
