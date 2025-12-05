using System;
using DeckBrew.Contracts.DTOs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace DeckBrew.Api.Controllers;

/// <summary>
/// System endpoints for health checks and diagnostics
/// </summary>
public static class SystemController
{
    /// <summary>
    /// Maps all system-related endpoints
    /// </summary>
    public static IEndpointRouteBuilder MapSystemEndpoints(this IEndpointRouteBuilder endpoints)
    {
        var group = endpoints.MapGroup("/v1")
            .WithTags("System");

        group.MapGet("/health", GetHealthAsync)
            .WithName("Health")
            .Produces<HealthResponse>(200);

        return endpoints;
    }

    private static IResult GetHealthAsync()
    {
        var response = new HealthResponse
        {
            Status = "healthy",
            Timestamp = DateTime.UtcNow,
            Version = "1.0.0"
        };

        return Results.Ok(response);
    }
}
