using System.Globalization;

namespace MyRecipeBook.API.Middleware
{
    public class CultureMiddleware
    {
        private readonly RequestDelegate _next;

        public CultureMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var supportedLanguages = CultureInfo.GetCultures(CultureTypes.AllCultures);
            var requestCulture = context.Request.Headers.AcceptLanguage.FirstOrDefault();
            var cultureInfo= new CultureInfo("pt-BR");

            //Verifica se existe a cultura no header e se ela e suportada pela aplicacao
            if (string.IsNullOrWhiteSpace(requestCulture) == false 
                && supportedLanguages.Any(c => c.Name.Equals(requestCulture)))  
            {
                    cultureInfo = new CultureInfo(requestCulture);
            }

            CultureInfo.CurrentCulture = cultureInfo;
            CultureInfo.CurrentUICulture = cultureInfo;

            //Permite que o fluxo continue. Caso nao tivesse, o fluxo pararia aqui.
            await _next(context);
        }
    }
}
