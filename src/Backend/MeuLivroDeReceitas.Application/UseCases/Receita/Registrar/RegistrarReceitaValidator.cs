using FluentValidation;
using MeuLivroDeReceitas.Comunicacao.Request;

namespace MeuLivroDeReceitas.Application.UseCases.Receita.Registrar;

public class RegistrarReceitaValidator : AbstractValidator<RequisicaoRegistrarReceitaJson>
{
    public RegistrarReceitaValidator()
    {
        RuleFor(c => c.Titulo).NotEmpty();
        RuleFor(c => c.Categoria).IsInEnum();
        RuleFor(c => c.ModoPreparo).NotEmpty();
        RuleFor(c => c.Ingredientes).NotEmpty();
        RuleForEach(c => c.Ingredientes).ChildRules(ingrediente =>
        {
            ingrediente.RuleFor(c => c.Produto).NotEmpty();
            ingrediente.RuleFor(c => c.Quantidade).NotEmpty();
        });

        RuleFor(c => c.Ingredientes).Custom((ingredientes, contexto) =>
        {
            var produtosDistintos = ingredientes.Select(c => c.Produto).Distinct();
            if (produtosDistintos.Count() != ingredientes.Count)
            {
                contexto.AddFailure(new FluentValidation.Results.ValidationFailure("Ingredientes", ""));
            }
        });
    }
}
