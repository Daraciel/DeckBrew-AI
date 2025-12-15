using DeckBrew.ExternalApi.Scryfall.Models;

namespace DeckBrew.ExternalApi.Scryfall;

/// <summary>
/// Service wrapper for Scryfall API operations
/// </summary>
public interface IScryfallService
{
    #region Card Search

    /// <summary>
    /// Search for cards using Scryfall's query syntax
    /// </summary>
    Task<IEnumerable<ScryfallCard>> SearchCardsAsync(string query, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get all results from a paginated search
    /// </summary>
    Task<IEnumerable<ScryfallCard>> SearchAllCardsAsync(string query, int maxPages = 10, CancellationToken cancellationToken = default);

    /// <summary>
    /// Search for a card by exact name
    /// </summary>
    Task<ScryfallCard?> GetCardByNameAsync(string name, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get autocomplete suggestions for card names
    /// </summary>
    Task<IEnumerable<string>> AutocompleteCardNameAsync(string partialName, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get a random card, optionally filtered by query
    /// </summary>
    Task<ScryfallCard> GetRandomCardAsync(string? query = null, CancellationToken cancellationToken = default);

    #endregion

    #region Commander Specific

    /// <summary>
    /// Search for legendary creatures that can be commanders
    /// </summary>
    Task<IEnumerable<ScryfallCard>> SearchCommandersAsync(string? colorIdentity = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get cards legal in Commander format
    /// </summary>
    Task<IEnumerable<ScryfallCard>> SearchCommanderLegalCardsAsync(string query, CancellationToken cancellationToken = default);

    #endregion

    #region Sets

    /// <summary>
    /// Get all available sets
    /// </summary>
    Task<IEnumerable<ScryfallSet>> GetAllSetsAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Get a specific set by code
    /// </summary>
    Task<ScryfallSet?> GetSetByCodeAsync(string code, CancellationToken cancellationToken = default);

    #endregion

    #region Symbology

    /// <summary>
    /// Get all mana symbols
    /// </summary>
    Task<IEnumerable<ScryfallSymbol>> GetAllSymbolsAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Parse a mana cost string into its components
    /// </summary>
    Task<ManaCost?> ParseManaCostAsync(string cost, CancellationToken cancellationToken = default);

    #endregion

    #region Catalogs

    /// <summary>
    /// Get all possible card names
    /// </summary>
    Task<IEnumerable<string>> GetAllCardNamesAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Get all creature types
    /// </summary>
    Task<IEnumerable<string>> GetCreatureTypesAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Get all keyword abilities
    /// </summary>
    Task<IEnumerable<string>> GetKeywordAbilitiesAsync(CancellationToken cancellationToken = default);

    #endregion

    #region Images

    /// <summary>
    /// Download card image from URI
    /// </summary>
    Task<Stream> GetCardImageAsync(string imageUri, CancellationToken cancellationToken = default);

    #endregion
}
