using FluentAssertions;
using MeuLivroDeReceitas.Exceptions;
using Microsoft.AspNetCore.Server.Kestrel.Core.Features;
using System.Net;
using System.Text.Json;
using Utilitario.ParaOsTestes.Requisicoes;
using Xunit;

namespace WebAPI.Test.V1.Usuario.Registrar;

public class RegistrarUsuarioTeste : ControllerBase
{
    private const string METODO = "usuario";

    public RegistrarUsuarioTeste(MeuLivroDeReceitaWebApplicationFactory<Program> factory) : base(factory) { }

    [Fact]
    public async Task Validar_Sucesso()
    {
        var req = RequisicaoRegistrarUsuarioBuilder.Construir();

        var resposta = await PostRequest(METODO, req);

        resposta.StatusCode.Should().Be(HttpStatusCode.Created);

        await using var respostaBody = await resposta.Content.ReadAsStreamAsync();

        var respostaJson = await JsonDocument.ParseAsync(respostaBody);

        respostaJson.RootElement.GetProperty("token").GetString().Should().NotBeNullOrWhiteSpace();
    }

    [Fact]
    public async Task Validar_Erro_Nome_Vazio()
    {
        var req = RequisicaoRegistrarUsuarioBuilder.Construir();
        req.Nome = string.Empty;

        var resposta = await PostRequest(METODO, req);

        resposta.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        await using var respostaBody = await resposta.Content.ReadAsStreamAsync();

        var respostaJson = await JsonDocument.ParseAsync(respostaBody);

        var erros = respostaJson.RootElement.GetProperty("mensagens").EnumerateArray();
        erros.Should().ContainSingle().And.Contain(c => c.GetString().Equals(ResourceMensagensDeErro.NOME_USER_VAZIO));
    }


}
