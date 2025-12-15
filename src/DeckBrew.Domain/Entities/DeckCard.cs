using System;

namespace DeckBrew.Domain.Entities;

/// <summary>
/// Representa la relación entre un mazo y una carta (tabla de unión).
/// </summary>
public class DeckCard
{
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>
    /// FK al mazo guardado.
    /// </summary>
    public Guid DeckId { get; set; }

    /// <summary>
    /// FK a la carta.
    /// </summary>
    public Guid CardId { get; set; }

    /// <summary>
    /// Cantidad de copias de esta carta en el mazo.
    /// </summary>
    public int Count { get; set; }

    /// <summary>
    /// Zona: "main", "sideboard", "commander".
    /// </summary>
    public string Zone { get; set; } = "main";

    /// <summary>
    /// Categoría opcional para organización (ej: "removal", "ramp", "finisher").
    /// </summary>
    public string? Category { get; set; }

    // Navigation properties
    public SavedDeck Deck { get; set; } = null!;
    public Card Card { get; set; } = null!;
}