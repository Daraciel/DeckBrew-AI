using DeckBrew.ExternalApi.Scryfall.Models;
using Refit;

namespace DeckBrew.ExternalApi.Scryfall;

public interface IScryfallApi
{
    #region Cards

    /// <summary>
    /// Search for cards using Scryfall's query syntax
    /// </summary>
    [Get("/cards/search")]
    Task<ScryfallList<ScryfallCard>> SearchCardsAsync(
        [AliasAs("q")] string query,
        [AliasAs("unique")] string? unique = null,
        [AliasAs("order")] string? order = null,
        [AliasAs("dir")] string? direction = null,
        [AliasAs("page")] int? page = null);

    /// <summary>
    /// Get a card by exact name
    /// </summary>
    [Get("/cards/named")]
    Task<ScryfallCard> GetCardByExactNameAsync([AliasAs("exact")] string exactName);

    /// <summary>
    /// Get a card by fuzzy name match
    /// </summary>
    [Get("/cards/named")]
    Task<ScryfallCard> GetCardByFuzzyNameAsync([AliasAs("fuzzy")] string fuzzyName, [AliasAs("set")] string? setCode = null);

    /// <summary>
    /// Get autocomplete suggestions for card names
    /// </summary>
    [Get("/cards/autocomplete")]
    Task<ScryfallCatalog> AutocompleteCardNameAsync([AliasAs("q")] string query, [AliasAs("include_extras")] bool includeExtras = false);

    /// <summary>
    /// Get a random card
    /// </summary>
    [Get("/cards/random")]
    Task<ScryfallCard> GetRandomCardAsync([AliasAs("q")] string? query = null);

    /// <summary>
    /// Get a card by Scryfall ID
    /// </summary>
    [Get("/cards/{id}")]
    Task<ScryfallCard> GetCardByIdAsync(Guid id);

    /// <summary>
    /// Get a card by set code and collector number
    /// </summary>
    [Get("/cards/{setCode}/{collectorNumber}")]
    Task<ScryfallCard> GetCardBySetAndNumberAsync(string setCode, string collectorNumber);

    #endregion

    #region Sets

    /// <summary>
    /// Get all sets
    /// </summary>
    [Get("/sets")]
    Task<ScryfallList<ScryfallSet>> GetAllSetsAsync();

    /// <summary>
    /// Get a set by code
    /// </summary>
    [Get("/sets/{code}")]
    Task<ScryfallSet> GetSetByCodeAsync(string code);

    /// <summary>
    /// Get a set by Scryfall ID
    /// </summary>
    [Get("/sets/{id}")]
    Task<ScryfallSet> GetSetByIdAsync(Guid id);

    #endregion

    #region Symbology

    /// <summary>
    /// Get all mana symbols
    /// </summary>
    [Get("/symbology")]
    Task<ScryfallList<ScryfallSymbol>> GetAllSymbolsAsync();

    /// <summary>
    /// Parse a mana cost string
    /// </summary>
    [Get("/symbology/parse-mana")]
    Task<ManaCost> ParseManaCostAsync([AliasAs("cost")] string cost);

    #endregion

    #region Catalog

    /// <summary>
    /// Get all card names
    /// </summary>
    [Get("/catalog/card-names")]
    Task<ScryfallCatalog> GetAllCardNamesAsync();

    /// <summary>
    /// Get all creature types
    /// </summary>
    [Get("/catalog/creature-types")]
    Task<ScryfallCatalog> GetCreatureTypesAsync();

    /// <summary>
    /// Get all planeswalker types
    /// </summary>
    [Get("/catalog/planeswalker-types")]
    Task<ScryfallCatalog> GetPlaneswalkerTypesAsync();

    /// <summary>
    /// Get all land types
    /// </summary>
    [Get("/catalog/land-types")]
    Task<ScryfallCatalog> GetLandTypesAsync();

    /// <summary>
    /// Get all artifact types
    /// </summary>
    [Get("/catalog/artifact-types")]
    Task<ScryfallCatalog> GetArtifactTypesAsync();

    /// <summary>
    /// Get all enchantment types
    /// </summary>
    [Get("/catalog/enchantment-types")]
    Task<ScryfallCatalog> GetEnchantmentTypesAsync();

    /// <summary>
    /// Get all spell types
    /// </summary>
    [Get("/catalog/spell-types")]
    Task<ScryfallCatalog> GetSpellTypesAsync();

    /// <summary>
    /// Get all powers (creature power values)
    /// </summary>
    [Get("/catalog/powers")]
    Task<ScryfallCatalog> GetPowersAsync();

    /// <summary>
    /// Get all toughnesses
    /// </summary>
    [Get("/catalog/toughnesses")]
    Task<ScryfallCatalog> GetToughnessesAsync();

    /// <summary>
    /// Get all loyalties (planeswalker loyalty values)
    /// </summary>
    [Get("/catalog/loyalties")]
    Task<ScryfallCatalog> GetLoyaltiesAsync();

    /// <summary>
    /// Get all watermarks
    /// </summary>
    [Get("/catalog/watermarks")]
    Task<ScryfallCatalog> GetWatermarksAsync();

    /// <summary>
    /// Get all keyword abilities
    /// </summary>
    [Get("/catalog/keyword-abilities")]
    Task<ScryfallCatalog> GetKeywordAbilitiesAsync();

    /// <summary>
    /// Get all keyword actions
    /// </summary>
    [Get("/catalog/keyword-actions")]
    Task<ScryfallCatalog> GetKeywordActionsAsync();

    /// <summary>
    /// Get all ability words
    /// </summary>
    [Get("/catalog/ability-words")]
    Task<ScryfallCatalog> GetAbilityWordsAsync();

    #endregion
}
