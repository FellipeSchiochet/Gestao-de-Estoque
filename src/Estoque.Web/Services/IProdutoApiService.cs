using Estoque.Web.Models;

namespace Estoque.Web.Services;

public interface IProdutoApiService
{
    Task<IReadOnlyList<ProdutoViewModel>> ObterTodosAsync(string? token, CancellationToken cancellationToken = default);
    Task<ProdutoViewModel?> ObterPorIdAsync(string? token, int id, CancellationToken cancellationToken = default);
    Task<ProdutoViewModel> CriarAsync(string? token, ProdutoFormModel produto, CancellationToken cancellationToken = default);
    Task<ProdutoViewModel> AtualizarAsync(string? token, ProdutoFormModel produto, CancellationToken cancellationToken = default);
    Task RemoverAsync(string? token, int id, CancellationToken cancellationToken = default);
}
