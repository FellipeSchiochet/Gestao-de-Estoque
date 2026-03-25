using Estoque.Domain.Entities;

namespace Estoque.Application.Interfaces.Repositories;

public interface IMovimentacaoEstoqueRepository
{
    Task AdicionarAsync(MovimentacaoEstoque movimentacao, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<MovimentacaoEstoque>> ObterPorProdutoIdAsync(int produtoId, CancellationToken cancellationToken = default);
    Task SalvarAlteracoesAsync(CancellationToken cancellationToken = default);
}
