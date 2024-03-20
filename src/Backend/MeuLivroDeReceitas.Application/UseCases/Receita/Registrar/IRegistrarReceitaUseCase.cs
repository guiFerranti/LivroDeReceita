using MeuLivroDeReceitas.Comunicacao.Request;
using MeuLivroDeReceitas.Comunicacao.Response;

namespace MeuLivroDeReceitas.Application.UseCases.Receita.Registrar;

public interface IRegistrarReceitaUseCase
{
    Task<RespostaReceitaJson> Executar(RequisicaoRegistrarReceitaJson requisicao);
}
