    using MeuLivroDeReceitas.Domain.Entidades;
using MeuLivroDeReceitas.Infrastructure.AcessoRepositorio;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace WebAPI.Test;

public class MeuLivroDeReceitaWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
{
    private Usuario _usuario;
    private string _senha;

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Test")
            .ConfigureServices(services =>
            {
                var descritor = services.SingleOrDefault(s => s.ServiceType == typeof(MeuLivroDeReceitasContext));
                if (descritor is not null) services.Remove(descritor);

                var provider = services.AddEntityFrameworkInMemoryDatabase().BuildServiceProvider();

                services.AddDbContext<MeuLivroDeReceitasContext>(opts =>
                {
                    opts.UseInMemoryDatabase("InMemoryForTesting");
                    opts.UseInternalServiceProvider(provider);
                });

                var servicesProvider = services.BuildServiceProvider();

                using var scope = servicesProvider.CreateScope();
                var scopeService = scope.ServiceProvider;

                var database = scopeService.GetRequiredService<MeuLivroDeReceitasContext>();

                database.Database.EnsureDeleted();

                (_usuario, _senha) = ContextSeedInMemory.Seed(database);
            });
    }

    public Usuario RecuperarUsuario()
    {
        return _usuario;
    }
    
    public string RecuperarSenha()
    {
        return _senha;
    }

}
