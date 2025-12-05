using System;

namespace DeckBrew.Domain.Entities;

public class Card
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string[] Colors { get; set; } = Array.Empty<string>();
    public string Type { get; set; } = string.Empty;
    public int? ConvertedManaCost { get; set; }
    public string ManaCost { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string[] Legalities { get; set; } = Array.Empty<string>();
    public string Text { get; set; } = string.Empty;
}
