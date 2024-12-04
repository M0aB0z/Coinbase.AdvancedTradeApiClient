﻿using Coinbase.AdvancedTrade.Enums;

namespace Coinbase.AdvancedTrade.Models;

/// <summary>
/// Represents a fill, which is a completed trade on the exchange. A fill is created for each side of the trade.
/// </summary>
public class Fill
{
    /// <summary>
    /// Gets or sets the unique identifier for this fill entry.
    /// </summary>
    public string EntryId { get; set; }

    /// <summary>
    /// Gets or sets the identifier of the trade associated with this fill.
    /// </summary>
    public string TradeId { get; set; }

    /// <summary>
    /// Gets or sets the identifier of the order associated with this fill.
    /// </summary>
    public string OrderId { get; set; }

    /// <summary>
    /// Gets or sets the time the trade was executed.
    /// </summary>
    public string TradeTime { get; set; }

    /// <summary>
    /// Gets or sets the type of the trade.
    /// </summary>
    public string TradeType { get; set; }

    /// <summary>
    /// Gets or sets the price at which the trade was executed.
    /// </summary>
    public double Price { get; set; }

    /// <summary>
    /// Gets or sets the size of the asset traded.
    /// </summary>
    public double Size { get; set; }

    /// <summary>
    /// Gets or sets the commission or fee taken by the exchange for executing the trade.
    /// </summary>
    public double Commission { get; set; }

    /// <summary>
    /// Gets or sets the identifier of the product being traded.
    /// </summary>
    public string ProductId { get; set; }

    /// <summary>
    /// Gets or sets the sequence timestamp for the fill, which indicates the order in which it was processed.
    /// </summary>
    public string SequenceTimestamp { get; set; }

    /// <summary>
    /// Gets or sets an indicator for the liquidity of the trade. E.g., "M" for maker or "T" for taker.
    /// </summary>
    public string LiquidityIndicator { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the size value is in quote currency.
    /// </summary>
    public bool SizeInQuote { get; set; }

    /// <summary>
    /// Gets or sets the identifier of the user associated with the fill.
    /// </summary>
    public string UserId { get; set; }

    /// <summary>
    /// Gets or sets the side of the trade, e.g., "buy" or "sell".
    /// </summary>
    public OrderSide Side { get; set; }
}
