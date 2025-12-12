using Microsoft.Maui.Hosting;
using Microsoft.Maui.Controls.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Refit;
using DeckBrew.Contracts; // Usar el contrato compartido
using System;
using System.Net.Http;

namespace DeckBrew.Mobile
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder.UseMauiApp<App>();

            var apiUrl = Environment.GetEnvironmentVariable("DECKBREW_API_URL")
#if ANDROID
    ?? "http://10.0.2.2:8100";  // IP especial del emulador que apunta a localhost de la PC
#else
    ?? "http://localhost:8100";
#endif
            
            // Usar la interfaz del contrato compartido
            builder.Services.AddRefitClient<IDeckBrewApi>()
                   .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler())
                   .ConfigureHttpClient(c => c.BaseAddress = new Uri(apiUrl));

            builder.Services.AddTransient<Views.HomePage>();
#if DEBUG
            builder.Logging.AddDebug();
#endif
            return builder.Build();
        }
    }
}
