﻿using MeuLivroDeReceitas.Application.Servicos.Criptografia;

namespace Utilitario.ParaOsTestes.Criptografia;

public class EncriptadorDeSenhaBuilder
{
    public static EncriptadorDeSenha  Instancia()
    {
        return new EncriptadorDeSenha("SENHA-ADICIONAL-TEST");
    }

}
