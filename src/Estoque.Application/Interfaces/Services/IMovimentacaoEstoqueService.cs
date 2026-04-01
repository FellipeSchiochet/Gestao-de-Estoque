using Estoque.Application.DTOs.Movimentacao;

namespace Estoque.Application.Interfaces.Services;

public interface IMovimentacaoEstoqueService
{
    Task<MovimentacaoEstoqueResponse> RegistrarAsync(MovimentacaoEstoqueRequest request, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<MovimentacaoEstoqueResponse>> ObterTodosAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyList<MovimentacaoEstoqueResponse>> ObterHistoricoPorProdutoAsync(int produtoId, CancellationToken cancellationToken = default);
}
