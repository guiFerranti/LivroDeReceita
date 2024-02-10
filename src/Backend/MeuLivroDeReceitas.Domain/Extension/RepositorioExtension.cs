using Microsoft.Extensions.Configuration;

namespace MeuLivroDeReceitas.Domain.Extension;

public static class RepositorioExtension
{
    public static string GetNomeDatabase(this IConfiguration configManager)
    {
        var nomeDb = configManager.GetConnectionString("DbName");

        return nomeDb;
    }

    public static string GetConexao(this IConfiguration configManager)
    {
        var conexao = configManager.GetConnectionString("Conexao");

        return conexao;
    }

    public static string GetConexaoCompleta(this IConfiguration configManager)
    {
        var nomeDb = configManager.GetNomeDatabase();
        var conexao = configManager.GetConexao();

        return $"{conexao}Database={nomeDb}";
    }


}
