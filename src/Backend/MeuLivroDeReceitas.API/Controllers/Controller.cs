using MeuLivroDeReceitas.Application.UseCases.Usuario.Registrar;
using MeuLivroDeReceitas.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace MeuLivroDeReceitas.API.Controllers;

[ApiController]
[Route("teste")]
public class Controller : ControllerBase
{

    [HttpGet]
    public async Task<IActionResult> Get([FromServices] IRegistrarUsuarioUseCase useCase)
    {

        var response = await useCase.Executar(new Comunicacao.Request.RequestRegistrarUsuarioJson
        {
            Email = "abc@gmail.com",
            Nome = "bc",
            Senha = "dasdsa",
            Telefone = "51 9 8026-1313"
            
        });
        return Ok(response);
    }


}
