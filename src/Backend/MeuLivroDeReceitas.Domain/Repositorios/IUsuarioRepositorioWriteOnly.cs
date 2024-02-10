using MeuLivroDeReceitas.Domain.Entidades;

namespace MeuLivroDeReceitas.Domain.Repositorios;

public interface IUsuarioRepositorioWriteOnly
{
    Task Adicionar(Usuario usuario);
}
