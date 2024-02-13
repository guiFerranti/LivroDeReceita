using FluentValidation;
using MeuLivroDeReceitas.Comunicacao.Request;

namespace MeuLivroDeReceitas.Application.UseCases.Usuario.AlterarSenha;

public class AlterarSenhaValidator : AbstractValidator<RequisicaoAlterarSenha>
{
    public AlterarSenhaValidator()
    {
        RuleFor(u => u.SenhaNova).SetValidator(new SenhaValidator());
    }
}
