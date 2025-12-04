using Microsoft.Maui.Hosting;
using Microsoft.Extensions.Logging;
using Refit;
using DeckBrew.Mobile.Services;

namespace DeckBrew.Mobile
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder.UseMauiApp<App>();

            var apiUrl = Environment.GetEnvironmentVariable("DECKBREW_API_URL") ?? "http://localhost:8080";
            builder.Services.AddRefitClient<IDeckbrewApi>()
                   .ConfigureHttpClient(c => c.BaseAddress = new Uri(apiUrl));

#if DEBUG
            builder.Logging.AddDebug();
#endif
            return builder.Build();
        }
    }
}
