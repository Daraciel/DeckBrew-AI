using DeckBrew.Contracts.DTOs;
using Refit;
using System.Threading;
using System.Threading.Tasks;

namespace DeckBrew.Contracts;

/// <summary>
/// Contract for DeckBrew API
/// This interface defines all available endpoints and serves as the contract
/// between the API implementation and the MAUI client.
/// </summary>
public interface IDeckBrewApi
{
    /// <summary>
    /// Generates a new deck based on the provided parameters
    /// </summary>
    [Post("/v1/generate")]
    Task<GenerateDeckResponse> GenerateDeckAsync([Body] GenerateDeckRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets a deck by its ID
    /// </summary>
    [Get("/v1/decks/{id}")]
    Task<DeckDto> GetDeckAsync(string id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a deck by its ID
    /// </summary>
    [Delete("/v1/decks/{id}")]
    Task DeleteDeckAsync(string id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Health check endpoint
    /// </summary>
    [Get("/v1/health")]
    Task<HealthResponse> GetHealthAsync(CancellationToken cancellationToken = default);
}