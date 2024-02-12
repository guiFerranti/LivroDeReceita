﻿using MeuLivroDeReceitas.Domain.Entidades;
using MeuLivroDeReceitas.Domain.Repositorios;
using Moq;

namespace Utilitario.ParaOsTestes.Repositorios;

public class UsuarioReadOnlyRepositorioBuilder
{
    private static UsuarioReadOnlyRepositorioBuilder _instance;
    private readonly Mock<IUsuarioReadOnlyRepositorio> _repositorio;

    private UsuarioReadOnlyRepositorioBuilder()
    {
        if (_repositorio == null)
        {
            _repositorio = new Mock<IUsuarioReadOnlyRepositorio>();
        }
    }

    public static UsuarioReadOnlyRepositorioBuilder Instancia()
    {
        _instance = new UsuarioReadOnlyRepositorioBuilder();
        return _instance;

    }

    public UsuarioReadOnlyRepositorioBuilder ExisteUsuarioComEmail(string email)
    {
        if (!string.IsNullOrEmpty(email)) _repositorio.Setup(i => i.ExisteUsuarioEmail(email)).ReturnsAsync(true);

        return this;
    }

    public UsuarioReadOnlyRepositorioBuilder Login(Usuario usuario)
    {
        _repositorio.Setup(i => i.Login(usuario.Email, usuario.Senha)).ReturnsAsync(usuario);
        
        
        return this;
    }


    public IUsuarioReadOnlyRepositorio Construir()
    {
        return _repositorio.Object;
    }
}
