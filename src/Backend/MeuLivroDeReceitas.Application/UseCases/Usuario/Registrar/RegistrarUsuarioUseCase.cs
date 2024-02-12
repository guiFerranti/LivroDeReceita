using AutoMapper;
using MeuLivroDeReceitas.Application.Servicos.Criptografia;
using MeuLivroDeReceitas.Application.Servicos.Token;
using MeuLivroDeReceitas.Comunicacao.Request;
using MeuLivroDeReceitas.Comunicacao.Response;
using MeuLivroDeReceitas.Domain.Repositorios;
using MeuLivroDeReceitas.Exceptions;
using MeuLivroDeReceitas.Exceptions.ExceptionsBase;

namespace MeuLivroDeReceitas.Application.UseCases.Usuario.Registrar;

public class RegistrarUsuarioUseCase : IRegistrarUsuarioUseCase
{
    private readonly IUsuarioReadOnlyRepositorio _usuarioRepositorioReadOnly;
    private readonly IUsuarioWriteOnlyRepositorio _repositorio;
    private readonly IMapper _mapper;
    private readonly IUnidadeDeTrabalho _unidadeDeTrabalho;
    private readonly EncriptadorDeSenha _encriptadorDeSenha;
    private readonly TokenController _tokenController;

    public RegistrarUsuarioUseCase(IUsuarioReadOnlyRepositorio usuarioRepositorioReadOnly, IUsuarioWriteOnlyRepositorio repositorio, IMapper mapper, IUnidadeDeTrabalho unidadeDeTrabalho, EncriptadorDeSenha encriptadorDeSenha, TokenController tokenController)
    {
        _usuarioRepositorioReadOnly = usuarioRepositorioReadOnly;
        _repositorio = repositorio;
        _mapper = mapper;
        _unidadeDeTrabalho = unidadeDeTrabalho;
        _encriptadorDeSenha = encriptadorDeSenha;
        _tokenController = tokenController;
    }

    public async Task<RespostaUsuarioRegistradoJson> Executar(RequestRegistrarUsuarioJson requisicao)
    {
        await Validar(requisicao);

        var user = _mapper.Map<Domain.Entidades.Usuario>(requisicao);
        user.Senha = _encriptadorDeSenha.Criptografar(requisicao.Senha);

        await _repositorio.Adicionar(user);

        await _unidadeDeTrabalho.Commit();

        var token = _tokenController.GerarToken(requisicao.Email);

        return new RespostaUsuarioRegistradoJson
        {
            Token = token,
        };
    }

    private async Task Validar(RequestRegistrarUsuarioJson requisicao)
    {
        var validator = new RegistrarUsuarioValidator();
        var result = validator.Validate(requisicao);

        var existeUsuario = await _usuarioRepositorioReadOnly.ExisteUsuarioEmail(requisicao.Email);

        if (existeUsuario)
        {
            result.Errors.Add(new FluentValidation.Results.ValidationFailure("email", ResourceMensagensDeErro.EMAIL_REPETIDO));
        }

        if (!result.IsValid)
        {
            var msgsErro = result.Errors.Select(error => error.ErrorMessage).ToList();

            throw new ErrosDeValidacaoException(msgsErro);
        }
    }

}
