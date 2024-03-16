using FluentAssertions;
using MeuLivroDeReceitas.Application.UseCases.Login.FazerLogin;
using MeuLivroDeReceitas.Comunicacao.Response;
using MeuLivroDeReceitas.Exceptions;
using MeuLivroDeReceitas.Exceptions.ExceptionsBase;
using Utilitario.ParaOsTestes.Criptografia;
using Utilitario.ParaOsTestes.Entidades;
using Utilitario.ParaOsTestes.Repositorios;
using Utilitario.ParaOsTestes.Token;
using Xunit;

namespace UseCases.Test.Login.FazerLogin;

public class LoginUseCaseTest
{
    [Fact]
    public async Task Validar_Sucesso()
    {
        (var usuario, var senha) = UsuarioBuilder.Construir();

        var useCase = CriarUseCase(usuario);

        var resposta = await useCase.Executar(new RequisicaoLoginJson
        {
            Email = usuario.Email,
            Senha = senha
        });

        resposta.Should().NotBeNull();
        resposta.Nome.Should().Be(usuario.Nome);
        resposta.Token.Should().NotBeNullOrWhiteSpace();
    }
        
    [Fact]
    public async Task Validar_Erro_Senha_Invalida()
    {
        (var usuario, var senha) = UsuarioBuilder.Construir();

        var useCase = CriarUseCase(usuario);

        Func<Task> acao = async () =>
        {
            var resposta = await useCase.Executar(new RequisicaoLoginJson
            {
                Email = usuario.Email,
                Senha = "senhaInvalidaX"
            });
        };

        await acao.Should().ThrowAsync<LoginInvalidoException>()
            .Where(exc => exc.Message.Equals(ResourceMensagensDeErro.LOGIN_INVALIDO));
    }
         
    [Fact]
    public async Task Validar_Erro_Email_Invalida()
    {
        (var usuario, var senha) = UsuarioBuilder.Construir();
        await Console.Out.WriteLineAsync();
        var useCase = CriarUseCase(usuario);

        Func<Task> acao = async () =>
        {
            var resposta = await useCase.Executar(new RequisicaoLoginJson
            {
                Email = "emailInvalidoX",
                Senha = senha
            });
        };

        await acao.Should().ThrowAsync<LoginInvalidoException>()
            .Where(exc => exc.Message.Equals(ResourceMensagensDeErro.LOGIN_INVALIDO));
    }
        
    [Fact]
    public async Task Validar_Erro_Email_Senha_Invalida()
    {
        (var usuario, var senha) = UsuarioBuilder.Construir();

        var useCase = CriarUseCase(usuario);

        Func<Task> acao = async () =>
        {
            var resposta = await useCase.Executar(new RequisicaoLoginJson
            {
                Email = "emailInvalidoX",
                Senha = "senhaInvalidaX"
            });
        };

        await acao.Should().ThrowAsync<LoginInvalidoException>()
            .Where(exc => exc.Message.Equals(ResourceMensagensDeErro.LOGIN_INVALIDO));
    }
        
    

    private LoginUseCase CriarUseCase(MeuLivroDeReceitas.Domain.Entidades.Usuario usuario)
    {

        var encriptador = EncriptadorDeSenhaBuilder.Instancia();
        var tokenController = TokenControllerBuilder.Instancia();
        var repositorioReadOnly = UsuarioReadOnlyRepositorioBuilder.Instancia().Login(usuario).Construir();

        return new LoginUseCase(encriptador, tokenController, repositorioReadOnly);
    }

}