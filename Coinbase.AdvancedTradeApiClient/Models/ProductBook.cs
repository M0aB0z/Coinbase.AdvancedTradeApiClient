using System;
using System.Collections.Generic;

namespace Coinbase.AdvancedTradeApiClient.Models;

/// <summary>
/// Represents the order book for a specific product on Coinbase.
/// </summary>
public class ProductBook
{
    /// <summary>
    /// Gets the product identifier associated with the order book.
    /// </summary>
    public string ProductId { get; internal set; }

    /// <summary>
    /// Gets the list of buy orders for the product.
    /// </summary>
    public List<Offer> Bids { get; internal set; }

    /// <summary>
    /// Gets the list of sell orders for the product.
    /// </summary>
    public List<Offer> Asks { get; internal set; }

    /// <summary>
    /// Gets the timestamp when the order book was captured.
    /// </summary>
    public DateTime Time { get; internal set; }
}

/// <summary>
/// Represents an individual offer (bid or ask) in the order book.
/// </summary>
public class Offer
{
    /// <summary>
    /// Gets the price at which the offer is made.
    /// </summary>
    public double? Price { get; internal set; }

    /// <summary>
    /// Gets the size or quantity of the offer.
    /// </summary>
    public double? Size { get; internal set; }
}
