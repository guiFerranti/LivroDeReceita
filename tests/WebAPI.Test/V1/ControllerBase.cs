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
}
