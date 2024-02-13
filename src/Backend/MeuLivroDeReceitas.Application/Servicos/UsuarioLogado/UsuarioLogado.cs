using MeuLivroDeReceitas.Application.Servicos.Token;
using MeuLivroDeReceitas.Domain.Entidades;
using MeuLivroDeReceitas.Domain.Repositorios.Usuario;
using Microsoft.AspNetCore.Http;

namespace MeuLivroDeReceitas.Application.Servicos.UsuarioLogado;

public class UsuarioLogado : IUsuarioLogado
{
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly TokenController _tokenController;
    private readonly IUsuarioReadOnlyRepositorio _usuarioReadOnlyRepositorio;

    public UsuarioLogado(IHttpContextAccessor contextAccessor, TokenController tokenController, IUsuarioReadOnlyRepositorio usuarioReadOnlyRepositorio)
    {
        _contextAccessor = contextAccessor;
        _tokenController = tokenController;
        _usuarioReadOnlyRepositorio = usuarioReadOnlyRepositorio;
    }

    public async Task<Usuario> RecuperarUsuario()
    {
        var token = _contextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
        var emailUsuario = _tokenController.RecuperarEmail(token);

        var usuario = await _usuarioReadOnlyRepositorio.RecuperarPorEmail(emailUsuario);

        return usuario;
    }
}
