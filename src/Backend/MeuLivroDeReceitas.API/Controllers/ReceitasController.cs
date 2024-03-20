using MeuLivroDeReceitas.API.Filtros;
using MeuLivroDeReceitas.Application.UseCases.Receita.Registrar;
using MeuLivroDeReceitas.Comunicacao.Request;
using MeuLivroDeReceitas.Comunicacao.Response;
using Microsoft.AspNetCore.Mvc;

namespace MeuLivroDeReceitas.API.Controllers;

[ServiceFilter(typeof(UsuarioAutenticadoAttribute))]
public class ReceitasController : MeuLivroDeReceitasController
{

    [HttpPost]
    [ProducesResponseType(typeof(RespostaReceitaJson), StatusCodes.Status201Created)]
    public async Task<IActionResult> Registrar([FromServices] IRegistrarReceitaUseCase useCase, [FromBody] RequisicaoRegistrarReceitaJson requisicao)
    {

        var resposta = await useCase.Executar(requisicao);

        return Created(string.Empty, resposta);
    }
}
