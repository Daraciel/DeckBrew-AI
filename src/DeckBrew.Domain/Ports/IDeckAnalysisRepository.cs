using DeckBrew.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DeckBrew.Domain.Ports;

/// <summary>
/// Puerto para análisis de mazos (caché).
/// </summary>
public interface IDeckAnalysisRepository
{
    Task<DeckAnalysis?> GetByDeckIdAsync(Guid deckId, CancellationToken cancellationToken = default);
    Task<DeckAnalysis> AddAsync(DeckAnalysis analysis, CancellationToken cancellationToken = default);
    Task UpdateAsync(DeckAnalysis analysis, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    Task DeleteByDeckIdAsync(Guid deckId, CancellationToken cancellationToken = default);
}