using MeuLivroDeReceitas.API.Filtros;
using MeuLivroDeReceitas.Application;
using MeuLivroDeReceitas.Application.Servicos.AutoMapper;
using MeuLivroDeReceitas.Domain.Extension;
using MeuLivroDeReceitas.Infrastructure;
using MeuLivroDeReceitas.Infrastructure.Migrations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddRepositorio(builder.Configuration);
builder.Services.AddApplication(builder.Configuration);

builder.Services.AddMvc(opts => opts.Filters.Add(typeof(FiltroDasExceptions)));

builder.Services.AddScoped(prov => new AutoMapper.MapperConfiguration(cfg =>
{
    cfg.AddProfile(new AutoMapperConfiguracao());
}).CreateMapper());

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


app.Run();

void AtualizarDb() 
{
    var nomeDb = builder.Configuration.GetNomeDatabase();
    var conexao = builder.Configuration.GetConexao();

    Database.CriarDatabase(conexao, nomeDb);

    app.MigrateDb();
}