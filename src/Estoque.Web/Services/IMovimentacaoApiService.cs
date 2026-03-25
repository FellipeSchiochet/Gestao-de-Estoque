using Estoque.Web.Models;

namespace Estoque.Web.Services;

public interface IMovimentacaoApiService
{
    Task<MovimentacaoViewModel> RegistrarAsync(string? token, MovimentacaoFormModel movimentacao, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<MovimentacaoViewModel>> ObterHistoricoAsync(string? token, int produtoId, CancellationToken cancellationToken = default);
}
