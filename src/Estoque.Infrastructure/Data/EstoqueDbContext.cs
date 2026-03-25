using Estoque.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Estoque.Infrastructure.Data;

public class EstoqueDbContext : DbContext
{
    public EstoqueDbContext(DbContextOptions<EstoqueDbContext> options) : base(options)
    {
    }

    public DbSet<Produto> Produtos => Set<Produto>();
    public DbSet<MovimentacaoEstoque> MovimentacoesEstoque => Set<MovimentacaoEstoque>();
    public DbSet<Usuario> Usuarios => Set<Usuario>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Produto>(entity =>
        {
            entity.ToTable("Produtos");

            entity.HasKey(x => x.Id);

            entity.Property(x => x.Nome)
                .HasMaxLength(150)
                .IsRequired();

            entity.Property(x => x.Descricao)
                .HasMaxLength(500);

            entity.Property(x => x.Preco)
                .HasPrecision(18, 2)
                .IsRequired();

            entity.Property(x => x.QuantidadeEmEstoque)
                .IsRequired();

            entity.Property(x => x.DataCadastro)
                .IsRequired();
        });

        modelBuilder.Entity<MovimentacaoEstoque>(entity =>
        {
            entity.ToTable("MovimentacoesEstoque");

            entity.HasKey(x => x.Id);

            entity.Property(x => x.Tipo)
                .HasConversion<string>()
                .HasMaxLength(20)
                .IsRequired();

            entity.Property(x => x.Quantidade)
                .IsRequired();

            entity.Property(x => x.Data)
                .IsRequired();

            entity.Property(x => x.Observacao)
                .HasMaxLength(500);

            entity.HasOne<Produto>()
                .WithMany()
                .HasForeignKey(x => x.ProdutoId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.ToTable("Usuarios");

            entity.HasKey(x => x.Id);

            entity.Property(x => x.Nome)
                .HasMaxLength(150)
                .IsRequired();

            entity.Property(x => x.Email)
                .HasMaxLength(200)
                .IsRequired();

            entity.Property(x => x.SenhaHash)
                .IsRequired();

            entity.Property(x => x.DataCadastro)
                .IsRequired();

            entity.HasIndex(x => x.Email)
                .IsUnique();
        });
    }
}
