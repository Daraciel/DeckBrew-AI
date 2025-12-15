using System.Text.Json.Serialization;

namespace DeckBrew.ExternalApi.Scryfall.Models;

public class ManaCost
{
    [JsonPropertyName("cost")]
    public string Cost { get; set; } = string.Empty;

    [JsonPropertyName("cmc")]
    public decimal Cmc { get; set; }

    [JsonPropertyName("colors")]
    public List<string>? Colors { get; set; }

    [JsonPropertyName("colorless")]
    public bool Colorless { get; set; }

    [JsonPropertyName("monocolored")]
    public bool Monocolored { get; set; }

    [JsonPropertyName("multicolored")]
    public bool Multicolored { get; set; }
}
