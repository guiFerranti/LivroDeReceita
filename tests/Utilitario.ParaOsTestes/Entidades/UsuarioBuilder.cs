using Bogus;
using MeuLivroDeReceitas.Application.Servicos.Criptografia;
using MeuLivroDeReceitas.Comunicacao.Request;
using MeuLivroDeReceitas.Domain.Entidades;
using Utilitario.ParaOsTestes.Criptografia;

namespace Utilitario.ParaOsTestes.Entidades;

public class UsuarioBuilder
{
    public static (Usuario usuario, string senha) Construir()
    {
        string senha = string.Empty;

        var usuario = new Faker<Usuario>()
            .RuleFor(r => r.Id, _ => 1)
            .RuleFor(r => r.Nome, n => n.Name.FullName())
            .RuleFor(r => r.Email, n => n.Internet.Email())
            .RuleFor(r => r.Senha, n => 
            {
                senha = n.Internet.Password();
                return EncriptadorDeSenhaBuilder.Instancia().Criptografar(senha);
            })
            .RuleFor(r => r.Telefone, n => n.Phone.PhoneNumber("## ! ####-####").Replace("!", $"{n.Random.Int(min: 1, max: 9)}"));

        return (usuario, senha);
    }
}
