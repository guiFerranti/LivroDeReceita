using MeuLivroDeReceitas.Comunicacao.Request;
using MeuLivroDeReceitas.Comunicacao.Response;

namespace MeuLivroDeReceitas.Application.UseCases.Usuario.Registrar;

public interface IRegistrarUsuarioUseCase
{
    Task<RespostaUsuarioRegistradoJson> Executar(RequisicaoRegistrarUsuarioJson requisicao);
}
