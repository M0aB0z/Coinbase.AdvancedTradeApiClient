using Coinbase.AdvancedTrade.Enums;
using Coinbase.AdvancedTrade.Utilities;
using Coinbase.AdvancedTrade.Utilities.Extensions;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Coinbase.AdvancedTrade.Models.Internal;

/// <summary>
/// Represents an individual trade on the market.
/// </summary>
internal class InternalTrade : IModelMapper<Trade>
{
    /// <summary>
    /// Gets or sets the unique identifier for the trade.
    /// </summary>
    [JsonPropertyName("trade_id")]
    public string TradeId { get; set; }

    /// <summary>
    /// Gets or sets the product identifier associated with the trade.
    /// </summary>
    [JsonPropertyName("product_id")]
    public string ProductId { get; set; }

    /// <summary>
    /// Gets or sets the price at which the trade occurred.
    /// </summary>
    [JsonPropertyName("price")]
    public string Price { get; set; }

    /// <summary>
    /// Gets or sets the size or quantity of the asset that was traded.
    /// </summary>
    [JsonPropertyName("size")]
    public string Size { get; set; }

    /// <summary>
    /// Gets or sets the timestamp of the trade.
    /// </summary>
    [JsonPropertyName("time")]
    public DateTime Time { get; set; }

    /// <summary>
    /// Gets or sets the side of the trade (e.g., "buy" or "sell").
    /// </summary>
    [JsonPropertyName("side")]
    public string Side { get; set; }

    /// <summary>
    /// Gets or sets the bid price at the time of the trade.
    /// </summary>
    [JsonPropertyName("bid")]
    public string Bid { get; set; }

    /// <summary>
    /// Gets or sets the ask price at the time of the trade.
    /// </summary>
    [JsonPropertyName("ask")]
    public string Ask { get; set; }

    /// <summary>
    /// Maps the internal model to the public model.
    /// </summary>
    /// <returns></returns>
    public Trade ToModel()
    {
        return new Trade
        {
            TradeId = TradeId,
            ProductId = ProductId,
            Price = Price.ToDouble(),
            Size = Size.ToDouble(),
            Time = Time,
            Side = Side.FromDescriptionToEnum<OrderSide>(),
            Bid = Bid.ToDouble(),
            Ask = Ask.ToDouble()
        };
    }
}

/// <summary>
/// Represents a collection of market trades along with the best bid and ask at the time.
/// </summary>
internal class InternalMarketTrades
{
    /// <summary>
    /// Gets or sets the list of trades.
    /// </summary>
    [JsonPropertyName("trades")]
    public IReadOnlyList<InternalTrade> Trades { get; set; }

    /// <summary>
    /// Gets or sets the best bid price at the time of the data collection.
    /// </summary>
    [JsonPropertyName("best_bid")]
    public string BestBid { get; set; }

    /// <summary>
    /// Gets or sets the best ask price at the time of the data collection.
    /// </summary>
    [JsonPropertyName("best_ask")]
    public string BestAsk { get; set; }
}
