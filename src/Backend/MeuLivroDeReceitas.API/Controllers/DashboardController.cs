using MeuLivroDeReceitas.API.Filtros;
using MeuLivroDeReceitas.Application.UseCases.Dashboard;
using MeuLivroDeReceitas.Comunicacao.Request;
using MeuLivroDeReceitas.Comunicacao.Response;
using Microsoft.AspNetCore.Mvc;

namespace MeuLivroDeReceitas.API.Controllers;

public class DashboardController : MeuLivroDeReceitasController
{


    [HttpPut]
    [ProducesResponseType(typeof(RespostaDashboardJson), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ServiceFilter(typeof(UsuarioAutenticadoAttribute))]
    public async Task<IActionResult> RecuperarDashboard(
        [FromServices] IDashboardUseCase useCase,
        [FromBody] RequisicaoDashboardJson requisicao)
    {
        var receitas = await useCase.Executar(requisicao);

        if(receitas.Receitas.Count != 0)
        {
            return Ok(receitas);
        }

        return NoContent();
    }

    


}
