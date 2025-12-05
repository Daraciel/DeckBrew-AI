using DeckBrew.Domain.Entities;
using DeckBrew.Domain.Ports;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DeckBrew.Infrastructure.Repositories;

/// <summary>
/// In-memory implementation of ICardRepository (MVP)
/// </summary>
public class InMemoryCardRepository : ICardRepository
{
    private readonly List<Card> _cards;

    public InMemoryCardRepository()
    {
        _cards = GenerateSampleCards();
    }

    public Task<IEnumerable<Card>> GetByColorsAsync(string[] colors, CancellationToken cancellationToken = default)
    {
        var filtered = _cards.Where(c => 
            c.Colors.Length == 0 || c.Colors.Any(color => colors.Contains(color))
        );
        return Task.FromResult(filtered);
    }

    public Task<IEnumerable<Card>> GetLegalInFormatAsync(string format, CancellationToken cancellationToken = default)
    {
        var filtered = _cards.Where(c => c.Legalities.Contains(format));
        return Task.FromResult(filtered);
    }

    public Task<Card?> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(_cards.FirstOrDefault(c => c.Id == id));
    }

    private List<Card> GenerateSampleCards()
    {
        return new List<Card>
        {
            new() { Id = "1", Name = "Lightning Bolt", Colors = new[] { "R" }, Type = "Instant", ConvertedManaCost = 1, ManaCost = "{R}", Price = 2.5m, Legalities = new[] { "Standard", "Modern", "Commander" }, Text = "Deal 3 damage to any target." },
            new() { Id = "2", Name = "Counterspell", Colors = new[] { "U" }, Type = "Instant", ConvertedManaCost = 2, ManaCost = "{U}{U}", Price = 1.5m, Legalities = new[] { "Modern", "Commander" }, Text = "Counter target spell." },
            new() { Id = "3", Name = "Birds of Paradise", Colors = new[] { "G" }, Type = "Creature", ConvertedManaCost = 1, ManaCost = "{G}", Price = 5.0m, Legalities = new[] { "Modern", "Commander" }, Text = "Flying. Tap: Add one mana of any color." },
            new() { Id = "4", Name = "Swords to Plowshares", Colors = new[] { "W" }, Type = "Instant", ConvertedManaCost = 1, ManaCost = "{W}", Price = 3.0m, Legalities = new[] { "Commander" }, Text = "Exile target creature. Its controller gains life equal to its power." },
            new() { Id = "5", Name = "Dark Ritual", Colors = new[] { "B" }, Type = "Instant", ConvertedManaCost = 1, ManaCost = "{B}", Price = 0.5m, Legalities = new[] { "Commander" }, Text = "Add {B}{B}{B}." }
        };
    }
}
