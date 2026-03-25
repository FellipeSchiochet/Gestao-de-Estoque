namespace Estoque.Domain.Entities;

public class Produto
{
    public int Id { get; private set; }
    public string Nome { get; private set; }
    public string? Descricao { get; private set; }
    public decimal Preco { get; private set; }
    public int QuantidadeEmEstoque { get; private set; }
    public DateTime DataCadastro { get; private set; }

    private Produto()
    {
        Nome = string.Empty;
    }

    public Produto(string nome, string? descricao, decimal preco, int quantidadeEmEstoque, DateTime? dataCadastro = null)
    {
        Nome = ValidarNome(nome);
        Descricao = NormalizarDescricao(descricao);
        Preco = ValidarPreco(preco);
        QuantidadeEmEstoque = ValidarQuantidade(quantidadeEmEstoque);
        DataCadastro = dataCadastro ?? DateTime.UtcNow;
    }

    public void AtualizarDados(string nome, string? descricao, decimal preco)
    {
        Nome = ValidarNome(nome);
        Descricao = NormalizarDescricao(descricao);
        Preco = ValidarPreco(preco);
    }

    public void DefinirQuantidadeEmEstoque(int quantidadeEmEstoque)
    {
        QuantidadeEmEstoque = ValidarQuantidade(quantidadeEmEstoque);
    }

    public void RegistrarEntrada(int quantidade)
    {
        QuantidadeEmEstoque += ValidarQuantidadePositiva(quantidade);
    }

    public void RegistrarSaida(int quantidade)
    {
        var quantidadeValidada = ValidarQuantidadePositiva(quantidade);

        if (quantidadeValidada > QuantidadeEmEstoque)
        {
            throw new InvalidOperationException("A quantidade de saída não pode ser maior que o estoque disponível.");
        }

        QuantidadeEmEstoque -= quantidadeValidada;
    }

    private static string ValidarNome(string nome)
    {
        if (string.IsNullOrWhiteSpace(nome))
        {
            throw new ArgumentException("O nome do produto é obrigatório.", nameof(nome));
        }

        return nome.Trim();
    }

    private static string? NormalizarDescricao(string? descricao)
    {
        return string.IsNullOrWhiteSpace(descricao) ? null : descricao.Trim();
    }

    private static decimal ValidarPreco(decimal preco)
    {
        if (preco < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(preco), "O preço não pode ser negativo.");
        }

        return preco;
    }

    private static int ValidarQuantidade(int quantidadeEmEstoque)
    {
        if (quantidadeEmEstoque < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(quantidadeEmEstoque), "A quantidade em estoque não pode ser negativa.");
        }

        return quantidadeEmEstoque;
    }

    private static int ValidarQuantidadePositiva(int quantidade)
    {
        if (quantidade <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(quantidade), "A quantidade deve ser maior que zero.");
        }

        return quantidade;
    }
}
