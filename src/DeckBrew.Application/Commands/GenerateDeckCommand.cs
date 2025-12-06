using DeckBrew.Domain.Entities;
using DeckBrew.Domain.ValueObjects;

namespace DeckBrew.Application.Commands;

/// <summary>
/// Command to generate a new deck
/// </summary>
public record GenerateDeckCommand
{
    public GenerationRequest Request { get; init; } = null!;
}

/// <summary>
/// Result of deck generation
/// </summary>
public record GenerateDeckResult
{
    public Deck Deck { get; init; } = null!;
}
