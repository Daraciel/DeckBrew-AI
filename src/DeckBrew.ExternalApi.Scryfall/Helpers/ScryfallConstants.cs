namespace DeckBrew.ExternalApi.Scryfall.Helpers;

/// <summary>
/// Constants for Scryfall API
/// </summary>
public static class ScryfallConstants
{
    public static class Colors
    {
        public const string White = "w";
        public const string Blue = "u";
        public const string Black = "b";
        public const string Red = "r";
        public const string Green = "g";
        public const string Colorless = "c";
    }

    public static class ColorIdentities
    {
        public const string Azorius = "wu";
        public const string Dimir = "ub";
        public const string Rakdos = "br";
        public const string Gruul = "rg";
        public const string Selesnya = "gw";
        public const string Orzhov = "wb";
        public const string Izzet = "ur";
        public const string Golgari = "bg";
        public const string Boros = "rw";
        public const string Simic = "gu";
        
        public const string Bant = "gwu";
        public const string Esper = "wub";
        public const string Grixis = "ubr";
        public const string Jund = "brg";
        public const string Naya = "rgw";
        public const string Abzan = "wbg";
        public const string Jeskai = "urw";
        public const string Sultai = "bgu";
        public const string Mardu = "rwb";
        public const string Temur = "gur";
        
        public const string FourColorNoWhite = "ubrg";
        public const string FourColorNoBlue = "brgw";
        public const string FourColorNoBlack = "rgwu";
        public const string FourColorNoRed = "gwub";
        public const string FourColorNoGreen = "wubr";
        
        public const string FiveColor = "wubrg";
    }

    public static class Formats
    {
        public const string Standard = "standard";
        public const string Pioneer = "pioneer";
        public const string Modern = "modern";
        public const string Legacy = "legacy";
        public const string Vintage = "vintage";
        public const string Commander = "commander";
        public const string Pauper = "pauper";
        public const string Historic = "historic";
        public const string Brawl = "brawl";
        public const string Future = "future";
        public const string Penny = "penny";
    }

    public static class Legalities
    {
        public const string Legal = "legal";
        public const string NotLegal = "not_legal";
        public const string Restricted = "restricted";
        public const string Banned = "banned";
    }

    public static class Rarities
    {
        public const string Common = "common";
        public const string Uncommon = "uncommon";
        public const string Rare = "rare";
        public const string Mythic = "mythic";
        public const string Special = "special";
        public const string Bonus = "bonus";
    }

    public static class Types
    {
        public const string Creature = "creature";
        public const string Instant = "instant";
        public const string Sorcery = "sorcery";
        public const string Artifact = "artifact";
        public const string Enchantment = "enchantment";
        public const string Land = "land";
        public const string Planeswalker = "planeswalker";
        public const string Battle = "battle";
        public const string Legendary = "legendary";
    }

    public static class Layouts
    {
        public const string Normal = "normal";
        public const string Split = "split";
        public const string Flip = "flip";
        public const string Transform = "transform";
        public const string ModalDfc = "modal_dfc";
        public const string Meld = "meld";
        public const string Leveler = "leveler";
        public const string Saga = "saga";
        public const string Adventure = "adventure";
        public const string Planar = "planar";
        public const string Scheme = "scheme";
        public const string Vanguard = "vanguard";
        public const string Token = "token";
        public const string DoubleFacedToken = "double_faced_token";
        public const string Emblem = "emblem";
        public const string Augment = "augment";
        public const string Host = "host";
        public const string ArtSeries = "art_series";
        public const string ReversibleCard = "reversible_card";
    }

    public static class SortOrders
    {
        public const string Name = "name";
        public const string Set = "set";
        public const string Released = "released";
        public const string Rarity = "rarity";
        public const string Color = "color";
        public const string Usd = "usd";
        public const string Tix = "tix";
        public const string Eur = "eur";
        public const string Cmc = "cmc";
        public const string Power = "power";
        public const string Toughness = "toughness";
        public const string Edhrec = "edhrec";
        public const string Artist = "artist";
    }

    public static class SortDirections
    {
        public const string Auto = "auto";
        public const string Ascending = "asc";
        public const string Descending = "desc";
    }

    public static class UniqueStrategies
    {
        public const string Cards = "cards";
        public const string Art = "art";
        public const string Prints = "prints";
    }
}
