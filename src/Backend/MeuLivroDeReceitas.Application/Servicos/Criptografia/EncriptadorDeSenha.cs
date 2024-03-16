using System.Security.Cryptography;
using System.Text;

namespace MeuLivroDeReceitas.Application.Servicos.Criptografia;

public class EncriptadorDeSenha
{
    private readonly string _chaveAdicional;

    public EncriptadorDeSenha(string chaveAdicional)
    {
        _chaveAdicional = chaveAdicional;
    }

    public string Criptografar(string senha)
    {
        var senhaAppendChave = $"{senha}{_chaveAdicional}";

        var bytes = Encoding.UTF8.GetBytes(senhaAppendChave);
        byte[] hashBytes = SHA512.HashData(bytes);

        return StringBytes(hashBytes);
    }

    private static string StringBytes(byte[] hashBytes)
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
