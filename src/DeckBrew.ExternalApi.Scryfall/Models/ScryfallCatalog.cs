using System.Text.Json.Serialization;

namespace DeckBrew.ExternalApi.Scryfall.Models;

public class ScryfallCatalog
{
    [JsonPropertyName("object")]
    public string Object { get; set; } = string.Empty;

    [JsonPropertyName("uri")]
    public string Uri { get; set; } = string.Empty;

    [JsonPropertyName("total_values")]
    public int TotalValues { get; set; }

    [JsonPropertyName("data")]
    public List<string> Data { get; set; } = new();
}
