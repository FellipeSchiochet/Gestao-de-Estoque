using Estoque.Domain.Entities;

namespace Estoque.Application.Interfaces.Repositories;

public interface IUsuarioRepository
{
    Task<Usuario?> ObterPorEmailAsync(string email, CancellationToken cancellationToken = default);
    Task AdicionarAsync(Usuario usuario, CancellationToken cancellationToken = default);
    Task SalvarAlteracoesAsync(CancellationToken cancellationToken = default);
}
