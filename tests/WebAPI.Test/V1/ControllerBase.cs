using FluentAssertions;
using MeuLivroDeReceitas.Comunicacao.Response;
using MeuLivroDeReceitas.Domain.Entidades;
using MeuLivroDeReceitas.Exceptions;
using System.Globalization;
using System.Text.Json;
using Xunit;

namespace WebAPI.Test.V1;

public class ControllerBase : IClassFixture<MeuLivroDeReceitaWebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public ControllerBase(MeuLivroDeReceitaWebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();

        ResourceMensagensDeErro.Culture = CultureInfo.CurrentCulture;
    }

    protected async Task<HttpResponseMessage> PostRequest(string metodo, object body)
    {
        var jsonString = JsonSerializer.Serialize(body);

        return await _client.PostAsync(metodo, new StringContent(jsonString, System.Text.Encoding.UTF8, "application/json"));
    }
    
    protected async Task<HttpResponseMessage> PutRequest(string metodo, object body, string token = "")
    {
        AutorizarRequisicao(token);

        var jsonString = JsonSerializer.Serialize(body);

        return await _client.PutAsync(metodo, new StringContent(jsonString, System.Text.Encoding.UTF8, "application/json"));
    }

    protected async Task<string> Login(string email, string senha)
    {
        var req = new RequisicaoLoginJson
        {
            Email = email,
            Senha = senha
        };

        var resposta = await PostRequest("login", req);

        await using var respostaBody = await resposta.Content.ReadAsStreamAsync();

        var respostaJson = await JsonDocument.ParseAsync(respostaBody);

        return respostaJson.RootElement.GetProperty("token").GetString();
    }

    private void AutorizarRequisicao(string token)
    {
        if (!string.IsNullOrEmpty(token))
        {
            _client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");


        }
    }
}
