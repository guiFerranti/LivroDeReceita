using MeuLivroDeReceitas.API.Filtros;
using MeuLivroDeReceitas.Application.UseCases.Usuario.AlterarSenha;
using MeuLivroDeReceitas.Application.UseCases.Usuario.Registrar;
using MeuLivroDeReceitas.Comunicacao.Request;
using MeuLivroDeReceitas.Comunicacao.Response;
using MeuLivroDeReceitas.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace MeuLivroDeReceitas.API.Controllers;

public class UsuarioController : MeuLivroDeReceitasController
{

    [HttpPost]
    [ProducesResponseType(typeof(RespostaUsuarioRegistradoJson), StatusCodes.Status201Created)]
    public async Task<IActionResult> RegistrarUsuario(
        [FromServices] IRegistrarUsuarioUseCase useCase,
        [FromBody] RequisicaoRegistrarUsuarioJson user)
    {
        var result = await useCase.Executar(user);

        return Created(string.Empty, result);
    }

    [HttpPut]
    [Route("alterar-senha")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ServiceFilter(typeof(UsuarioAutenticadoAttribute))]
    public async Task<IActionResult> AlterarSenha(
        [FromServices] IAlterarSenhaUseCase useCase,
        [FromBody] RequisicaoAlterarSenhaJson requisicao)
    {
        await useCase.Executar(requisicao);

        return NoContent();
    }

    


}
