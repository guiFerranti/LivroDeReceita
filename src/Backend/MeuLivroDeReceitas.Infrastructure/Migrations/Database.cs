using Dapper;
using MySqlConnector;

namespace MeuLivroDeReceitas.Infrastructure.Migrations;

public static class Database
{
    public static void CriarDatabase(string conexaoString, string nomeDb)
    {
        using var conexao = new MySqlConnection(conexaoString);

        var parameters = new DynamicParameters();
        parameters.Add("nome", nomeDb);

        var reg = conexao.Query("SELECT * FROM INFORMATION_SCHEMA.SCHEMATA WHERE SCHEMA_NAME = @nome", parameters);

        if (!reg.Any()) {
            conexao.Execute($"CREATE DATABASE {nomeDb}");
        }
    }
}
