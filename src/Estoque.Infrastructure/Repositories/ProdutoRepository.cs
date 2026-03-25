using Estoque.Application.Interfaces.Repositories;
using Estoque.Domain.Entities;
using Estoque.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Estoque.Infrastructure.Repositories;

public class ProdutoRepository : IProdutoRepository
{
    private readonly EstoqueDbContext _context;

    public ProdutoRepository(EstoqueDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<Produto>> ObterTodosAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Produtos
            .AsNoTracking()
            .OrderBy(p => p.Nome)
            .ToListAsync(cancellationToken);
    }

    public async Task<Produto?> ObterPorIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.Produtos
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    }

    public async Task AdicionarAsync(Produto produto, CancellationToken cancellationToken = default)
    {
        await _context.Produtos.AddAsync(produto, cancellationToken);
    }

    public void Atualizar(Produto produto)
    {
        _context.Produtos.Update(produto);
    }

    public void Remover(Produto produto)
    {
        _context.Produtos.Remove(produto);
    }

    public async Task SalvarAlteracoesAsync(CancellationToken cancellationToken = default)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}
