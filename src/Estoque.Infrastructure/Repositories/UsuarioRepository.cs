using Estoque.Application.Interfaces.Repositories;
using Estoque.Domain.Entities;
using Estoque.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Estoque.Infrastructure.Repositories;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly EstoqueDbContext _context;

    public UsuarioRepository(EstoqueDbContext context)
    {
        _context = context;
    }

    public async Task<Usuario?> ObterPorEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        var emailNormalizado = email.Trim().ToLowerInvariant();

        return await _context.Usuarios
            .FirstOrDefaultAsync(u => u.Email == emailNormalizado, cancellationToken);
    }

    public async Task AdicionarAsync(Usuario usuario, CancellationToken cancellationToken = default)
    {
        await _context.Usuarios.AddAsync(usuario, cancellationToken);
    }

    public async Task SalvarAlteracoesAsync(CancellationToken cancellationToken = default)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}
