using MeuLivroDeReceitas.Comunicacao.Enum;

namespace MeuLivroDeReceitas.Comunicacao.Response;

public class RespostaReceitaJson
{
    public string Id { get; set; }
    public string Titulo { get; set; }
    public Categoria Categoria { get; set; }
    public string ModoPreparo { get; set; }
    public ICollection<RespostaIngredientesJson> Ingredientes { get; set; }
}
