namespace MeuLivroDeReceitas.Domain.Repositorios.Usuario;

public interface IUsuarioReadOnlyRepositorio
{
    Task<bool> ExisteUsuarioEmail(string email);
    Task<Entidades.Usuario> Login(string email, string senha);
    Task<Entidades.Usuario> RecuperarPorEmail(string email);
}
