using DeckBrew.ExternalApi.Scryfall;
using DeckBrew.ExternalApi.Scryfall.Helpers;

namespace DeckBrew.ExternalApi.Scryfall.Examples;

/// <summary>
/// Examples of how to use the Scryfall service
/// </summary>
public class ScryfallUsageExamples
{
    private readonly IScryfallService _scryfallService;

    public ScryfallUsageExamples(IScryfallService scryfallService)
    {
        _scryfallService = scryfallService;
    }

    #region Basic Card Search

    public async Task SearchByName()
    {
        // Search for a specific card
        var card = await _scryfallService.GetCardByNameAsync("Lightning Bolt");
        
        if (card != null)
        {
            Console.WriteLine($"Found: {card.Name}");
            Console.WriteLine($"Mana Cost: {card.ManaCost}");
            Console.WriteLine($"Type: {card.TypeLine}");
            Console.WriteLine($"Text: {card.OracleText}");
        }
    }

    public async Task AutocompleteSearch()
    {
        // Autocomplete for user input
        var suggestions = await _scryfallService.AutocompleteCardNameAsync("teferi");
        
        foreach (var suggestion in suggestions.Take(5))
        {
            Console.WriteLine($"- {suggestion}");
        }
    }

    public async Task RandomCard()
    {
        // Get a random legendary creature
        var randomCommander = await _scryfallService.GetRandomCardAsync("t:legendary t:creature");
        Console.WriteLine($"Random Commander: {randomCommander.Name}");
    }

    #endregion

    #region Commander Searches

    public async Task FindGrixisCommanders()
    {
        // Find all Grixis commanders
        var commanders = await _scryfallService.SearchCommandersAsync(ScryfallConstants.ColorIdentities.Grixis);
        
        Console.WriteLine($"Found {commanders.Count()} Grixis commanders:");
        foreach (var commander in commanders.Take(10))
        {
            Console.WriteLine($"- {commander.Name} ({commander.ManaCost})");
        }
    }

    public async Task FindBudgetCommanders()
    {
        // Find commanders under $5
        var query = ScryfallQueryBuilder.Create()
            .IsCommander()
            .Custom("usd<5")
            .Build();

        var commanders = await _scryfallService.SearchCardsAsync(query);
        
        Console.WriteLine("Budget Commanders (under $5):");
        foreach (var commander in commanders.Take(10))
        {
            var price = commander.Prices?.Usd ?? "N/A";
            Console.WriteLine($"- {commander.Name}: ${price}");
        }
    }

    public async Task FindTribalCommanders()
    {
        // Find elf tribal commanders
        var query = ScryfallQueryBuilder.Create()
            .IsCommander()
            .WithOracleText("Elf")
            .Build();

        var commanders = await _scryfallService.SearchCardsAsync(query);
        
        Console.WriteLine("Elf Tribal Commanders:");
        foreach (var commander in commanders)
        {
            Console.WriteLine($"- {commander.Name}");
        }
    }

    #endregion

    #region Advanced Query Building

    public async Task ComplexQueryExample()
    {
        // Find red/green creatures with power >= 5, CMC <= 6, legal in Commander
        var query = ScryfallQueryBuilder.Create()
            .WithColor(ScryfallConstants.Colors.Red, ScryfallConstants.Colors.Green)
            .WithType(ScryfallConstants.Types.Creature)
            .WithPower(5, ">=")
            .WithCmc(6, "<=")
            .WithFormat(ScryfallConstants.Formats.Commander)
            .Build();

        var cards = await _scryfallService.SearchCardsAsync(query);
        
        Console.WriteLine($"Found {cards.Count()} big R/G creatures:");
        foreach (var card in cards.Take(10))
        {
            Console.WriteLine($"- {card.Name} ({card.Power}/{card.Toughness}) - {card.ManaCost}");
        }
    }

    public async Task FindCardsWithKeyword()
    {
        // Find cards with "Cascade"
        var query = ScryfallQueryBuilder.Create()
            .WithKeyword("Cascade")
            .WithFormat(ScryfallConstants.Formats.Commander)
            .Build();

        var cards = await _scryfallService.SearchCardsAsync(query);
        
        Console.WriteLine("Cards with Cascade:");
        foreach (var card in cards.Take(10))
        {
            Console.WriteLine($"- {card.Name}");
        }
    }

    #endregion

    #region Sets and Symbology

    public async Task GetLatestSets()
    {
        var sets = await _scryfallService.GetAllSetsAsync();
        
        var recentSets = sets
            .Where(s => s.ReleasedAt.HasValue)
            .OrderByDescending(s => s.ReleasedAt)
            .Take(10);

        Console.WriteLine("Recent Sets:");
        foreach (var set in recentSets)
        {
            Console.WriteLine($"- {set.Name} ({set.Code}) - {set.ReleasedAt:yyyy-MM-dd}");
        }
    }

    public async Task GetManaSymbols()
    {
        var symbols = await _scryfallService.GetAllSymbolsAsync();
        
        var manaSymbols = symbols.Where(s => s.RepresentsMana);

        Console.WriteLine("Mana Symbols:");
        foreach (var symbol in manaSymbols)
        {
            Console.WriteLine($"- {symbol.Symbol}: {symbol.English}");
        }
    }

    public async Task ParseManaCost()
    {
        var cost = await _scryfallService.ParseManaCostAsync("{2}{U}{U}{R}");
        
        if (cost != null)
        {
            Console.WriteLine($"Mana Cost: {cost.Cost}");
            Console.WriteLine($"CMC: {cost.Cmc}");
            Console.WriteLine($"Colors: {string.Join(", ", cost.Colors ?? new List<string>())}");
            Console.WriteLine($"Multicolored: {cost.Multicolored}");
        }
    }

    #endregion

    #region Images

    public async Task DownloadCardImage()
    {
        var card = await _scryfallService.GetCardByNameAsync("Black Lotus");
        
        if (card?.ImageUris?.Png != null)
        {
            var imageStream = await _scryfallService.GetCardImageAsync(card.ImageUris.Png);
            
            // Save to file
            using var fileStream = File.Create("black_lotus.png");
            await imageStream.CopyToAsync(fileStream);
            
            Console.WriteLine("Image downloaded successfully!");
        }
    }

    #endregion

    #region Deck Building Helpers

    public async Task FindRampSpells()
    {
        // Find green ramp spells
        var query = ScryfallQueryBuilder.Create()
            .WithColor(ScryfallConstants.Colors.Green)
            .WithOracleText("search your library for")
            .WithType("instant OR sorcery")
            .WithCmc(4, "<=")
            .WithFormat(ScryfallConstants.Formats.Commander)
            .Build();

        var rampSpells = await _scryfallService.SearchCardsAsync(query);
        
        Console.WriteLine("Green Ramp Spells (CMC <= 4):");
        foreach (var spell in rampSpells.Take(10))
        {
            Console.WriteLine($"- {spell.Name} ({spell.ManaCost})");
        }
    }

    public async Task FindCardDraw()
    {
        // Find blue card draw spells
        var query = ScryfallQueryBuilder.Create()
            .WithColor(ScryfallConstants.Colors.Blue)
            .WithOracleText("draw")
            .WithFormat(ScryfallConstants.Formats.Commander)
            .Build();

        var drawSpells = await _scryfallService.SearchCardsAsync(query);
        
        Console.WriteLine("Blue Card Draw Spells:");
        foreach (var spell in drawSpells.Take(10))
        {
            Console.WriteLine($"- {spell.Name}");
        }
    }

    public async Task FindRemovalSpells()
    {
        // Find black removal spells
        var query = ScryfallQueryBuilder.Create()
            .WithColor(ScryfallConstants.Colors.Black)
            .WithOracleText("destroy target")
            .WithType("instant OR sorcery")
            .WithFormat(ScryfallConstants.Formats.Commander)
            .Build();

        var removalSpells = await _scryfallService.SearchCardsAsync(query);
        
        Console.WriteLine("Black Removal Spells:");
        foreach (var spell in removalSpells.Take(10))
        {
            Console.WriteLine($"- {spell.Name}");
        }
    }

    #endregion

    #region Catalog Queries

    public async Task GetCreatureTypes()
    {
        var types = await _scryfallService.GetCreatureTypesAsync();
        
        Console.WriteLine($"Total Creature Types: {types.Count()}");
        Console.WriteLine("Sample creature types:");
        foreach (var type in types.Take(20))
        {
            Console.WriteLine($"- {type}");
        }
    }

    public async Task GetKeywords()
    {
        var keywords = await _scryfallService.GetKeywordAbilitiesAsync();
        
        Console.WriteLine("Keyword Abilities:");
        foreach (var keyword in keywords.Take(20))
        {
            Console.WriteLine($"- {keyword}");
        }
    }

    #endregion
}
