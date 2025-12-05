using DeckBrew.Application.Commands;
using DeckBrew.Domain.Entities;
using DeckBrew.Domain.Ports;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DeckBrew.Application.Handlers;

/// <summary>
/// Handler for GenerateDeckCommand
/// </summary>
public class GenerateDeckHandler
{
    private readonly ICardRepository _cardRepository;
    private readonly IRulesEngine _rulesEngine;
    private readonly IDeckRepository _deckRepository;

    public GenerateDeckHandler(
        ICardRepository cardRepository,
        IRulesEngine rulesEngine,
        IDeckRepository deckRepository)
    {
        _cardRepository = cardRepository;
        _rulesEngine = rulesEngine;
        _deckRepository = deckRepository;
    }

    public async Task<GenerateDeckResult> Handle(GenerateDeckCommand command, CancellationToken cancellationToken = default)
    {
        var request = command.Request;

        // Get legal cards for the format and colors
        var legalCards = await _cardRepository.GetLegalInFormatAsync(request.Format, cancellationToken);
        var colorFilteredCards = legalCards.Where(c => 
            c.Colors.Length == 0 || c.Colors.Any(color => request.Colors.Contains(color))
        ).ToList();

        // Apply budget filter if specified
        if (request.Budget.HasValue)
        {
            colorFilteredCards = colorFilteredCards.Where(c => c.Price <= request.Budget.Value).ToList();
        }

        // Generate deck based on style (simplified MVP logic)
        var deck = new Deck
        {
            Format = request.Format,
            Colors = request.Colors,
            Style = request.Style,
            Budget = request.Budget
        };

        // Generate cards based on style
        var requiredCount = _rulesEngine.GetRequiredCardCount(request.Format);
        deck.Cards = GenerateCardList(colorFilteredCards, request.Style, requiredCount);

        // Generate key cards (top 5 by CMC/price)
        deck.KeyCards = GenerateKeyCards(deck.Cards, colorFilteredCards);

        // Generate risks
        deck.Risks = GenerateRisks(deck, request);

        // Generate mulligan guide
        deck.Mulligan = GenerateMulliganGuide(request.Style);

        // Validate deck
        var validationErrors = _rulesEngine.ValidateDeck(deck, request.Format);
        if (validationErrors.Any())
        {
            deck.Risks.AddRange(validationErrors);
        }

        // Save deck
        await _deckRepository.SaveAsync(deck, cancellationToken);

        return new GenerateDeckResult { Deck = deck };
    }

    private List<CardSlot> GenerateCardList(List<Card> availableCards, string style, int requiredCount)
    {
        var cards = new List<CardSlot>();
        
        // Simplified logic: distribute cards by CMC based on style
        var distribution = style.ToLower() switch
        {
            "aggro" => new Dictionary<int, int> { { 1, 12 }, { 2, 16 }, { 3, 8 }, { 4, 4 }, { 0, 20 } }, // 0 = lands
            "control" => new Dictionary<int, int> { { 2, 8 }, { 3, 8 }, { 4, 12 }, { 5, 8 }, { 0, 24 } },
            "midrange" => new Dictionary<int, int> { { 2, 10 }, { 3, 12 }, { 4, 10 }, { 5, 6 }, { 0, 22 } },
            "combo" => new Dictionary<int, int> { { 1, 8 }, { 2, 16 }, { 3, 12 }, { 4, 4 }, { 0, 20 } },
            _ => new Dictionary<int, int> { { 2, 10 }, { 3, 12 }, { 4, 10 }, { 5, 6 }, { 0, 22 } }
        };

        foreach (var (cmc, count) in distribution)
        {
            if (cmc == 0)
            {
                // Add basic lands
                cards.Add(new CardSlot { Name = "Island", Count = count });
            }
            else
            {
                var cmcCards = availableCards.Where(c => c.ConvertedManaCost == cmc).ToList();
                var selected = cmcCards.Take(count / 4).ToList(); // Simplified: take 4 of each
                foreach (var card in selected)
                {
                    cards.Add(new CardSlot { Name = card.Name, Count = 4 });
                }
            }
        }

        return cards;
    }

    private List<KeyCard> GenerateKeyCards(List<CardSlot> cardSlots, List<Card> availableCards)
    {
        var keyCards = new List<KeyCard>();
        var nonLandCards = cardSlots.Where(cs => !cs.Name.Contains("Island") && !cs.Name.Contains("Plains")).Take(5);

        foreach (var slot in nonLandCards)
        {
            keyCards.Add(new KeyCard
            {
                Name = slot.Name,
                Rationale = "Key card for the strategy"
            });
        }

        return keyCards;
    }

    private List<string> GenerateRisks(Deck deck, Domain.ValueObjects.GenerationRequest request)
    {
        var risks = new List<string>();

        // Check mana base
        var landCount = deck.Cards.Where(c => c.Name.Contains("Island") || c.Name.Contains("Plains")).Sum(c => c.Count);
        if (landCount < 20)
        {
            risks.Add("Mana base might be too light");
        }

        // Check budget
        if (request.Budget.HasValue && request.Budget.Value < 50)
        {
            risks.Add("Low budget may limit card quality");
        }

        return risks;
    }

    private string GenerateMulliganGuide(string style)
    {
        return style.ToLower() switch
        {
            "aggro" => "Keep hands with 2-3 lands and early threats (1-2 CMC).",
            "control" => "Keep hands with 3-4 lands and at least one removal spell.",
            "midrange" => "Keep hands with 2-4 lands and a good curve.",
            "combo" => "Keep hands with key combo pieces or tutors.",
            _ => "Keep hands with 2-4 lands and a reasonable curve."
        };
    }
}
