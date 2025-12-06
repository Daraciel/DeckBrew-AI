using DeckBrew.Domain.Entities;
using DeckBrew.Domain.Ports;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DeckBrew.Infrastructure.Repositories;

/// <summary>
/// In-memory implementation of IDeckRepository (MVP)
/// </summary>
public class InMemoryDeckRepository : IDeckRepository
{
    private readonly Dictionary<string, Deck> _decks = new();

    public Task<Deck?> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        _decks.TryGetValue(id, out var deck);
        return Task.FromResult(deck);
    }

    public Task<string> SaveAsync(Deck deck, CancellationToken cancellationToken = default)
    {
        _decks[deck.Id] = deck;
        return Task.FromResult(deck.Id);
    }

    public Task DeleteAsync(string id, CancellationToken cancellationToken = default)
    {
        _decks.Remove(id);
        return Task.CompletedTask;
    }

    public Task<IEnumerable<Deck>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return Task.FromResult(_decks.Values.AsEnumerable());
    }
}
