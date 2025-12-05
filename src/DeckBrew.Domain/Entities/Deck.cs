using System;
using System.Collections.Generic;

namespace DeckBrew.Domain.Entities;

public class Deck
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Format { get; set; } = string.Empty;
    public string[] Colors { get; set; } = Array.Empty<string>();
    public string Style { get; set; } = string.Empty;
    public decimal? Budget { get; set; }
    public List<CardSlot> Cards { get; set; } = new();
    public List<KeyCard> KeyCards { get; set; } = new();
    public List<string> Risks { get; set; } = new();
    public string Mulligan { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}

public class CardSlot
{
    public string Name { get; set; } = string.Empty;
    public int Count { get; set; }
}

public class KeyCard
{
    public string Name { get; set; } = string.Empty;
    public string Rationale { get; set; } = string.Empty;
}
