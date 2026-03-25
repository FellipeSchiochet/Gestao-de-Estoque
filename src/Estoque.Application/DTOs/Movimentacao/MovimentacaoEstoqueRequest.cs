using System.ComponentModel.DataAnnotations;
using Estoque.Domain.Enums;

namespace Estoque.Application.DTOs.Movimentacao;

public class MovimentacaoEstoqueRequest
{
    [Range(1, int.MaxValue)]
    public int ProdutoId { get; set; }

    [Required]
    public TipoMovimentacaoEstoque Tipo { get; set; }

    [Range(1, int.MaxValue)]
    public int Quantidade { get; set; }

    [StringLength(500)]
    public string? Observacao { get; set; }
}
