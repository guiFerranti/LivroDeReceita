﻿using System.Security.Cryptography.X509Certificates;

namespace MeuLivroDeReceitas.Comunicacao.Request;

public class RequisicaoRegistrarUsuarioJson
{
    public string Nome { get; set; }
    public string Email { get; set; }
    public string Senha { get; set; }
    public string Telefone { get; set; }
}
