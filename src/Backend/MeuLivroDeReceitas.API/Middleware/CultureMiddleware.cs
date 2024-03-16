using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace MeuLivroDeReceitas.API.Middleware;

public class CultureMiddleware
{
    private readonly RequestDelegate _next;

    private readonly IList<string> _idiomasSuportados = new List<string>
    {
        "pt",
        "en"
    };


    public CultureMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        var culture = new CultureInfo("pt");

        if (context.Request.Headers.ContainsKey("Accept-language")) 
        {
            var language = context.Request.Headers.AcceptLanguage.ToString();

            if (_idiomasSuportados.Any(c => c.Equals(language)))
            {
                culture = new CultureInfo(language);
            }
        }

        CultureInfo.CurrentCulture = culture;
        CultureInfo.CurrentUICulture = culture;

        await _next(context);
    }
}
