using Coinbase.AdvancedTradeApiClient.Enums;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Coinbase.AdvancedTradeApiClient.Models;

/// <summary>
/// Represents an individual trade on the market.
/// </summary>
public class Trade
{
    /// <summary>
    /// Gets the unique identifier for the trade.
    /// </summary>
    public string TradeId { get; internal set; }

    /// <summary>
    /// Gets the product identifier associated with the trade.
    /// </summary>
    public string ProductId { get; internal set; }

    /// <summary>
    /// Gets the price at which the trade occurred.
    /// </summary>
    public decimal Price { get; internal set; }

    /// <summary>
    /// Gets the size or quantity of the asset that was traded.
    /// </summary>
    public decimal Size { get; internal set; }

    /// <summary>
    /// Gets the timestamp of the trade.
    /// </summary>
    public DateTime Time { get; internal set; }

    /// <summary>
    /// Gets the side of the trade (e.g., "buy" or "sell").
    /// </summary>
    public OrderSide Side { get; internal set; }


    /// <inheritDoc/>
    public override string ToString()
        => $"{Time} {Side} {Math.Round(Size, 4)} {ProductId} @{Math.Round(Price, 4)}";
}

/// <summary>
/// Represents a collection of market trades along with the best bid and ask at the time.
/// </summary>
public class MarketTrades
{
    /// <summary>
    /// Gets the list of trades.
    /// </summary>
    public IReadOnlyList<Trade> Trades { get; internal set; }

    /// <summary>
    /// Gets the best bid price at the time of the data collection.
    /// </summary>
    public decimal BestBid { get; internal set; }

    /// <summary>
    /// Gets the best ask price at the time of the data collection.
    /// </summary>
    public decimal BestAsk { get; internal set; }
}
