using System;

namespace DeckBrew.Domain.Entities;

/// <summary>
/// Representa un set/colección de Magic: The Gathering.
/// </summary>
public class MtgSet
{
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>
    /// Código del set (ej: "MOM", "ONE").
    /// </summary>
    public string Code { get; set; } = string.Empty;

    /// <summary>
    /// Nombre del set (ej: "March of the Machine").
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Fecha de lanzamiento.
    /// </summary>
    public DateTime ReleasedAt { get; set; }

    /// <summary>
    /// Tipo de set (expansion, core, masters, etc.).
    /// </summary>
    public string SetType { get; set; } = string.Empty;

    /// <summary>
    /// Cantidad de cartas en el set.
    /// </summary>
    public int CardCount { get; set; }

    /// <summary>
    /// Indica si es un set digital.
    /// </summary>
    public bool IsDigital { get; set; }
}