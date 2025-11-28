using NotesApp.Services;
using System.Globalization;

namespace NotesApp.CustomMiddleware;

public class CustomNotesLocalizationMiddleware
{
    private readonly RequestDelegate _next;

    public CustomNotesLocalizationMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var authNotesService = context.RequestServices.GetRequiredService<IAuthNotesService>();
        if (authNotesService.IsAuthenticated())
        {
            var language = authNotesService.GetLanguage();
            CultureInfo culture;
            switch (language)
            {
                case Enum.Language.English:
                    culture = new CultureInfo("En");
                    break;
                case Enum.Language.Russian:
                    culture = new CultureInfo("Ru");
                    break;
                default:
                    throw new ArgumentException($"Unknown language {language}");
            }

            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;
        }
        else
        {
            //ru-RU,ru;q=0.9,en-US;q=0.8,en;q=0.7,la;q=0.6
            var h = context.Request.Headers["accept-language"];
        }

        // Before main action check
        //OnActionExecuting

        // call next service
        await _next.Invoke(context);

        //OnActionExecuted
        // After main action check
    }
}