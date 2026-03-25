using Estoque.Application.DTOs.Produto;
using Estoque.Application.Interfaces.Repositories;
using Estoque.Application.Interfaces.Services;
using Estoque.Domain.Entities;

namespace Estoque.Application.Services;

public class ProdutoService : IProdutoService
{
    private readonly IProdutoRepository _produtoRepository;

    public ProdutoService(IProdutoRepository produtoRepository)
    {
        _produtoRepository = produtoRepository;
    }

    public async Task<IReadOnlyList<ProdutoResponse>> ObterTodosAsync(CancellationToken cancellationToken = default)
    {
        var produtos = await _produtoRepository.ObterTodosAsync(cancellationToken);

        return produtos
            .Select(MapearParaResponse)
            .ToList();
    }

    public async Task<ProdutoResponse?> ObterPorIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var produto = await _produtoRepository.ObterPorIdAsync(id, cancellationToken);

        return produto is null ? null : MapearParaResponse(produto);
    }

    public async Task<ProdutoResponse> CriarAsync(ProdutoRequest request, CancellationToken cancellationToken = default)
    {
        var produto = new Produto(
            request.Nome,
            request.Descricao,
            request.Preco,
            request.QuantidadeEmEstoque);

        await _produtoRepository.AdicionarAsync(produto, cancellationToken);
        await _produtoRepository.SalvarAlteracoesAsync(cancellationToken);

        return MapearParaResponse(produto);
    }

    public async Task<ProdutoResponse> AtualizarAsync(int id, ProdutoRequest request, CancellationToken cancellationToken = default)
    {
        var produto = await _produtoRepository.ObterPorIdAsync(id, cancellationToken)
            ?? throw new KeyNotFoundException($"Produto com id {id} não foi encontrado.");

        produto.AtualizarDados(request.Nome, request.Descricao, request.Preco);
        produto.DefinirQuantidadeEmEstoque(request.QuantidadeEmEstoque);

        _produtoRepository.Atualizar(produto);
        await _produtoRepository.SalvarAlteracoesAsync(cancellationToken);

        return MapearParaResponse(produto);
    }

    public async Task<bool> RemoverAsync(int id, CancellationToken cancellationToken = default)
    {
        var produto = await _produtoRepository.ObterPorIdAsync(id, cancellationToken);

        if (produto is null)
        {
            return false;
        }

        _produtoRepository.Remover(produto);
        await _produtoRepository.SalvarAlteracoesAsync(cancellationToken);

        return true;
    }

    private static ProdutoResponse MapearParaResponse(Produto produto)
    {
        return new ProdutoResponse
        {
            Id = produto.Id,
            Nome = produto.Nome,
            Descricao = produto.Descricao,
            Preco = produto.Preco,
            QuantidadeEmEstoque = produto.QuantidadeEmEstoque,
            DataCadastro = produto.DataCadastro
        };
    }
}
