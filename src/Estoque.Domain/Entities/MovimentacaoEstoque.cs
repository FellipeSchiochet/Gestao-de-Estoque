using Estoque.Domain.Enums;

namespace Estoque.Domain.Entities;

public class MovimentacaoEstoque
{
    public int Id { get; private set; }
    public int ProdutoId { get; private set; }
    public TipoMovimentacaoEstoque Tipo { get; private set; }
    public int Quantidade { get; private set; }
    public DateTime Data { get; private set; }
    public string? Observacao { get; private set; }

    private MovimentacaoEstoque()
    {
    }

    public MovimentacaoEstoque(int produtoId, TipoMovimentacaoEstoque tipo, int quantidade, DateTime? data = null, string? observacao = null)
    {
        ProdutoId = ValidarProdutoId(produtoId);
        Tipo = ValidarTipo(tipo);
        Quantidade = ValidarQuantidade(quantidade);
        Data = data ?? DateTime.UtcNow;
        Observacao = NormalizarObservacao(observacao);
    }

    public void AtualizarObservacao(string? observacao)
    {
        Observacao = NormalizarObservacao(observacao);
    }

    private static int ValidarProdutoId(int produtoId)
    {
        if (produtoId <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(produtoId), "O identificador do produto deve ser maior que zero.");
        }

        return produtoId;
    }

    private static TipoMovimentacaoEstoque ValidarTipo(TipoMovimentacaoEstoque tipo)
    {
        if (!Enum.IsDefined(tipo))
        {
            throw new ArgumentOutOfRangeException(nameof(tipo), "O tipo de movimentação informado é inválido.");
        }

        return tipo;
    }

    private static int ValidarQuantidade(int quantidade)
    {
        if (quantidade <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(quantidade), "A quantidade da movimentação deve ser maior que zero.");
        }

        return quantidade;
    }

    private static string? NormalizarObservacao(string? observacao)
    {
        return string.IsNullOrWhiteSpace(observacao) ? null : observacao.Trim();
    }
}
