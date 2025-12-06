using System;

namespace DeckBrew.Domain.ValueObjects;

public record GenerationRequest
{
    public string Format { get; init; } = string.Empty;
    public string[] Colors { get; init; } = Array.Empty<string>();
    public string Style { get; init; } = string.Empty;
    public decimal? Budget { get; init; }
    public string? LocalMeta { get; init; }
}
