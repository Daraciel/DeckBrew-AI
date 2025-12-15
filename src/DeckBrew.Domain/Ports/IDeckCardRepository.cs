using DeckBrew.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DeckBrew.Domain.Ports;

/// <summary>
/// Puerto para gestión de cartas en mazos.
/// </summary>
public interface IDeckCardRepository
{
    Task<IReadOnlyList<DeckCard>> GetByDeckIdAsync(Guid deckId, CancellationToken cancellationToken = default);
    Task<DeckCard> AddAsync(DeckCard deckCard, CancellationToken cancellationToken = default);
    Task AddRangeAsync(IEnumerable<DeckCard> deckCards, CancellationToken cancellationToken = default);
    Task UpdateAsync(DeckCard deckCard, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    Task DeleteByDeckIdAsync(Guid deckId, CancellationToken cancellationToken = default);
}