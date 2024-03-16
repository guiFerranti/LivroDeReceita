using FluentAssertions;
using MeuLivroDeReceitas.Comunicacao.Response;
using MeuLivroDeReceitas.Exceptions;
using System.Net;
using System.Text.Json;
using Utilitario.ParaOsTestes.Requisicoes;
using Xunit;

namespace WebAPI.Test.V1.Usuario.AlterarSenha;

public class AlterarSenhaTeste : ControllerBase
{
    private const string METODO = "usuario/alterar-senha";

    private MeuLivroDeReceitas.Domain.Entidades.Usuario _usuario;
    private string _senha;


    public AlterarSenhaTeste(MeuLivroDeReceitaWebApplicationFactory<Program> factory) : base(factory)
    {
        _usuario = factory.RecuperarUsuario();
        _senha = factory.RecuperarSenha();

    }

    [Fact]
    public async Task Validar_Sucesso()
    {
        var token = await Login(_usuario.Email, _senha);

        var req = RequisicaoAlterarSenhaUsuarioBuilder.Construir();
        req.SenhaAtual = _senha;

        var resposta = await PutRequest(METODO, req, token);

        resposta.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task Validar_Erro_NovaSenhaEmBranco()
    {
        var token = await Login(_usuario.Email, _senha);

        var req = RequisicaoAlterarSenhaUsuarioBuilder.Construir();
        req.SenhaAtual = _senha;
        req.SenhaNova = string.Empty;

        var resposta = await PutRequest(METODO, req, token);

        resposta.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        await using var respostaBody = await resposta.Content.ReadAsStreamAsync();

        var respostaData = await JsonDocument.ParseAsync(respostaBody);

        var erros = respostaData.RootElement.GetProperty("mensagens").EnumerateArray();

        erros.Should().ContainSingle().And.Contain(x => x.GetString().Equals(ResourceMensagensDeErro.SENHA_USER_VAZIO));


    }
}
