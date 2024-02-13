using FluentValidation;
using MeuLivroDeReceitas.Comunicacao.Request;
using MeuLivroDeReceitas.Exceptions;

namespace MeuLivroDeReceitas.Application.UseCases.Usuario;

public class SenhaValidator : AbstractValidator<string>
{
    public SenhaValidator()
    {
        RuleFor(senha => senha).NotEmpty().WithMessage(ResourceMensagensDeErro.SENHA_USER_VAZIO);
        When(senha => !string.IsNullOrWhiteSpace(senha), () =>
        {
            RuleFor(senha => senha.Length).GreaterThanOrEqualTo(6).WithMessage(ResourceMensagensDeErro.SENHA_USER_INVALIDO);
        });
    }


}
