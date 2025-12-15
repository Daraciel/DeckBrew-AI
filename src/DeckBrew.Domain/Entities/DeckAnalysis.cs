using System;

namespace DeckBrew.Domain.Entities;

/// <summary>
/// Representa el análisis generado para un mazo (caché de resultados).
/// Relación 1:1 con SavedDeck.
/// </summary>
public class DeckAnalysis
{
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>
    /// FK al mazo (relación 1:1).
    /// </summary>
    public Guid DeckId { get; set; }

    /// <summary>
    /// Key cards con razonamiento (JSON serializado).
    /// Ejemplo: [{"name": "Counterspell", "rationale": "Control key card"}]
    /// </summary>
    public string KeyCardsJson { get; set; } = string.Empty;

    /// <summary>
    /// Riesgos identificados (JSON array).
    /// Ejemplo: ["Mana base might be too light", "Vulnerable to board wipes"]
    /// </summary>
    public string RisksJson { get; set; } = string.Empty;

    /// <summary>
    /// Guía de mulligan.
    /// </summary>
    public string MulliganGuide { get; set; } = string.Empty;

    /// <summary>
    /// Datos de la curva de maná (JSON: {0: 5, 1: 8, 2: 12, ...}).
    /// </summary>
    public string ManaCurveDataJson { get; set; } = string.Empty;

    /// <summary>
    /// Distribución de colores (JSON: {"W": 15, "U": 20, "B": 5, ...}).
    /// </summary>
    public string ColorDistributionJson { get; set; } = string.Empty;

    /// <summary>
    /// Fecha de generación del análisis.
    /// </summary>
    public DateTime GeneratedAt { get; set; } = DateTime.UtcNow;

    // Navigation property
    public SavedDeck Deck { get; set; } = null!;
}