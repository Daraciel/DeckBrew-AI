using FluentValidation;
using DeckBrew.Domain.ValueObjects;
using System.Linq;

namespace DeckBrew.Application.Validators;

public class GenerationRequestValidator : AbstractValidator<GenerationRequest>
{
    public GenerationRequestValidator()
    {
        RuleFor(x => x.Format)
            .NotEmpty()
            .Must(f => new[] { "Standard", "Modern", "Commander" }.Contains(f))
            .WithMessage("Format must be Standard, Modern, or Commander");

        RuleFor(x => x.Style)
            .NotEmpty()
            .Must(s => new[] { "aggro", "control", "midrange", "combo" }.Contains(s))
            .WithMessage("Style must be aggro, control, midrange, or combo");

        RuleFor(x => x.Colors)
            .NotNull()
            .Must(c => c.Length > 0)
            .WithMessage("At least one color must be specified");

        RuleFor(x => x.Budget)
            .GreaterThan(0)
            .When(x => x.Budget.HasValue)
            .WithMessage("Budget must be greater than 0");
    }
}
