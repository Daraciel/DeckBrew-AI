namespace DeckBrew.Api.DTOs;

/// <summary>
/// Response DTO for deck generation
/// </summary>
public record GenerateDeckResponse
{
    public required CardSlotDto[] Cards { get; init; }
    public required KeyCardDto[] KeyCards { get; init; }
    public required string[] Risks { get; init; }
    public required string Mulligan { get; init; }
}

/// <summary>
/// DTO for a card slot in the deck
/// </summary>
public record CardSlotDto
{
    public required string Name { get; init; }
    public required int Count { get; init; }
}

/// <summary>
/// DTO for a key card with rationale
/// </summary>
public record KeyCardDto
{
    public required string Name { get; init; }
    public required string Rationale { get; init; }
}