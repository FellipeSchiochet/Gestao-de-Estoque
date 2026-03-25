using System.ComponentModel.DataAnnotations;

namespace Estoque.Web.Models;

public class MovimentacaoFormModel
{
    [Range(1, int.MaxValue, ErrorMessage = "Selecione um produto.")]
    public int ProdutoId { get; set; }

    [Required(ErrorMessage = "Selecione o tipo de movimentação.")]
    public TipoMovimentacaoEstoqueViewModel Tipo { get; set; } = TipoMovimentacaoEstoqueViewModel.Entrada;

    [Range(1, int.MaxValue, ErrorMessage = "A quantidade deve ser maior que zero.")]
    public int Quantidade { get; set; }

    [StringLength(500, ErrorMessage = "A observação deve ter no máximo 500 caracteres.")]
    public string? Observacao { get; set; }
}
