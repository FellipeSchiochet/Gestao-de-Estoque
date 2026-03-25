using System.ComponentModel.DataAnnotations;

namespace Estoque.Application.DTOs.Produto;

public class ProdutoRequest
{
    [Required]
    [StringLength(150)]
    public string Nome { get; set; } = string.Empty;

    [StringLength(500)]
    public string? Descricao { get; set; }

    [Range(typeof(decimal), "0", "9999999999999999")]
    public decimal Preco { get; set; }

    [Range(0, int.MaxValue)]
    public int QuantidadeEmEstoque { get; set; }
}
