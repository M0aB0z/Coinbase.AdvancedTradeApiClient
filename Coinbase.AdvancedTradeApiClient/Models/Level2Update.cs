using System;

namespace Coinbase.AdvancedTradeApiClient.Models;

/// <summary>
/// Represents a Level 2 update.
/// </summary>
public class Level2Update
{
    /// <summary>
    /// Gets or sets the side of the Level 2 update.
    /// </summary>
    public string Side { get; set; }

    /// <summary>
    /// Gets or sets the time when the Level 2 update occurred.
    /// </summary>
    public DateTime EventTime { get; set; }

    /// <summary>
    /// Gets or sets the price level for the Level 2 update.
    /// </summary>
    public decimal PriceLevel { get; set; }

    /// <summary>
    /// Gets or sets the new quantity for the Level 2 update.
    /// </summary>
    public decimal NewQuantity { get; set; }
}
