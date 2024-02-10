using FluentMigrator.Builders.Create.Table;
using FluentMigrator.Expressions;
using System.Reflection.Emit;

namespace MeuLivroDeReceitas.Infrastructure.Migrations.Versions;

public static class BaseVersion
{
    public static ICreateTableColumnOptionOrWithColumnSyntax InserirTabelaBase(ICreateTableWithColumnOrSchemaOrDescriptionSyntax tabela)
    {
        return tabela
            .WithColumn("Id").AsInt64().PrimaryKey().Identity()
            .WithColumn("DataCriacao").AsDateTime().NotNullable();
    }
}
