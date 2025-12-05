using Microsoft.Maui.Hosting;
using Microsoft.Maui.Controls.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Refit;
using DeckBrew.Mobile.Services;
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

            var apiUrl = Environment.GetEnvironmentVariable("DECKBREW_API_URL") ?? "http://localhost:8100";
            builder.Services.AddRefitClient<IDeckbrewApi>()
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
