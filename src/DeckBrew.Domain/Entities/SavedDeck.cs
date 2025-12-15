using System;
using System.Collections.Generic;

namespace DeckBrew.Domain.Entities;

/// <summary>
/// Representa un mazo guardado por el usuario con metadatos y análisis.
/// </summary>
public class SavedDeck
{
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>
    /// ID del usuario (futuro, para autenticación).
    /// </summary>
    public Guid? UserId { get; set; }

    /// <summary>
    /// Nombre del mazo.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Formato (Standard, Modern, Commander, etc.).
    /// </summary>
    public string Format { get; set; } = string.Empty;

    /// <summary>
    /// Colores del mazo (ej: ["U", "R"]).
    /// </summary>
    public string[] Colors { get; set; } = Array.Empty<string>();

    /// <summary>
    /// Estilo del mazo (aggro, control, midrange, combo).
    /// </summary>
    public string Style { get; set; } = string.Empty;

    /// <summary>
    /// Total de cartas en el mazo (60 o 100).
    /// </summary>
    public int TotalCards { get; set; }

    /// <summary>
    /// Cantidad de cartas en el maindeck.
    /// </summary>
    public int MainDeckCount { get; set; }

    /// <summary>
    /// Cantidad de cartas en el sideboard.
    /// </summary>
    public int SideboardCount { get; set; }

    /// <summary>
    /// Presupuesto total del mazo.
    /// </summary>
    public decimal? Budget { get; set; }

    /// <summary>
    /// Fecha de creación.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Última modificación.
    /// </summary>
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Indica si el mazo está archivado.
    /// </summary>
    public bool IsArchived { get; set; }

    /// <summary>
    /// Notas del usuario sobre el mazo.
    /// </summary>
    public string? Notes { get; set; }

    // Navigation properties
    public ICollection<DeckCard> DeckCards { get; set; } = new List<DeckCard>();
    public DeckAnalysis? Analysis { get; set; }
}