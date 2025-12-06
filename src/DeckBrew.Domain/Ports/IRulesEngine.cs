using DeckBrew.Domain.Entities;
using System.Collections.Generic;

namespace DeckBrew.Domain.Ports;

/// <summary>
/// Port for rules engine
/// </summary>
public interface IRulesEngine
{
    bool IsLegalDeck(Deck deck, string format);
    IEnumerable<string> ValidateDeck(Deck deck, string format);
    int GetRequiredCardCount(string format);
}
