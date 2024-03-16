using FluentAssertions;
using MeuLivroDeReceitas.Application.UseCases.Usuario.AlterarSenha;
using MeuLivroDeReceitas.Application.UseCases.Usuario.Registrar;
using MeuLivroDeReceitas.Comunicacao.Request;
using MeuLivroDeReceitas.Exceptions;
using System.Reflection.Metadata;
using Utilitario.ParaOsTestes.Requisicoes;
using Xunit;

namespace Validators.Test.Usuario.AlterarSenha;

public class AlterarSenhaValidatorTest
{
    [Fact]
    public void Validar_Sucesso()
    {
        var validator = new AlterarSenhaValidator();

        var req = RequisicaoAlterarSenhaUsuarioBuilder.Construir();

        var result = validator.Validate(req);

        result.IsValid.Should().BeTrue();
    }
    
    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(5)]
    public void Validar_Senha_Nova_Invalida(int tamanhoSenha)
    {
        var validator = new AlterarSenhaValidator();

        var req = RequisicaoAlterarSenhaUsuarioBuilder.Construir(tamanhoSenha);

        var result = validator.Validate(req);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(error => error.ErrorMessage.Equals(ResourceMensagensDeErro.SENHA_USER_INVALIDO));
    }

    [Fact]
    public void Validar_Senha_Nova_Vazio()
    {
        var validator = new AlterarSenhaValidator();

        var req = RequisicaoAlterarSenhaUsuarioBuilder.Construir();
        req.SenhaNova = string.Empty;

        var result = validator.Validate(req);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(error => error.ErrorMessage.Equals(ResourceMensagensDeErro.SENHA_USER_VAZIO));
    }

}
