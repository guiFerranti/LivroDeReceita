using System.Security.Cryptography;
using System.Text;

namespace MeuLivroDeReceitas.Application.Servicos.Criptografia;

public class EncriptadorDeSenha
{
    private readonly string _chaveDeEncripitacao;

    public EncriptadorDeSenha(string chaveDeEncripitacao)
    {
        _chaveDeEncripitacao = chaveDeEncripitacao;
    }

    public string Criptografar(string senha)
    {
        var senhaAppendChave = $"{senha}{_chaveDeEncripitacao}";

        var bytes = Encoding.UTF8.GetBytes(senhaAppendChave);
        var sha512 = SHA512.Create();
        byte[] hashBytes = sha512.ComputeHash(bytes);

        return StringBytes(hashBytes);
    }

    private string StringBytes(byte[] hashBytes)
    {
        var sb = new StringBuilder();
        foreach (byte b in hashBytes)
        {
            var hex = b.ToString("x2");
            sb.Append(hex);
        }
        return sb.ToString();
    }
}
