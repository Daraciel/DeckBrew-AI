using DeckBrew.ExternalApi.Scryfall.Models;

namespace DeckBrew.ExternalApi.Scryfall;

/// <summary>
/// Implementation of Scryfall API service
/// </summary>
public class ScryfallService : IScryfallService
{
    private readonly IScryfallApi _api;
    private readonly HttpClient _httpClient;
    private readonly SemaphoreSlim _rateLimitSemaphore;
    private const int RequestsPerSecond = 10;
    private const int RateLimitDelayMs = 100; // 10 requests per second = 100ms between requests

    public ScryfallService(IScryfallApi api, HttpClient httpClient)
    {
        _api = api ?? throw new ArgumentNullException(nameof(api));
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        _rateLimitSemaphore = new SemaphoreSlim(1, 1);
    }

    #region Card Search

    public async Task<IEnumerable<ScryfallCard>> SearchCardsAsync(string query, CancellationToken cancellationToken = default)
    {
        await RateLimitAsync(cancellationToken);
        var result = await _api.SearchCardsAsync(query);
        return result.Data;
    }

    public async Task<IEnumerable<ScryfallCard>> SearchAllCardsAsync(string query, int maxPages = 10, CancellationToken cancellationToken = default)
    {
        var allCards = new List<ScryfallCard>();
        var page = 1;

        await RateLimitAsync(cancellationToken);
        var firstPage = await _api.SearchCardsAsync(query, page: page);
        allCards.AddRange(firstPage.Data);

        while (firstPage.HasMore && page < maxPages)
        {
            page++;
            await RateLimitAsync(cancellationToken);
            var nextPage = await _api.SearchCardsAsync(query, page: page);
            allCards.AddRange(nextPage.Data);
            
            if (!nextPage.HasMore)
                break;
        }

        return allCards;
    }

    public async Task<ScryfallCard?> GetCardByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        try
        {
            await RateLimitAsync(cancellationToken);
            return await _api.GetCardByExactNameAsync(name);
        }
        catch (Refit.ApiException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            // Try fuzzy search as fallback
            try
            {
                await RateLimitAsync(cancellationToken);
                return await _api.GetCardByFuzzyNameAsync(name);
            }
            catch
            {
                return null;
            }
        }
    }

    public async Task<IEnumerable<string>> AutocompleteCardNameAsync(string partialName, CancellationToken cancellationToken = default)
    {
        await RateLimitAsync(cancellationToken);
        var result = await _api.AutocompleteCardNameAsync(partialName);
        return result.Data;
    }

    public async Task<ScryfallCard> GetRandomCardAsync(string? query = null, CancellationToken cancellationToken = default)
    {
        await RateLimitAsync(cancellationToken);
        return await _api.GetRandomCardAsync(query);
    }

    #endregion

    #region Commander Specific

    public async Task<IEnumerable<ScryfallCard>> SearchCommandersAsync(string? colorIdentity = null, CancellationToken cancellationToken = default)
    {
        var query = "is:commander";
        
        if (!string.IsNullOrWhiteSpace(colorIdentity))
        {
            query += $" commander:{colorIdentity.ToLower()}";
        }

        return await SearchAllCardsAsync(query, maxPages: 20, cancellationToken);
    }

    public async Task<IEnumerable<ScryfallCard>> SearchCommanderLegalCardsAsync(string query, CancellationToken cancellationToken = default)
    {
        var commanderQuery = $"{query} f:commander";
        return await SearchCardsAsync(commanderQuery, cancellationToken);
    }

    #endregion

    #region Sets

    public async Task<IEnumerable<ScryfallSet>> GetAllSetsAsync(CancellationToken cancellationToken = default)
    {
        await RateLimitAsync(cancellationToken);
        var result = await _api.GetAllSetsAsync();
        return result.Data;
    }

    public async Task<ScryfallSet?> GetSetByCodeAsync(string code, CancellationToken cancellationToken = default)
    {
        try
        {
            await RateLimitAsync(cancellationToken);
            return await _api.GetSetByCodeAsync(code);
        }
        catch (Refit.ApiException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return null;
        }
    }

    #endregion

    #region Symbology

    public async Task<IEnumerable<ScryfallSymbol>> GetAllSymbolsAsync(CancellationToken cancellationToken = default)
    {
        await RateLimitAsync(cancellationToken);
        var result = await _api.GetAllSymbolsAsync();
        return result.Data;
    }

    public async Task<ManaCost?> ParseManaCostAsync(string cost, CancellationToken cancellationToken = default)
    {
        try
        {
            await RateLimitAsync(cancellationToken);
            return await _api.ParseManaCostAsync(cost);
        }
        catch (Refit.ApiException)
        {
            return null;
        }
    }

    #endregion

    #region Catalogs

    public async Task<IEnumerable<string>> GetAllCardNamesAsync(CancellationToken cancellationToken = default)
    {
        await RateLimitAsync(cancellationToken);
        var result = await _api.GetAllCardNamesAsync();
        return result.Data;
    }

    public async Task<IEnumerable<string>> GetCreatureTypesAsync(CancellationToken cancellationToken = default)
    {
        await RateLimitAsync(cancellationToken);
        var result = await _api.GetCreatureTypesAsync();
        return result.Data;
    }

    public async Task<IEnumerable<string>> GetKeywordAbilitiesAsync(CancellationToken cancellationToken = default)
    {
        await RateLimitAsync(cancellationToken);
        var result = await _api.GetKeywordAbilitiesAsync();
        return result.Data;
    }

    #endregion

    #region Images

    public async Task<Stream> GetCardImageAsync(string imageUri, CancellationToken cancellationToken = default)
    {
        await RateLimitAsync(cancellationToken);
        var response = await _httpClient.GetAsync(imageUri, cancellationToken);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStreamAsync(cancellationToken);
    }

    #endregion

    #region Rate Limiting

    private async Task RateLimitAsync(CancellationToken cancellationToken)
    {
        await _rateLimitSemaphore.WaitAsync(cancellationToken);
        try
        {
            await Task.Delay(RateLimitDelayMs, cancellationToken);
        }
        finally
        {
            _rateLimitSemaphore.Release();
        }
    }

    #endregion
}
