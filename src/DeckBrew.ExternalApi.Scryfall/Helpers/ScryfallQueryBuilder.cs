namespace DeckBrew.ExternalApi.Scryfall.Helpers;

/// <summary>
/// Helper class for building Scryfall search queries
/// </summary>
public class ScryfallQueryBuilder
{
    private readonly List<string> _filters = new();

    public ScryfallQueryBuilder WithColor(params string[] colors)
    {
        if (colors.Length > 0)
        {
            _filters.Add($"c:{string.Join("", colors).ToLower()}");
        }
        return this;
    }

    public ScryfallQueryBuilder WithColorIdentity(params string[] colors)
    {
        if (colors.Length > 0)
        {
            _filters.Add($"id:{string.Join("", colors).ToLower()}");
        }
        return this;
    }

    public ScryfallQueryBuilder WithCommanderIdentity(params string[] colors)
    {
        if (colors.Length > 0)
        {
            _filters.Add($"commander:{string.Join("", colors).ToLower()}");
        }
        return this;
    }

    public ScryfallQueryBuilder WithType(string type)
    {
        _filters.Add($"t:{type}");
        return this;
    }

    public ScryfallQueryBuilder IsCommander()
    {
        _filters.Add("is:commander");
        return this;
    }

    public ScryfallQueryBuilder IsLegendary()
    {
        _filters.Add("t:legendary");
        return this;
    }

    public ScryfallQueryBuilder WithFormat(string format, string legality = "legal")
    {
        _filters.Add($"f:{format}");
        return this;
    }

    public ScryfallQueryBuilder WithCmc(int cmc, string comparison = "=")
    {
        _filters.Add($"cmc{comparison}{cmc}");
        return this;
    }

    public ScryfallQueryBuilder WithCmcRange(int min, int max)
    {
        _filters.Add($"cmc>={min} cmc<={max}");
        return this;
    }

    public ScryfallQueryBuilder WithSet(string setCode)
    {
        _filters.Add($"set:{setCode}");
        return this;
    }

    public ScryfallQueryBuilder WithRarity(string rarity)
    {
        _filters.Add($"r:{rarity}");
        return this;
    }

    public ScryfallQueryBuilder WithOracleText(string text)
    {
        _filters.Add($"o:\"{text}\"");
        return this;
    }

    public ScryfallQueryBuilder WithPower(int power, string comparison = "=")
    {
        _filters.Add($"pow{comparison}{power}");
        return this;
    }

    public ScryfallQueryBuilder WithToughness(int toughness, string comparison = "=")
    {
        _filters.Add($"tou{comparison}{toughness}");
        return this;
    }

    public ScryfallQueryBuilder WithKeyword(string keyword)
    {
        _filters.Add($"o:{keyword}");
        return this;
    }

    public ScryfallQueryBuilder NotColor(params string[] colors)
    {
        if (colors.Length > 0)
        {
            _filters.Add($"-c:{string.Join("", colors).ToLower()}");
        }
        return this;
    }

    public ScryfallQueryBuilder IsUnique()
    {
        _filters.Add("is:unique");
        return this;
    }

    public ScryfallQueryBuilder Custom(string filter)
    {
        _filters.Add(filter);
        return this;
    }

    public string Build()
    {
        return string.Join(" ", _filters);
    }

    public static ScryfallQueryBuilder Create() => new();

    public override string ToString() => Build();
}
