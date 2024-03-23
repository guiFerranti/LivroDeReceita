using MeuLivroDeReceitas.Comunicacao.Request;
using MeuLivroDeReceitas.Comunicacao.Response;

namespace MeuLivroDeReceitas.Application.UseCases.Dashboard;

public interface IDashboardUseCase
{
    Task<RespostaDashboardJson> Executar(RequisicaoDashboardJson requisicao);
}
