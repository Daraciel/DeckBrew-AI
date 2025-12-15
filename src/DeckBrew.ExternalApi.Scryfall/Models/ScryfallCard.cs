using System.Text.Json.Serialization;

namespace DeckBrew.ExternalApi.Scryfall.Models;

public class ScryfallCard
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }

    [JsonPropertyName("oracle_id")]
    public Guid? OracleId { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("lang")]
    public string Lang { get; set; } = "en";

    [JsonPropertyName("released_at")]
    public DateTime? ReleasedAt { get; set; }

    [JsonPropertyName("uri")]
    public string Uri { get; set; } = string.Empty;

    [JsonPropertyName("scryfall_uri")]
    public string ScryfallUri { get; set; } = string.Empty;

    [JsonPropertyName("layout")]
    public string Layout { get; set; } = string.Empty;

    [JsonPropertyName("image_uris")]
    public ImageUris? ImageUris { get; set; }

    [JsonPropertyName("mana_cost")]
    public string? ManaCost { get; set; }

    [JsonPropertyName("cmc")]
    public decimal Cmc { get; set; }

    [JsonPropertyName("type_line")]
    public string TypeLine { get; set; } = string.Empty;

    [JsonPropertyName("oracle_text")]
    public string? OracleText { get; set; }

    [JsonPropertyName("colors")]
    public List<string>? Colors { get; set; }

    [JsonPropertyName("color_identity")]
    public List<string>? ColorIdentity { get; set; }

    [JsonPropertyName("keywords")]
    public List<string>? Keywords { get; set; }

    [JsonPropertyName("legalities")]
    public Dictionary<string, string>? Legalities { get; set; }

    [JsonPropertyName("games")]
    public List<string>? Games { get; set; }

    [JsonPropertyName("reserved")]
    public bool Reserved { get; set; }

    [JsonPropertyName("set")]
    public string Set { get; set; } = string.Empty;

    [JsonPropertyName("set_name")]
    public string SetName { get; set; } = string.Empty;

    [JsonPropertyName("set_type")]
    public string SetType { get; set; } = string.Empty;

    [JsonPropertyName("collector_number")]
    public string CollectorNumber { get; set; } = string.Empty;

    [JsonPropertyName("rarity")]
    public string Rarity { get; set; } = string.Empty;

    [JsonPropertyName("artist")]
    public string? Artist { get; set; }

    [JsonPropertyName("border_color")]
    public string BorderColor { get; set; } = string.Empty;

    [JsonPropertyName("frame")]
    public string Frame { get; set; } = string.Empty;

    [JsonPropertyName("prices")]
    public Prices? Prices { get; set; }

    [JsonPropertyName("related_uris")]
    public Dictionary<string, string>? RelatedUris { get; set; }

    [JsonPropertyName("card_faces")]
    public List<CardFace>? CardFaces { get; set; }

    [JsonPropertyName("power")]
    public string? Power { get; set; }

    [JsonPropertyName("toughness")]
    public string? Toughness { get; set; }

    [JsonPropertyName("loyalty")]
    public string? Loyalty { get; set; }
}

public class ImageUris
{
    [JsonPropertyName("small")]
    public string? Small { get; set; }

    [JsonPropertyName("normal")]
    public string? Normal { get; set; }

    [JsonPropertyName("large")]
    public string? Large { get; set; }

    [JsonPropertyName("png")]
    public string? Png { get; set; }

    [JsonPropertyName("art_crop")]
    public string? ArtCrop { get; set; }

    [JsonPropertyName("border_crop")]
    public string? BorderCrop { get; set; }
}

public class Prices
{
    [JsonPropertyName("usd")]
    public string? Usd { get; set; }

    [JsonPropertyName("usd_foil")]
    public string? UsdFoil { get; set; }

    [JsonPropertyName("eur")]
    public string? Eur { get; set; }

    [JsonPropertyName("eur_foil")]
    public string? EurFoil { get; set; }

    [JsonPropertyName("tix")]
    public string? Tix { get; set; }
}

public class CardFace
{
    [JsonPropertyName("object")]
    public string Object { get; set; } = string.Empty;

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("mana_cost")]
    public string? ManaCost { get; set; }

    [JsonPropertyName("type_line")]
    public string TypeLine { get; set; } = string.Empty;

    [JsonPropertyName("oracle_text")]
    public string? OracleText { get; set; }

    [JsonPropertyName("colors")]
    public List<string>? Colors { get; set; }

    [JsonPropertyName("power")]
    public string? Power { get; set; }

    [JsonPropertyName("toughness")]
    public string? Toughness { get; set; }

    [JsonPropertyName("loyalty")]
    public string? Loyalty { get; set; }

    [JsonPropertyName("image_uris")]
    public ImageUris? ImageUris { get; set; }
}
