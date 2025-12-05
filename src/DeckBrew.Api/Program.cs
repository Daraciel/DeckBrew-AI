using DeckBrew.Application.Commands;
using DeckBrew.Application.Handlers;
using DeckBrew.Application.Validators;
using DeckBrew.Domain.Ports;
using DeckBrew.Infrastructure.Repositories;
using DeckBrew.Infrastructure.Rules;
using DeckBrew.Infrastructure.Synergy;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;
using System.Threading;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "DeckBrew API", Version = "v1" });
});

// Register validators
builder.Services.AddValidatorsFromAssemblyContaining<GenerationRequestValidator>();

// Register domain ports with infrastructure adapters
builder.Services.AddSingleton<ICardRepository, InMemoryCardRepository>();
builder.Services.AddSingleton<IDeckRepository, InMemoryDeckRepository>();
builder.Services.AddSingleton<IRulesEngine, MtgRulesEngine>();
builder.Services.AddSingleton<ISynergyCalculator, StubSynergyCalculator>();

// Register handlers
builder.Services.AddScoped<GenerateDeckHandler>();

var app = builder.Build();

// Configure middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "DeckBrew API v1"));
}

// Map endpoints
app.MapPost("/v1/generate", async (
    GenerateDeckCommand command,
    GenerateDeckHandler handler,
    IValidator<DeckBrew.Domain.ValueObjects.GenerationRequest> validator,
    CancellationToken cancellationToken) =>
{
    var validationResult = await validator.ValidateAsync(command.Request, cancellationToken);
    if (!validationResult.IsValid)
    {
        return Results.BadRequest(validationResult.Errors);
    }

    var result = await handler.Handle(command, cancellationToken);
    return Results.Ok(new
    {
        cards = result.Deck.Cards.Select(c => new { name = c.Name, count = c.Count }),
        keyCards = result.Deck.KeyCards.Select(k => new { name = k.Name, rationale = k.Rationale }),
        risks = result.Deck.Risks,
        mulligan = result.Deck.Mulligan
    });
})
.WithName("GenerateDeck")
.WithTags("Deck Generation")
.Produces(200)
.Produces(400);

app.MapGet("/v1/decks/{id}", async (
    string id,
    IDeckRepository repository,
    CancellationToken cancellationToken) =>
{
    var deck = await repository.GetByIdAsync(id, cancellationToken);
    return deck is not null ? Results.Ok(deck) : Results.NotFound();
})
.WithName("GetDeck")
.WithTags("Deck Management")
.Produces(200)
.Produces(404);

app.MapDelete("/v1/decks/{id}", async (
    string id,
    IDeckRepository repository,
    CancellationToken cancellationToken) =>
{
    await repository.DeleteAsync(id, cancellationToken);
    return Results.NoContent();
})
.WithName("DeleteDeck")
.WithTags("Deck Management")
.Produces(204);

app.MapGet("/v1/health", () => Results.Ok(new { status = "healthy", timestamp = DateTime.UtcNow }))
    .WithName("Health")
    .WithTags("System")
    .Produces(200);

app.Run();
