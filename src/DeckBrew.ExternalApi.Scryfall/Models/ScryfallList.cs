using System.Text.Json.Serialization;

namespace DeckBrew.ExternalApi.Scryfall.Models;

public class ScryfallList<T>
{
    [JsonPropertyName("object")]
    public string Object { get; set; } = string.Empty;

    [JsonPropertyName("total_cards")]
    public int? TotalCards { get; set; }

    [JsonPropertyName("has_more")]
    public bool HasMore { get; set; }

    [JsonPropertyName("next_page")]
    public string? NextPage { get; set; }

    [JsonPropertyName("data")]
    public List<T> Data { get; set; } = new();

    [JsonPropertyName("warnings")]
    public List<string>? Warnings { get; set; }
}
