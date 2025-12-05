using DeckBrew.Domain.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DeckBrew.Domain.Ports;

/// <summary>
/// Port for deck repository
/// </summary>
public interface IDeckRepository
{
    Task<Deck?> GetByIdAsync(string id, CancellationToken cancellationToken = default);
    Task<string> SaveAsync(Deck deck, CancellationToken cancellationToken = default);
    Task DeleteAsync(string id, CancellationToken cancellationToken = default);
    Task<IEnumerable<Deck>> GetAllAsync(CancellationToken cancellationToken = default);
}
