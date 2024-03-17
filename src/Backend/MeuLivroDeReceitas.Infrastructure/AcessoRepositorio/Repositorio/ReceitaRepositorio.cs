using MeuLivroDeReceitas.Domain.Entidades;
using MeuLivroDeReceitas.Domain.Repositorios.Receita;

namespace MeuLivroDeReceitas.Infrastructure.AcessoRepositorio.Repositorio;

public class ReceitaRepositorio : IReceitaWriteOnlyRepositorio
{

    private readonly MeuLivroDeReceitasContext _context;

    public ReceitaRepositorio(MeuLivroDeReceitasContext context)
    {
        _context = context;
    }

    public async Task Registrar(Receita receita)
    {
        await _context.Receitas.AddAsync(receita);
    }
}
