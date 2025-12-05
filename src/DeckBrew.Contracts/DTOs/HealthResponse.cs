using System;
namespace DeckBrew.Contracts.DTOs;

/// <summary>
/// Health check response
/// </summary>
public record HealthResponse
{
    public required string Status { get; init; }
    public required DateTime Timestamp { get; init; }
    public required string Version { get; init; }
}