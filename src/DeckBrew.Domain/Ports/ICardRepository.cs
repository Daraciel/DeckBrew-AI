using DeckBrew.Domain.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DeckBrew.Domain.Ports;

/// <summary>
/// Port for card repository
/// </summary>
public interface ICardRepository
{
    Task<IEnumerable<Card>> GetByColorsAsync(string[] colors, CancellationToken cancellationToken = default);
    Task<IEnumerable<Card>> GetLegalInFormatAsync(string format, CancellationToken cancellationToken = default);
    Task<Card?> GetByIdAsync(string id, CancellationToken cancellationToken = default);
}
