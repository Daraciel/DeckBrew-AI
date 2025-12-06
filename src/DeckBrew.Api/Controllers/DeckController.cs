using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DeckBrew.Application.Commands;
using DeckBrew.Application.Handlers;
using DeckBrew.Contracts;
using DeckBrew.Contracts.DTOs;
using DeckBrew.Domain.Ports;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace DeckBrew.Api.Controllers;

/// <summary>
/// Endpoints for deck generation and management
/// Implements the IDeckBrewApi contract
/// </summary>
public static class DeckController
{
    /// <summary>
    /// Maps all deck-related endpoints
    /// </summary>
    public static IEndpointRouteBuilder MapDeckEndpoints(this IEndpointRouteBuilder endpoints)
    {
        var group = endpoints.MapGroup("/v1")
            .WithTags("Deck");

        group.MapPost("/generate", GenerateDeckAsync)
            .WithName("GenerateDeck")
            .Produces<GenerateDeckResponse>(200)
            .Produces(400);

        group.MapGet("/decks/{id}", GetDeckAsync)
            .WithName("GetDeck")
            .Produces<DeckDto>(200)
            .Produces(404);

        group.MapDelete("/decks/{id}", DeleteDeckAsync)
            .WithName("DeleteDeck")
            .Produces(204);

        return endpoints;
    }

    private static async Task<IResult> GenerateDeckAsync(
        GenerateDeckRequest request,
        GenerateDeckHandler handler,
        IValidator<Domain.ValueObjects.GenerationRequest> validator,
        CancellationToken cancellationToken)
    {
        // Map DTO to Domain ValueObject
        var domainRequest = new Domain.ValueObjects.GenerationRequest
        {
            Format = request.Request.Format,
            Colors = request.Request.Colors,
            Style = request.Request.Style,
            Budget = request.Request.Budget.HasValue ? (decimal)request.Request.Budget.Value : null,
            LocalMeta = request.Request.LocalMeta
        };

        var validationResult = await validator.ValidateAsync(domainRequest, cancellationToken);
        if (!validationResult.IsValid)
        {
            return Results.BadRequest(validationResult.Errors);
        }

        var command = new GenerateDeckCommand { Request = domainRequest };
        var result = await handler.Handle(command, cancellationToken);
        
        // Map Domain to DTO
        var response = new GenerateDeckResponse
        {
            Cards = result.Deck.Cards.Select(c => new CardSlotDto 
            { 
                Name = c.Name, 
                Count = c.Count 
            }).ToArray(),
            KeyCards = result.Deck.KeyCards.Select(k => new KeyCardDto 
            { 
                Name = k.Name, 
                Rationale = k.Rationale 
            }).ToArray(),
            Risks = result.Deck.Risks.ToArray(),
            Mulligan = result.Deck.Mulligan
        };

        return Results.Ok(response);
    }

    private static async Task<IResult> GetDeckAsync(
        string id,
        IDeckRepository repository,
        CancellationToken cancellationToken)
    {
        var deck = await repository.GetByIdAsync(id, cancellationToken);
        
        if (deck is null)
            return Results.NotFound();

        // Map Domain to DTO
        var dto = new DeckDto
        {
            Id = deck.Id,
            Format = deck.Format,
            Colors = deck.Colors,
            Style = deck.Style,
            Budget = deck.Budget,
            Cards = deck.Cards.Select(c => new CardSlotDto { Name = c.Name, Count = c.Count }).ToArray(),
            KeyCards = deck.KeyCards.Select(k => new KeyCardDto { Name = k.Name, Rationale = k.Rationale }).ToArray(),
            Risks = deck.Risks.ToArray(),
            Mulligan = deck.Mulligan,
            CreatedAt = deck.CreatedAt
        };

        return Results.Ok(dto);
    }

    private static async Task<IResult> DeleteDeckAsync(
        string id,
        IDeckRepository repository,
        CancellationToken cancellationToken)
    {
        await repository.DeleteAsync(id, cancellationToken);
        return Results.NoContent();
    }
}
