using FluentAssertions;
using MeuLivroDeReceitas.Application.UseCases.Usuario.Registrar;
using MeuLivroDeReceitas.Exceptions;
using MeuLivroDeReceitas.Exceptions.ExceptionsBase;
using Utilitario.ParaOsTestes.Criptografia;
using Utilitario.ParaOsTestes.Mapper;
using Utilitario.ParaOsTestes.Repositorios;
using Utilitario.ParaOsTestes.Requisicoes;
using Utilitario.ParaOsTestes.Token;
using Xunit;

namespace UseCases.Test.Usuario.Registrar;

public class RegistrarUsuarioUseCaseTest
{
    [Fact]
    public async Task Validar_Sucesso()
    {
        var req = RequisicaoRegistrarUsuarioBuilder.Construir();

        var useCase = CriarUseCase();
        var resposta = await useCase.Executar(req);

        resposta.Should().NotBeNull();
        resposta.Token.Should().NotBeNullOrWhiteSpace();
    }

    [Fact]
    public async Task Validar_Erro_Email_Duplicado()
    {
        var req = RequisicaoRegistrarUsuarioBuilder.Construir();

        var useCase = CriarUseCase(req.Email);

        Func<Task> acao = async () =>
        {
            await useCase.Executar(req);
        };

        await acao.Should().ThrowAsync<ErrosDeValidacaoException>()
            .Where(exc => exc.MensagensDeErro.Count == 1 && exc.MensagensDeErro.Contains(ResourceMensagensDeErro.EMAIL_REPETIDO));
    }

    [Fact]
    public async Task Validar_Erro_Email_Vazio()
    {
        
        var req = RequisicaoRegistrarUsuarioBuilder.Construir();
        req.Email = string.Empty;
        
        var useCase = CriarUseCase();

        Func<Task> acao = async () =>
        {
            await useCase.Executar(req);
        };

        await acao.Should().ThrowAsync<ErrosDeValidacaoException>()
            .Where(exc => exc.MensagensDeErro.Count == 1 && exc.MensagensDeErro.Contains(ResourceMensagensDeErro.EMAIL_USER_VAZIO));
    }


    private RegistrarUsuarioUseCase CriarUseCase(string email = "")
    {
        var repositorio = UsuarioWriteOnlyRepositorioBuilder.Instancia().Construir();
        var mapper = MapperBuilder.Instancia();
        var unidadeDeTrabalho = UnidadeDeTrabalhoBuilder.Instancia().Construir();
        var encriptador = EncriptadorDeSenhaBuilder.Instancia();
        var tokenController = TokenControllerBuilder.Instancia();
        var repositorioReadOnly = UsuarioReadOnlyRepositorioBuilder.Instancia().ExisteUsuarioComEmail(email).Construir();

        return new RegistrarUsuarioUseCase(repositorioReadOnly, repositorio, mapper, unidadeDeTrabalho, encriptador, tokenController);
    }

}
