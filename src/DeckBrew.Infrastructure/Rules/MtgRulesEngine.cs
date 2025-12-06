using DeckBrew.Domain.Entities;
using DeckBrew.Domain.Ports;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DeckBrew.Infrastructure.Rules;

/// <summary>
/// MTG rules engine implementation
/// </summary>
public class MtgRulesEngine : IRulesEngine
{
    public bool IsLegalDeck(Deck deck, string format)
    {
        var errors = ValidateDeck(deck, format);
        return !errors.Any();
    }

    public IEnumerable<string> ValidateDeck(Deck deck, string format)
    {
        var errors = new List<string>();

        // Check card count
        var requiredCount = GetRequiredCardCount(format);
        var actualCount = deck.Cards.Sum(c => c.Count);
        
        if (actualCount < requiredCount)
        {
            errors.Add($"Deck must have at least {requiredCount} cards. Currently has {actualCount}.");
        }

        // Check card limits (4-of rule for non-Commander formats, except basic lands)
        if (format != "Commander")
        {
            var invalidCards = deck.Cards
                .Where(c => !IsBasicLand(c.Name) && c.Count > 4)
                .ToList();

            foreach (var card in invalidCards)
            {
                errors.Add($"Card '{card.Name}' exceeds the 4-copy limit (has {card.Count}).");
            }
        }
        else
        {
            // Commander: singleton rule (except basic lands)
            var invalidCards = deck.Cards
                .Where(c => !IsBasicLand(c.Name) && c.Count > 1)
                .ToList();

            foreach (var card in invalidCards)
            {
                errors.Add($"Card '{card.Name}' exceeds the 1-copy limit in Commander (has {card.Count}).");
            }
        }

        return errors;
    }

    public int GetRequiredCardCount(string format)
    {
        return format switch
        {
            "Commander" => 100,
            "Standard" => 60,
            "Modern" => 60,
            _ => 60
        };
    }

    private bool IsBasicLand(string cardName)
    {
        var basicLands = new[] { "Plains", "Island", "Swamp", "Mountain", "Forest", "Wastes" };
        return basicLands.Any(land => cardName.Contains(land, StringComparison.OrdinalIgnoreCase));
    }
}
