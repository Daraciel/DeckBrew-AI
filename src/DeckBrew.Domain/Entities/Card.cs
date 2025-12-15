using System;

namespace DeckBrew.Domain.Entities;

/// <summary>
/// Representa una carta de Magic: The Gathering con toda la información necesaria
/// para construcción de mazos, validación de legalidad y análisis de sinergias.
/// </summary>
public class Card
{
    /// <summary>
    /// Identificador único interno (GUID).
    /// </summary>
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>
    /// Identificador de Scryfall (UUID externo).
    /// </summary>
    public Guid ScryfallId { get; set; }

    /// <summary>
    /// Nombre de la carta.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Coste de maná (ej: "{2}{U}{R}").
    /// </summary>
    public string ManaCost { get; set; } = string.Empty;

    /// <summary>
    /// Converted Mana Cost / Mana Value.
    /// </summary>
    public int Cmc { get; set; }

    /// <summary>
    /// Línea de tipo (ej: "Creature — Human Wizard").
    /// </summary>
    public string TypeLine { get; set; } = string.Empty;

    /// <summary>
    /// Colores de la carta (ej: ["U", "R"]).
    /// </summary>
    public string[] Colors { get; set; } = Array.Empty<string>();

    /// <summary>
    /// Identidad de color (relevante para Commander).
    /// </summary>
    public string[] ColorIdentity { get; set; } = Array.Empty<string>();

    /// <summary>
    /// Rareza: common, uncommon, rare, mythic.
    /// </summary>
    public string Rarity { get; set; } = string.Empty;

    /// <summary>
    /// Código del set (ej: "MOM", "ONE").
    /// </summary>
    public string SetCode { get; set; } = string.Empty;

    /// <summary>
    /// Texto de reglas de la carta.
    /// </summary>
    public string OracleText { get; set; } = string.Empty;

    /// <summary>
    /// Poder (para criaturas).
    /// </summary>
    public string? Power { get; set; }

    /// <summary>
    /// Resistencia (para criaturas).
    /// </summary>
    public string? Toughness { get; set; }

    /// <summary>
    /// Lealtad inicial (para planeswalkers).
    /// </summary>
    public string? Loyalty { get; set; }

    /// <summary>
    /// Habilidades de la carta (ej: ["Flying", "Haste"].
    /// </summary>
    public string[] Keywords { get; set; } = Array.Empty<string>();

    /// <summary>
    /// Layout de la carta (normal, transform, modal_dfc, etc.).
    /// </summary>
    public string Layout { get; set; } = "normal";

    /// <summary>
    /// URLs de imágenes en diferentes tamaños (JSON serializado).
    /// </summary>
    public string ImageUrisJson { get; set; } = string.Empty;

    /// <summary>
    /// Precio en USD (puede ser null si no está disponible).
    /// </summary>
    public decimal? PriceUsd { get; set; }

    /// <summary>
    /// Precio en EUR (puede ser null si no está disponible).
    /// </summary>
    public decimal? PriceEur { get; set; }

    /// <summary>
    /// Formatos legales (JSON serializado: {"standard": "legal", "modern": "legal"}).
    /// </summary>
    public string LegalitiesJson { get; set; } = string.Empty;

    /// <summary>
    /// Fecha de lanzamiento de la carta.
    /// </summary>
    public DateTime ReleasedAt { get; set; }

    /// <summary>
    /// Última sincronización con Scryfall.
    /// </summary>
    public DateTime LastUpdated { get; set; } = DateTime.UtcNow;

    // Navigation properties (no se mapean directamente en algunos ORMs)
    public ICollection<DeckCard> DeckCards { get; set; } = new List<DeckCard>();
}
