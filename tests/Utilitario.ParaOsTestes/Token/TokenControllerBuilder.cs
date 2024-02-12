using MeuLivroDeReceitas.Application.Servicos.Token;

namespace Utilitario.ParaOsTestes.Token;

public class TokenControllerBuilder
{
    public static TokenController Instancia()
    {
        return new TokenController(1000, "ZnBmMnRTfkwnRWN3MEw1W1UlIkYheVxRUj9DITsxaWpBY2lnXyMyQn5C");
    }
}
