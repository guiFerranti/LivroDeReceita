using FluentAssertions;
using System.Net;
using Utilitario.ParaOsTestes.Requisicoes;
using Utilitario.ParaOsTestes.Token;
using Xunit;

namespace WebAPI.Test.V1.Usuario.AlterarSenha;

public class AlterarSenhaTokenInvalido : ControllerBase
{
    private const string METODO = "usuario/alterar-senha";

    private MeuLivroDeReceitas.Domain.Entidades.Usuario _usuario;
    private string _senha;


    public AlterarSenhaTokenInvalido(MeuLivroDeReceitaWebApplicationFactory<Program> factory) : base(factory)
    {
        _usuario = factory.RecuperarUsuario();
        _senha = factory.RecuperarSenha();

    }

    [Fact]
    public async Task Validar_Erro_Token_Vazio()
    {
        var token = string.Empty;

        var req = RequisicaoAlterarSenhaUsuarioBuilder.Construir();
        req.SenhaAtual = _senha;

        var resposta = await PutRequest(METODO, req, token);

        resposta.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }


    [Fact]
    public async Task Validar_Erro_TokenValido_Usuario_NaoEncontrado()
    {
        var token = TokenControllerBuilder.Instancia().GerarToken("usuario@notexist.com");

        var req = RequisicaoAlterarSenhaUsuarioBuilder.Construir();
        req.SenhaAtual = _senha;

        var resposta = await PutRequest(METODO, req, token);

        resposta.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }
    
    [Fact]
    public async Task Validar_Erro_Token_Expirado()
    {
        var token = TokenControllerBuilder.InstanciaExpirada().GerarToken(_usuario.Email);

        Thread.Sleep(1000);

        var req = RequisicaoAlterarSenhaUsuarioBuilder.Construir();
        req.SenhaAtual = _senha;

        var resposta = await PutRequest(METODO, req, token);

        resposta.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }


}
