using Estoque.Application.DTOs.Produto;

namespace Estoque.Application.Interfaces.Services;

public interface IProdutoService
{
    Task<IReadOnlyList<ProdutoResponse>> ObterTodosAsync(CancellationToken cancellationToken = default);
    Task<ProdutoResponse?> ObterPorIdAsync(int id, CancellationToken cancellationToken = default);
    Task<ProdutoResponse> CriarAsync(ProdutoRequest request, CancellationToken cancellationToken = default);
    Task<ProdutoResponse> AtualizarAsync(int id, ProdutoRequest request, CancellationToken cancellationToken = default);
    Task<bool> RemoverAsync(int id, CancellationToken cancellationToken = default);
}
