using DeckBrew.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DeckBrew.Domain.Ports;

/// <summary>
/// Puerto para acceso a datos de mazos guardados.
/// </summary>
public interface ISavedDeckRepository
{
    Task<SavedDeck?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<SavedDeck>> GetAllAsync(Guid? userId = null, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<SavedDeck>> GetByUserAsync(Guid userId, CancellationToken cancellationToken = default);
    Task<SavedDeck> AddAsync(SavedDeck deck, CancellationToken cancellationToken = default);
    Task UpdateAsync(SavedDeck deck, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    Task<int> CountByUserAsync(Guid userId, CancellationToken cancellationToken = default);
}