namespace Estoque.Web.Models;

public class MovimentacaoViewModel
{
    public int Id { get; set; }
    public int ProdutoId { get; set; }
    public string ProdutoNome { get; set; } = string.Empty;
    public TipoMovimentacaoEstoqueViewModel Tipo { get; set; }
    public int Quantidade { get; set; }
    public DateTime Data { get; set; }
    public string? Observacao { get; set; }
    public int EstoqueAtual { get; set; }
}
