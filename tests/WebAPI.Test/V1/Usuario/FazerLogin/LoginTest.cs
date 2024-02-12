using FluentAssertions;
using MeuLivroDeReceitas.Comunicacao.Response;
using MeuLivroDeReceitas.Exceptions;
using System.Net;
using System.Text.Json;
using Xunit;

namespace WebAPI.Test.V1.Usuario.FazerLogin;

public class LoginTest : ControllerBase
{
    private const string METODO = "login";

    private MeuLivroDeReceitas.Domain.Entidades.Usuario _usuario;
    private string _senha;


    public LoginTest(MeuLivroDeReceitaWebApplicationFactory<Program> factory) : base(factory) 
    {
        _usuario = factory.RecuperarUsuario();
        _senha = factory.RecuperarSenha();
    
    }

    [Fact]
    public async Task Validar_Sucesso()
    {
        var req = new RequisicaoLoginJson
        {
            Email = _usuario.Email,
            Senha = _senha
        };

        var resposta = await PostRequest(METODO, req);

        resposta.StatusCode.Should().Be(HttpStatusCode.OK);

        await using var respostaBody = await resposta.Content.ReadAsStreamAsync();

        var respostaJson = await JsonDocument.ParseAsync(respostaBody);

        respostaJson.RootElement.GetProperty("token").GetString().Should().NotBeNullOrWhiteSpace();
        respostaJson.RootElement.GetProperty("nome").GetString().Should().NotBeNullOrWhiteSpace().And.Be(_usuario.Nome);
    }

    [Fact]
    public async Task Validar_Erro_Senha_Invalida()
    {
        var req = new RequisicaoLoginJson
        {
            Email = _usuario.Email,
            Senha = "senhaInvalidaX"
        };

        var resposta = await PostRequest(METODO, req);

        resposta.StatusCode.Should().Be(HttpStatusCode.Unauthorized);

        await using var respostaBody = await resposta.Content.ReadAsStreamAsync();

        var respostaJson = await JsonDocument.ParseAsync(respostaBody);

        var erros = respostaJson.RootElement.GetProperty("mensagens").EnumerateArray();

        erros.Should().ContainSingle().And.Contain(e => e.GetString().Equals(ResourceMensagensDeErro.LOGIN_INVALIDO));
    }

    [Fact]
    public async Task Validar_Erro_Email_Invalido()
    {
        var req = new RequisicaoLoginJson
        {
            Email = "emailInvalido",
            Senha = _senha
        };

        var resposta = await PostRequest(METODO, req);

        resposta.StatusCode.Should().Be(HttpStatusCode.Unauthorized);

        await using var respostaBody = await resposta.Content.ReadAsStreamAsync();

        var respostaJson = await JsonDocument.ParseAsync(respostaBody);

        var erros = respostaJson.RootElement.GetProperty("mensagens").EnumerateArray();

        erros.Should().ContainSingle().And.Contain(e => e.GetString().Equals(ResourceMensagensDeErro.LOGIN_INVALIDO));
    }

    [Fact]
    public async Task Validar_Erro_Email_Senha_Invalidos()
    {
        var req = new RequisicaoLoginJson
        {
            Email = "emailInvalido",
            Senha = "senhaInvalidaX"
        };

        var resposta = await PostRequest(METODO, req);

        resposta.StatusCode.Should().Be(HttpStatusCode.Unauthorized);

        await using var respostaBody = await resposta.Content.ReadAsStreamAsync();

        var respostaJson = await JsonDocument.ParseAsync(respostaBody);

        var erros = respostaJson.RootElement.GetProperty("mensagens").EnumerateArray();

        erros.Should().ContainSingle().And.Contain(e => e.GetString().Equals(ResourceMensagensDeErro.LOGIN_INVALIDO));
    }
}
