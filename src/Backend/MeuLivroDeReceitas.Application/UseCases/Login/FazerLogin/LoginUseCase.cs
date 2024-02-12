using MeuLivroDeReceitas.Application.Servicos.Criptografia;
using MeuLivroDeReceitas.Application.Servicos.Token;
using MeuLivroDeReceitas.Comunicacao.Request;
using MeuLivroDeReceitas.Comunicacao.Response;
using MeuLivroDeReceitas.Domain.Repositorios;
using MeuLivroDeReceitas.Exceptions.ExceptionsBase;
using System;

namespace MeuLivroDeReceitas.Application.UseCases.Login.FazerLogin;

public class LoginUseCase : ILoginUseCase
{
    private readonly EncriptadorDeSenha _encriptadorDeSenha;
    private readonly TokenController _tokenController;
    private readonly IUsuarioReadOnlyRepositorio _usuarioRepositorioReadOnly;

    public LoginUseCase(EncriptadorDeSenha encriptadorDeSenha, TokenController tokenController, IUsuarioReadOnlyRepositorio usuarioRepositorioReadOnly)
    {
        _encriptadorDeSenha = encriptadorDeSenha;
        _tokenController = tokenController;
        _usuarioRepositorioReadOnly = usuarioRepositorioReadOnly;
    }

    public async Task<RespostaLoginJson> Executar(RequisicaoLoginJson request)
    {
        var encryptSenha = _encriptadorDeSenha.Criptografar(request.Senha);

        var usuario = await _usuarioRepositorioReadOnly.Login(request.Email, encryptSenha);

        if (usuario == null)
        {
            throw new LoginInvalidoException();
        }

        return new RespostaLoginJson
        {
            Nome = usuario.Nome,
            Token = _tokenController.GerarToken(request.Email)
        };
    }
}
