namespace Estoque.Application.DTOs.Produto;

public class ProdutoResponse
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string? Descricao { get; set; }
    public decimal Preco { get; set; }
    public int QuantidadeEmEstoque { get; set; }
    public DateTime DataCadastro { get; set; }
}
