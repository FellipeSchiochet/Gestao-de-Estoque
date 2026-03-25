using Estoque.Domain.Entities;

namespace Estoque.Application.Interfaces.Repositories;

public interface IProdutoRepository
{
    Task<IReadOnlyList<Produto>> ObterTodosAsync(CancellationToken cancellationToken = default);
    Task<Produto?> ObterPorIdAsync(int id, CancellationToken cancellationToken = default);
    Task AdicionarAsync(Produto produto, CancellationToken cancellationToken = default);
    void Atualizar(Produto produto);
    void Remover(Produto produto);
    Task SalvarAlteracoesAsync(CancellationToken cancellationToken = default);
}
