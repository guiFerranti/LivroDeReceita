﻿using Bogus;
using MeuLivroDeReceitas.Comunicacao.Request;

namespace Utilitario.ParaOsTestes.Requisicoes;

public class RequisicaoAlterarSenhaUsuarioBuilder
{
    public static RequisicaoAlterarSenhaJson Construir(int tamanhoSenha = 10)
    {
        return new Faker<RequisicaoAlterarSenhaJson>()
            .RuleFor(c => c.SenhaNova, n => n.Internet.Password(tamanhoSenha))
            .RuleFor(c => c.SenhaAtual, n => n.Internet.Password(10));
    }
}
