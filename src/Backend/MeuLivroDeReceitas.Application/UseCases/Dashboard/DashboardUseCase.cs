﻿using AutoMapper;
using MeuLivroDeReceitas.Application.Servicos.UsuarioLogado;
using MeuLivroDeReceitas.Comunicacao.Request;
using MeuLivroDeReceitas.Comunicacao.Response;
using MeuLivroDeReceitas.Domain.Repositorios.Receita;

namespace MeuLivroDeReceitas.Application.UseCases.Dashboard;

public class DashboardUseCase : IDashboardUseCase
{
    private readonly IReceitaReadOnlyRepositorio _repositorio;
    private readonly IUsuarioLogado _usuarioLogado;
    private readonly IMapper _mapper;

    public DashboardUseCase(IReceitaReadOnlyRepositorio repositorio, IUsuarioLogado usuarioLogado, IMapper mapper)
    {
        _repositorio = repositorio;
        _usuarioLogado = usuarioLogado;
        _mapper = mapper;
    }


    public async Task<RespostaDashboardJson> Executar(RequisicaoDashboardJson requisicao)
    {
        var usuarioLogado = _usuarioLogado.RecuperarUsuario();

        var receitas = await _repositorio.RecuperarTodasDoUsuario(usuarioLogado.Id);

        receitas = Filtrar(requisicao, receitas);

        return new RespostaDashboardJson
        {
            Receitas = _mapper.Map<List<RespostaReceitaDashboardJson>>(receitas)
        };
    }

    private static IList<Domain.Entidades.Receita> Filtrar(RequisicaoDashboardJson requisicao, IList<Domain.Entidades.Receita> receitas)
    {
        var receitasFiltradas = receitas;

        if (requisicao.Categoria.HasValue)
        {
            receitasFiltradas = receitas.Where(r => r.Categoria == (Domain.Enum.Categoria)requisicao.Categoria.Value).ToList();
        }

        if (!string.IsNullOrWhiteSpace(requisicao.TituloOuIngrediente))
        {
            receitasFiltradas = receitas.Where(r => r.Titulo.Contains(requisicao.TituloOuIngrediente) || r.Ingredientes.Any(i => i.Produto.Contains(requisicao.TituloOuIngrediente))).ToList();
        }

        return receitasFiltradas;
    }

}
