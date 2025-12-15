using System.Text.Json.Serialization;

namespace DeckBrew.ExternalApi.Scryfall.Models;

public class ScryfallSet
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }

    [JsonPropertyName("code")]
    public string Code { get; set; } = string.Empty;

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("uri")]
    public string Uri { get; set; } = string.Empty;

    [JsonPropertyName("scryfall_uri")]
    public string ScryfallUri { get; set; } = string.Empty;

    [JsonPropertyName("search_uri")]
    public string SearchUri { get; set; } = string.Empty;

    [JsonPropertyName("released_at")]
    public DateTime? ReleasedAt { get; set; }

    [JsonPropertyName("set_type")]
    public string SetType { get; set; } = string.Empty;

    [JsonPropertyName("card_count")]
    public int CardCount { get; set; }

    [JsonPropertyName("digital")]
    public bool Digital { get; set; }

    [JsonPropertyName("foil_only")]
    public bool FoilOnly { get; set; }

    [JsonPropertyName("icon_svg_uri")]
    public string? IconSvgUri { get; set; }
}
