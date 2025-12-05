using DeckBrew.Domain.Entities;
using DeckBrew.Domain.Ports;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DeckBrew.Infrastructure.Synergy;

/// <summary>
/// Stub implementation of ISynergyCalculator (to be replaced with embeddings in future)
/// </summary>
public class StubSynergyCalculator : ISynergyCalculator
{
    public Task<IEnumerable<Card>> GetSynergisticCardsAsync(
        Card card, 
        IEnumerable<Card> availableCards, 
        int topK = 10, 
        CancellationToken cancellationToken = default)
    {
        // MVP: Return random cards with same colors
        var synergistic = availableCards
            .Where(c => c.Colors.Any(color => card.Colors.Contains(color)))
            .Take(topK);

        return Task.FromResult(synergistic);
    }
}
