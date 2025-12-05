using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Linq;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddValidatorsFromAssemblyContaining<GenerationRequestValidator>();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapPost("/generate", (GenerationRequest req, IValidator<GenerationRequest> validator) =>
{
    var result = validator.Validate(req);
    if (!result.IsValid) return Results.BadRequest(result.Errors);

    // Reemplaza la inicialización de DeckResponse usando el constructor con todos los argumentos requeridos
    var deck = new DeckResponse(
        new[] { new CardSlotDto("Island", 20) },
        new[] { new KeyCardDto("Example Card", "Sinergia con control") },
        new[] { "Maná base mejorable" },
        "Mantén manos con 2-3 tierras."
    );
    return Results.Ok(deck);
})
.WithName("GenerateDeck")
.WithOpenApi();

app.Run();

public record GenerationRequest(string format, string[] colors, string style, double? budget);
public record DeckResponse(CardSlotDto[] cards, KeyCardDto[] keyCards, string[] risks, string mulligan);
public record CardSlotDto(string name, int count);
public record KeyCardDto(string name, string rationale);

public class GenerationRequestValidator : AbstractValidator<GenerationRequest>
{
    public GenerationRequestValidator()
    {
        RuleFor(x => x.format).NotEmpty().Must(f => new[] { "Standard", "Modern", "Commander" }.Contains(f));
        RuleFor(x => x.style).NotEmpty().Must(s => new[] { "aggro", "control", "midrange", "combo" }.Contains(s));
        RuleFor(x => x.colors).NotNull().Must(c => c.Length > 0);
        RuleFor(x => x.budget).GreaterThan(0).When(x => x.budget.HasValue);
    }
}
