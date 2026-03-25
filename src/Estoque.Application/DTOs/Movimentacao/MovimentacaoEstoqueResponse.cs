using Estoque.Domain.Enums;

namespace Estoque.Application.DTOs.Movimentacao;

public class MovimentacaoEstoqueResponse
{
    public int Id { get; set; }
    public int ProdutoId { get; set; }
    public string ProdutoNome { get; set; } = string.Empty;
    public TipoMovimentacaoEstoque Tipo { get; set; }
    public int Quantidade { get; set; }
    public DateTime Data { get; set; }
    public string? Observacao { get; set; }
    public int EstoqueAtual { get; set; }
}
