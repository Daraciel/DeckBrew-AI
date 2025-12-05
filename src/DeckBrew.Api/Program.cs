using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using DeckBrew.Api.Controllers;
using DeckBrew.Application.Handlers;
using DeckBrew.Application.Validators;
using DeckBrew.Domain.Ports;
using DeckBrew.Infrastructure.Repositories;
using DeckBrew.Infrastructure.Rules;
using DeckBrew.Infrastructure.Synergy;
using FluentValidation;

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

// Map controller endpoints
app.MapDeckEndpoints();
app.MapSystemEndpoints();

app.Run();
