using System;

namespace DeckBrew.Api.DTOs;

/// <summary>
/// Response DTO for health check endpoint
/// </summary>
public record HealthResponse
{
    public required string Status { get; init; }
    public required DateTime Timestamp { get; init; }
    public required string Version { get; init; }
}