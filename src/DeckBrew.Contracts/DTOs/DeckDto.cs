using System;
namespace DeckBrew.Contracts.DTOs;

/// <summary>
/// Full deck DTO with metadata
/// </summary>
public record DeckDto
{
    public required string Id { get; init; }
    public required string Format { get; init; }
    public required string[] Colors { get; init; }
    public required string Style { get; init; }
    public decimal? Budget { get; init; }
    public required CardSlotDto[] Cards { get; init; }
    public required KeyCardDto[] KeyCards { get; init; }
    public required string[] Risks { get; init; }
    public required string Mulligan { get; init; }
    public required DateTime CreatedAt { get; init; }
}