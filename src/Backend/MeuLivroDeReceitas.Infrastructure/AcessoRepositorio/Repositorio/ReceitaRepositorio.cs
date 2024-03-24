using MeuLivroDeReceitas.Domain.Entidades;
using MeuLivroDeReceitas.Domain.Repositorios.Receita;
using Microsoft.EntityFrameworkCore;

namespace MeuLivroDeReceitas.Infrastructure.AcessoRepositorio.Repositorio;

public class ReceitaRepositorio : IReceitaWriteOnlyRepositorio, IReceitaReadOnlyRepositorio
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

    public async Task<IList<Receita>> RecuperarTodasDoUsuario(long usuarioId)
    {
        return await _context.Receitas.AsNoTracking()
            .Include(r => r.Ingredientes)
            .Where(r => r.UsuarioId == usuarioId).ToListAsync();
    }

    public async Task<Receita> RecuperarPorId(long receitaId)
    {
        return await _context.Receitas.AsNoTracking()
            .Include(r => r.Ingredientes)
            .FirstOrDefaultAsync(r => r.Id == receitaId);
    }
}
