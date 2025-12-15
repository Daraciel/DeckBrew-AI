using DeckBrew.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DeckBrew.Domain.Ports;

/// <summary>
/// Port for card repository
/// </summary>
public interface ICardRepository
{
    Task<Card?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Card?> GetByScryfallIdAsync(Guid scryfallId, CancellationToken cancellationToken = default);
    Task<Card?> GetByNameAsync(string name, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Card>> FindAsync(CardSearchCriteria criteria, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Card>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Card> AddAsync(Card card, CancellationToken cancellationToken = default);
    Task AddRangeAsync(IEnumerable<Card> cards, CancellationToken cancellationToken = default);
    Task UpdateAsync(Card card, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(Guid scryfallId, CancellationToken cancellationToken = default);
}

/// <summary>
/// Criterios de búsqueda para cartas.
/// </summary>
public class CardSearchCriteria
{
    public string? Name { get; set; }
    public string[]? Colors { get; set; }
    public string? Format { get; set; }
    public int? MaxCmc { get; set; }
    public int? MinCmc { get; set; }
    public decimal? MaxPrice { get; set; }
    public string? TypeLine { get; set; }
    public string[]? Keywords { get; set; }
    public string? SetCode { get; set; }
    public int Skip { get; set; }
    public int Take { get; set; } = 100;
}
