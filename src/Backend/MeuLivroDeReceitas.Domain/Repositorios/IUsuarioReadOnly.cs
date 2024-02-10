namespace MeuLivroDeReceitas.Domain.Repositorios;

public interface IUsuarioReadOnly
{
    Task<bool> ExisteUsuarioEmail(string email);
}
