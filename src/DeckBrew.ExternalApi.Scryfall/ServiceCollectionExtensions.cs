using Microsoft.Extensions.DependencyInjection;
using Refit;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DeckBrew.ExternalApi.Scryfall;

public static class ServiceCollectionExtensions
{
    private const string ScryfallBaseUrl = "https://api.scryfall.com";

    /// <summary>
    /// Registers Scryfall API services in the dependency injection container
    /// </summary>
    public static IServiceCollection AddScryfallServices(this IServiceCollection services)
    {
        var refitSettings = new RefitSettings
        {
            ContentSerializer = new SystemTextJsonContentSerializer(new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                PropertyNameCaseInsensitive = true
            })
        };

        services.AddRefitClient<IScryfallApi>(refitSettings)
            .ConfigureHttpClient(c =>
            {
                c.BaseAddress = new Uri(ScryfallBaseUrl);
                c.DefaultRequestHeaders.Add("User-Agent", "DeckBrew/1.0");
                c.Timeout = TimeSpan.FromSeconds(30);
            });

        services.AddHttpClient("ScryfallImages", c =>
        {
            c.DefaultRequestHeaders.Add("User-Agent", "DeckBrew/1.0");
            c.Timeout = TimeSpan.FromSeconds(60);
        });

        services.AddScoped<IScryfallService>(sp =>
        {
            var api = sp.GetRequiredService<IScryfallApi>();
            var httpClientFactory = sp.GetRequiredService<IHttpClientFactory>();
            var httpClient = httpClientFactory.CreateClient("ScryfallImages");
            return new ScryfallService(api, httpClient);
        });

        return services;
    }
}
