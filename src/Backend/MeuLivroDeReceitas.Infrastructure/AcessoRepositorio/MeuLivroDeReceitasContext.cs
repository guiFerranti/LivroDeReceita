using MeuLivroDeReceitas.Domain.Entidades;
using Microsoft.EntityFrameworkCore;

namespace MeuLivroDeReceitas.Infrastructure.AcessoRepositorio;

public class MeuLivroDeReceitasContext : DbContext
{
    public MeuLivroDeReceitasContext(DbContextOptions options) : base(options) { }

    public DbSet<Usuario> Usuarios { get; set; }




    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof (MeuLivroDeReceitasContext).Assembly);
    }


}
