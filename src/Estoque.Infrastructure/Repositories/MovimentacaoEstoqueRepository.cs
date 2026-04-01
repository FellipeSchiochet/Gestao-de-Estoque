using Estoque.Application.Interfaces.Repositories;
using Estoque.Domain.Entities;
using Estoque.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Estoque.Infrastructure.Repositories;

public class MovimentacaoEstoqueRepository : IMovimentacaoEstoqueRepository
{
    private readonly EstoqueDbContext _context;

    public MovimentacaoEstoqueRepository(EstoqueDbContext context)
    {
        _context = context;
    }

    public async Task AdicionarAsync(MovimentacaoEstoque movimentacao, CancellationToken cancellationToken = default)
    {
        await _context.MovimentacoesEstoque.AddAsync(movimentacao, cancellationToken);
    }

    public async Task<IReadOnlyList<MovimentacaoEstoque>> ObterTodosAsync(CancellationToken cancellationToken = default)
    {
        return await _context.MovimentacoesEstoque
            .AsNoTracking()
            .OrderByDescending(m => m.Data)
            .ThenByDescending(m => m.Id)
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<MovimentacaoEstoque>> ObterPorProdutoIdAsync(int produtoId, CancellationToken cancellationToken = default)
    {
        return await _context.MovimentacoesEstoque
            .AsNoTracking()
            .Where(m => m.ProdutoId == produtoId)
            .OrderBy(m => m.Data)
            .ThenBy(m => m.Id)
            .ToListAsync(cancellationToken);
    }

    public async Task SalvarAlteracoesAsync(CancellationToken cancellationToken = default)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}
