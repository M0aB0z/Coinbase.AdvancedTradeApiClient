using Coinbase.AdvancedTrade.Enums;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Coinbase.AdvancedTrade.Models;

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
    public double Price { get; internal set; }

    /// <summary>
    /// Gets the size or quantity of the asset that was traded.
    /// </summary>
    [JsonPropertyName("size")]
    public double Size { get; internal set; }

    /// <summary>
    /// Gets the timestamp of the trade.
    /// </summary>
    public string Time { get; internal set; }

    /// <summary>
    /// Gets the side of the trade (e.g., "buy" or "sell").
    /// </summary>
    public OrderSide Side { get; internal set; }

    /// <summary>
    /// Gets the bid price at the time of the trade.
    /// </summary>
    public double Bid { get; internal set; }

    /// <summary>
    /// Gets the ask price at the time of the trade.
    /// </summary>
    public double Ask { get; internal set; }
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
    public double BestBid { get; internal set; }

    /// <summary>
    /// Gets the best ask price at the time of the data collection.
    /// </summary>
    public double BestAsk { get; internal set; }
}
