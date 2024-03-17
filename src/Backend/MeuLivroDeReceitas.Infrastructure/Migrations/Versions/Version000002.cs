using FluentMigrator;

namespace MeuLivroDeReceitas.Infrastructure.Migrations.Versions;

[Migration((long)NumeroVersoes.CriarTabelaReceitas, "Criar tabela receitas")]
public class Version000002 : Migration
{
    public override void Down() {}

    public override void Up()
    {
        CriarTabelaReceita();
        CriarTabelaIngredientes();

    }

    private void CriarTabelaReceita()
    {
        var tabela = Create.Table("Receitas");
        BaseVersion.InserirTabelaBase(tabela);


        tabela
            .WithColumn("Titulo").AsString(100).NotNullable()
            .WithColumn("Categoria").AsInt16().NotNullable()
            .WithColumn("ModoPreparo").AsString(8000).NotNullable();
    }

    private void CriarTabelaIngredientes()
    {
        var tabela = Create.Table("Ingredientes");
        BaseVersion.InserirTabelaBase(tabela);


        tabela
            .WithColumn("Produto").AsString(100).NotNullable()
            .WithColumn("Quantidade").AsString(100).NotNullable()
            .WithColumn("ReceitaId").AsInt64().NotNullable().ForeignKey("FK_Ingredientes_Receita_Id", "Receitas", "Id");
    }
}
