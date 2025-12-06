using DeckBrew.Domain.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DeckBrew.Domain.Ports;

/// <summary>
/// Port for synergy calculator using embeddings
/// </summary>
public interface ISynergyCalculator
{
    Task<IEnumerable<Card>> GetSynergisticCardsAsync(Card card, IEnumerable<Card> availableCards, int topK = 10, CancellationToken cancellationToken = default);
}
