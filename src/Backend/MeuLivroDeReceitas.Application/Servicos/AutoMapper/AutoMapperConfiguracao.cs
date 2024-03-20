using AutoMapper;
using HashidsNet;
using MeuLivroDeReceitas.Comunicacao.Request;
using MeuLivroDeReceitas.Comunicacao.Response;
using System.Runtime.InteropServices.Marshalling;

namespace MeuLivroDeReceitas.Application.Servicos.AutoMapper;

public class AutoMapperConfiguracao : Profile
{
    private readonly IHashids _hashids;

    public AutoMapperConfiguracao(IHashids hashId)
    {
        _hashids = hashId;

        RequisicaoParaEntidade();
        EntidadeParaRequisicao();
    }

    private void RequisicaoParaEntidade()
    {
        CreateMap<RequisicaoRegistrarUsuarioJson, Domain.Entidades.Usuario>()
           .ForMember(destino => destino.Senha, config => config.Ignore());

        CreateMap<RequisicaoRegistrarReceitaJson, Domain.Entidades.Receita>();
        CreateMap<RequisicaoRegistrarIngredienteJson, Domain.Entidades.Ingrediente>();
    }

    private void EntidadeParaRequisicao()
    {
        CreateMap<Domain.Entidades.Receita, RespostaReceitaJson>()
            .ForMember(destino => destino.Id, config => config.MapFrom(origem => _hashids.EncodeLong(origem.Id)));

        CreateMap<Domain.Entidades.Ingrediente, RespostaIngredientesJson>()
            .ForMember(destino => destino.Id, config => config.MapFrom(origem => _hashids.EncodeLong(origem.Id)));
    }
}
