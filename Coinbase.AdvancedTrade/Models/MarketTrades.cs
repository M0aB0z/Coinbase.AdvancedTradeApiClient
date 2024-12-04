﻿using Coinbase.AdvancedTrade.Enums;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Coinbase.AdvancedTrade.Models;

/// <summary>
/// Represents an individual trade on the market.
/// </summary>
public class Trade
{
    /// <summary>
    /// Gets or sets the unique identifier for the trade.
    /// </summary>
    public string TradeId { get; set; }

    /// <summary>
    /// Gets or sets the product identifier associated with the trade.
    /// </summary>
    public string ProductId { get; set; }

    /// <summary>
    /// Gets or sets the price at which the trade occurred.
    /// </summary>
    public double Price { get; set; }

    /// <summary>
    /// Gets or sets the size or quantity of the asset that was traded.
    /// </summary>
    [JsonPropertyName("size")]
    public double Size { get; set; }

    /// <summary>
    /// Gets or sets the timestamp of the trade.
    /// </summary>
    public string Time { get; set; }

    /// <summary>
    /// Gets or sets the side of the trade (e.g., "buy" or "sell").
    /// </summary>
    public OrderSide Side { get; set; }

    /// <summary>
    /// Gets or sets the bid price at the time of the trade.
    /// </summary>
    public double Bid { get; set; }

    /// <summary>
    /// Gets or sets the ask price at the time of the trade.
    /// </summary>
    public double Ask { get; set; }
}

/// <summary>
/// Represents a collection of market trades along with the best bid and ask at the time.
/// </summary>
public class MarketTrades
{
    /// <summary>
    /// Gets or sets the list of trades.
    /// </summary>
    public IReadOnlyList<Trade> Trades { get; set; }

    /// <summary>
    /// Gets or sets the best bid price at the time of the data collection.
    /// </summary>
    public double BestBid { get; set; }

    /// <summary>
    /// Gets or sets the best ask price at the time of the data collection.
    /// </summary>
    public double BestAsk { get; set; }
}
