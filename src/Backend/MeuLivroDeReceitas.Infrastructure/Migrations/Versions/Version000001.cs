using FluentMigrator;

namespace MeuLivroDeReceitas.Infrastructure.Migrations.Versions;

[Migration((long)NumeroVersoes.CriarTabelaUsuario, "Criar tabela usuario")]
public class Version000001 : Migration
{
    public override void Down() {}

    public override void Up()
    {
        var tabela = Create.Table("User");
        BaseVersion.InserirTabelaBase(tabela);


        tabela
            .WithColumn("Name").AsString(100).NotNullable()
            .WithColumn("Email").AsString(100).NotNullable()
            .WithColumn("Senha").AsString(2000).NotNullable()
            .WithColumn("Telefone").AsString(14).NotNullable();
    }
}
