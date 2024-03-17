using FluentValidation;
using MeuLivroDeReceitas.Comunicacao.Request;
using MeuLivroDeReceitas.Exceptions;
using System.Text.RegularExpressions;

namespace MeuLivroDeReceitas.Application.UseCases.Usuario.Registrar;

public class RegistrarUsuarioValidator : AbstractValidator<RequisicaoRegistrarUsuarioJson>
{
    public RegistrarUsuarioValidator()
    {
        RuleFor(r => r.Nome).NotEmpty().WithMessage(ResourceMensagensDeErro.NOME_USER_VAZIO);

        RuleFor(r => r.Telefone).NotEmpty().WithMessage(ResourceMensagensDeErro.TELEFONE_USER_VAZIO);
        When(r => !string.IsNullOrWhiteSpace(r.Telefone), () =>
        {
            RuleFor(r => r.Telefone).Custom((telefone, contexto) =>
            {
                string telefonePattern = "[0-9]{2} [1-9]{1} [0-9]{4}-[0-9]{4}";
                TimeSpan timeout = TimeSpan.FromMilliseconds(500);
                var result = Regex.IsMatch(telefone, telefonePattern, RegexOptions.None, timeout);

                if (!result) {
                    contexto.AddFailure(new FluentValidation.Results.ValidationFailure(nameof(telefone), ResourceMensagensDeErro.TELEFONE_USER_INVALIDO));
                }
            });
        });

        RuleFor(r => r.Email).NotEmpty().WithMessage(ResourceMensagensDeErro.EMAIL_USER_VAZIO);
        When(r => !string.IsNullOrWhiteSpace(r.Email), () =>
        {
            RuleFor(r => r.Email).EmailAddress().WithMessage(ResourceMensagensDeErro.EMAIL_USER_INVALIDO);
        });

        RuleFor(c => c.Senha).SetValidator(new SenhaValidator());
    }
}
