using AutoMapper;
using MeuLivroDeReceitas.Application.Servicos.AutoMapper;

namespace Utilitario.ParaOsTestes.Mapper;

public class MapperBuilder
{
    public static IMapper Instancia()
    {
        var configuracao = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<AutoMapperConfiguracao>();
        });

        return configuracao.CreateMapper();

    }
}
