﻿using AutoMapper;
using MeuLivroDeReceitas.Comunicacao.Request;

namespace MeuLivroDeReceitas.Application.Servicos.AutoMapper;

public class AutoMapperConfiguracao : Profile
{
    public AutoMapperConfiguracao()
    {
        CreateMap<RequestRegistrarUsuarioJson, Domain.Entidades.Usuario>().ReverseMap()
            .ForMember(destino => destino.Senha, config => config.Ignore());
    }
}
