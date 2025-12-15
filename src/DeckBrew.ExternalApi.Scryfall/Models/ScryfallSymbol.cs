using System.Text.Json.Serialization;

namespace DeckBrew.ExternalApi.Scryfall.Models;

public class ScryfallSymbol
{
    [JsonPropertyName("object")]
    public string Object { get; set; } = string.Empty;

    [JsonPropertyName("symbol")]
    public string Symbol { get; set; } = string.Empty;

    [JsonPropertyName("loose_variant")]
    public string? LooseVariant { get; set; }

    [JsonPropertyName("english")]
    public string English { get; set; } = string.Empty;

    [JsonPropertyName("transposable")]
    public bool Transposable { get; set; }

    [JsonPropertyName("represents_mana")]
    public bool RepresentsMana { get; set; }

    [JsonPropertyName("appears_in_mana_costs")]
    public bool AppearsInManaCosts { get; set; }

    [JsonPropertyName("mana_value")]
    public decimal? ManaValue { get; set; }

    [JsonPropertyName("funny")]
    public bool Funny { get; set; }

    [JsonPropertyName("colors")]
    public List<string>? Colors { get; set; }

    [JsonPropertyName("gatherer_alternates")]
    public List<string>? GathererAlternates { get; set; }

    [JsonPropertyName("svg_uri")]
    public string? SvgUri { get; set; }
}
