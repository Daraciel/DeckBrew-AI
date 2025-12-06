namespace DeckBrew.Contracts.DTOs;

/// <summary>
/// Request for deck generation
/// </summary>
public record GenerateDeckRequest
{
    public required GenerationRequestDto Request { get; init; }
}

public record GenerationRequestDto
{
    public required string Format { get; init; }
    public required string[] Colors { get; init; }
    public required string Style { get; init; }
    public double? Budget { get; init; }
    public string? LocalMeta { get; init; }
}