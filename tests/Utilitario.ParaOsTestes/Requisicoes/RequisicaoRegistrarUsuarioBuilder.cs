using Bogus;
using MeuLivroDeReceitas.Comunicacao.Request;

namespace Utilitario.ParaOsTestes.Requisicoes;

public class RequisicaoRegistrarUsuarioBuilder
{
    public static RequisicaoRegistrarUsuarioJson Construir(int tamanhoSenha = 10)
    {
        return new Faker<RequisicaoRegistrarUsuarioJson>()
            .RuleFor(r => r.Nome, n => n.Person.FullName)
            .RuleFor(r => r.Email, n => n.Internet.Email())
            .RuleFor(r => r.Senha, n => n.Internet.Password(tamanhoSenha))
            .RuleFor(r => r.Telefone, n => n.Phone.PhoneNumber("## ! ####-####").Replace("!", $"{n.Random.Int(min: 1, max: 9)}"));
    }
}
