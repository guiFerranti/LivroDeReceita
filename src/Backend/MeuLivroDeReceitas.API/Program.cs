using MeuLivroDeReceitas.API.Filtros;
using MeuLivroDeReceitas.API.Middleware;
using MeuLivroDeReceitas.Application;
using MeuLivroDeReceitas.Application.Servicos.AutoMapper;
using MeuLivroDeReceitas.Domain.Extension;
using MeuLivroDeReceitas.Infrastructure;
using MeuLivroDeReceitas.Infrastructure.AcessoRepositorio;
using MeuLivroDeReceitas.Infrastructure.Migrations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// lower case na rota

builder.Services.AddRouting(opt => opt.LowercaseUrls = true);

// adicionar httpcontext para acessar tokens 

builder.Services.AddHttpContextAccessor();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication(builder.Configuration);

builder.Services.AddMvc(opts => opts.Filters.Add(typeof(FiltroDasExceptions)));

builder.Services.AddScoped(prov => new AutoMapper.MapperConfiguration(cfg =>
{
    cfg.AddProfile(new AutoMapperConfiguracao());
}).CreateMapper());

// adicionando autenticação

builder.Services.AddScoped<UsuarioAutenticadoAtributo>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

AtualizarDb();

app.UseMiddleware<CultureMiddleware>();

app.Run();

void AtualizarDb() 
{
    using var serviceScope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
    using var context = serviceScope.ServiceProvider.GetService<MeuLivroDeReceitasContext>();

    bool? databaseInMemory = context?.Database?.ProviderName?.Equals("Microsoft.EntityFrameworkCore.InMemory");

    if (!databaseInMemory.HasValue || !databaseInMemory.Value)
    {
        var nomeDb = builder.Configuration.GetNomeDatabase();
        var conexao = builder.Configuration.GetConexao();

        Database.CriarDatabase(conexao, nomeDb);

        app.MigrateDb();
    }

}

public partial class Program { }