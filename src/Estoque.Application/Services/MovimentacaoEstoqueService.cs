using Estoque.Application.DTOs.Movimentacao;
using Estoque.Application.Interfaces.Repositories;
using Estoque.Application.Interfaces.Services;
using Estoque.Domain.Entities;
using Estoque.Domain.Enums;

namespace Estoque.Application.Services;

public class MovimentacaoEstoqueService : IMovimentacaoEstoqueService
{
    private readonly IProdutoRepository _produtoRepository;
    private readonly IMovimentacaoEstoqueRepository _movimentacaoRepository;

    public MovimentacaoEstoqueService(
        IProdutoRepository produtoRepository,
        IMovimentacaoEstoqueRepository movimentacaoRepository)
    {
        _produtoRepository = produtoRepository;
        _movimentacaoRepository = movimentacaoRepository;
    }

    public async Task<MovimentacaoEstoqueResponse> RegistrarAsync(
        MovimentacaoEstoqueRequest request,
        CancellationToken cancellationToken = default)
    {
        var produto = await _produtoRepository.ObterPorIdAsync(request.ProdutoId, cancellationToken)
            ?? throw new KeyNotFoundException($"Produto com id {request.ProdutoId} não foi encontrado.");

        AplicarMovimentacao(produto, request.Tipo, request.Quantidade);

        var movimentacao = new MovimentacaoEstoque(
            request.ProdutoId,
            request.Tipo,
            request.Quantidade,
            observacao: request.Observacao);

        await _movimentacaoRepository.AdicionarAsync(movimentacao, cancellationToken);
        _produtoRepository.Atualizar(produto);
        await _movimentacaoRepository.SalvarAlteracoesAsync(cancellationToken);

        return MapearParaResponse(movimentacao, produto);
    }

    public async Task<IReadOnlyList<MovimentacaoEstoqueResponse>> ObterHistoricoPorProdutoAsync(
        int produtoId,
        CancellationToken cancellationToken = default)
    {
        var produto = await _produtoRepository.ObterPorIdAsync(produtoId, cancellationToken)
            ?? throw new KeyNotFoundException($"Produto com id {produtoId} não foi encontrado.");

        var movimentacoes = await _movimentacaoRepository.ObterPorProdutoIdAsync(produtoId, cancellationToken);
        var estoqueCalculado = 0;
        var responses = new List<MovimentacaoEstoqueResponse>();

        foreach (var movimentacao in movimentacoes)
        {
            estoqueCalculado = movimentacao.Tipo == TipoMovimentacaoEstoque.Entrada
                ? estoqueCalculado + movimentacao.Quantidade
                : estoqueCalculado - movimentacao.Quantidade;

            responses.Add(new MovimentacaoEstoqueResponse
            {
                Id = movimentacao.Id,
                ProdutoId = movimentacao.ProdutoId,
                ProdutoNome = produto.Nome,
                Tipo = movimentacao.Tipo,
                Quantidade = movimentacao.Quantidade,
                Data = movimentacao.Data,
                Observacao = movimentacao.Observacao,
                EstoqueAtual = estoqueCalculado
            });
        }

        return responses;
    }

    private static void AplicarMovimentacao(Produto produto, TipoMovimentacaoEstoque tipo, int quantidade)
    {
        switch (tipo)
        {
            case TipoMovimentacaoEstoque.Entrada:
                produto.RegistrarEntrada(quantidade);
                break;
            case TipoMovimentacaoEstoque.Saida:
                produto.RegistrarSaida(quantidade);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(tipo), "Tipo de movimentação inválido.");
        }
    }

    private static MovimentacaoEstoqueResponse MapearParaResponse(MovimentacaoEstoque movimentacao, Produto produto)
    {
        return new MovimentacaoEstoqueResponse
        {
            Id = movimentacao.Id,
            ProdutoId = movimentacao.ProdutoId,
            ProdutoNome = produto.Nome,
            Tipo = movimentacao.Tipo,
            Quantidade = movimentacao.Quantidade,
            Data = movimentacao.Data,
            Observacao = movimentacao.Observacao,
            EstoqueAtual = produto.QuantidadeEmEstoque
        };
    }
}
