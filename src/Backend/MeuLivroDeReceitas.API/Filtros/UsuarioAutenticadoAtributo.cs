using MeuLivroDeReceitas.Application.Servicos.Token;
using MeuLivroDeReceitas.Comunicacao.Response;
using MeuLivroDeReceitas.Domain.Repositorios.Usuario;
using MeuLivroDeReceitas.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;

namespace MeuLivroDeReceitas.API.Filtros;

public class UsuarioAutenticadoAtributo : AuthorizeAttribute, IAsyncAuthorizationFilter
{
    private readonly TokenController _tokenController;
    private readonly IUsuarioReadOnlyRepositorio _repositorioReadOnly;

    public UsuarioAutenticadoAtributo(TokenController tokenController, IUsuarioReadOnlyRepositorio repositorioReadOnly)
    {
        _tokenController = tokenController;
        _repositorioReadOnly = repositorioReadOnly;
    }

    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        try
        {
            var token = TokenNaRequisicao(context);

            var email = _tokenController.RecuperarEmail(token);
            var usuario = _repositorioReadOnly.RecuperarPorEmail(email);

            if (usuario is null)
            {
                throw new Exception();
            }
        }
        catch (SecurityTokenExpiredException)
        {
            TokenExpirado(context);
        }
        catch
        {
            UsuarioSemPermissao(context);
        }
    }

    private string TokenNaRequisicao(AuthorizationFilterContext context)
    {
        var auth = context.HttpContext.Request.Headers["Authorization"].ToString();

        if (string.IsNullOrWhiteSpace(auth))
        {
            throw new Exception();
        }

        return auth.Replace("Bearer ", "");
    }

    private void TokenExpirado(AuthorizationFilterContext context)
    {
        context.Result = new UnauthorizedObjectResult(new RespostaErroJson(ResourceMensagensDeErro.TOKEN_EXPIRADO));
    }
    
    private void UsuarioSemPermissao(AuthorizationFilterContext context)
    {
        context.Result = new UnauthorizedObjectResult(new RespostaErroJson(ResourceMensagensDeErro.USUARIO_SEM_PERMISSAO));
    }

}
