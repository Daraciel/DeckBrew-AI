using DeckBrew.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DeckBrew.Domain.Ports;

/// <summary>
/// Puerto para acceso a datos de sets/colecciones.
/// </summary>
public interface IMtgSetRepository
{
    Task<MtgSet?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<MtgSet?> GetByCodeAsync(string code, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<MtgSet>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<MtgSet> AddAsync(MtgSet set, CancellationToken cancellationToken = default);
    Task UpdateAsync(MtgSet set, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(string code, CancellationToken cancellationToken = default);
}