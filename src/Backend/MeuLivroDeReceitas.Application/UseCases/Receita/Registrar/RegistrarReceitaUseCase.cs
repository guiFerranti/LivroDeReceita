using AutoMapper;
using MeuLivroDeReceitas.Application.Servicos.UsuarioLogado;
using MeuLivroDeReceitas.Comunicacao.Request;
using MeuLivroDeReceitas.Comunicacao.Response;
using MeuLivroDeReceitas.Domain.Repositorios;
using MeuLivroDeReceitas.Domain.Repositorios.Receita;
using MeuLivroDeReceitas.Exceptions.ExceptionsBase;

namespace MeuLivroDeReceitas.Application.UseCases.Receita.Registrar;

public class RegistrarReceitaUseCase : IRegistrarReceitaUseCase
{
    private readonly IMapper _mapper;
    private readonly IUnidadeDeTrabalho _unidadeDeTrabalho;
    readonly private IUsuarioLogado _usuarioLogado;
    private readonly IReceitaWriteOnlyRepositorio _receitaWriteOnlyRepositorio;

    public RegistrarReceitaUseCase(IMapper mapper, IUnidadeDeTrabalho unidadeDeTrabalho, IUsuarioLogado usuarioLogado, IReceitaWriteOnlyRepositorio repositorio)
    {
        _mapper = mapper;
        _unidadeDeTrabalho = unidadeDeTrabalho;
        _usuarioLogado = usuarioLogado;
        _receitaWriteOnlyRepositorio = repositorio;
    }

    public async Task<RespostaReceitaJson> Executar(RequisicaoRegistrarReceitaJson requisicao)
    {
        Validar(requisicao);

        var usuarioLogado = await _usuarioLogado.RecuperarUsuario();

        var receita = _mapper.Map<Domain.Entidades.Receita>(requisicao);
        receita.UsuarioId = usuarioLogado.Id;

        await _receitaWriteOnlyRepositorio.Registrar(receita);

        await _unidadeDeTrabalho.Commit();

        return _mapper.Map<RespostaReceitaJson>(receita);
    }


    private void Validar(RequisicaoRegistrarReceitaJson requisicao)
    {
        var validator = new RegistrarReceitaValidator();
        var resultado = validator.Validate(requisicao);

        if (!resultado.IsValid)
        {
            var mensagensDeErro = resultado.Errors.Select(c => c.ErrorMessage).ToList();
            throw new ErrosDeValidacaoException(mensagensDeErro);
        }
    }
}
