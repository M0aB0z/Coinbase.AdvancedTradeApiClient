using Coinbase.AdvancedTradeApiClient.Enums;

namespace Coinbase.AdvancedTradeApiClient.Models;

/// <summary>
/// Represents a fill, which is a completed trade on the exchange. A fill is created for each side of the trade.
/// </summary>
public class Fill
{
    /// <summary>
    /// Gets the unique identifier for this fill entry.
    /// </summary>
    public string EntryId { get; internal set; }

    /// <summary>
    /// Gets the identifier of the trade associated with this fill.
    /// </summary>
    public string TradeId { get; internal set; }

    /// <summary>
    /// Gets the identifier of the order associated with this fill.
    /// </summary>
    public string OrderId { get; internal set; }

    /// <summary>
    /// Gets the time the trade was executed.
    /// </summary>
    public string TradeTime { get; internal set; }

    /// <summary>
    /// Gets the type of the trade.
    /// </summary>
    public string TradeType { get; internal set; }

    /// <summary>
    /// Gets the price at which the trade was executed.
    /// </summary>
    public decimal Price { get; internal set; }

    /// <summary>
    /// Gets the size of the asset traded.
    /// </summary>
    public double Size { get; internal set; }

    /// <summary>
    /// Gets the commission or fee taken by the exchange for executing the trade.
    /// </summary>
    public decimal Commission { get; internal set; }

    /// <summary>
    /// Gets the identifier of the product being traded.
    /// </summary>
    public string ProductId { get; internal set; }

    /// <summary>
    /// Gets the sequence timestamp for the fill, which indicates the order in which it was processed.
    /// </summary>
    public string SequenceTimestamp { get; internal set; }

    /// <summary>
    /// Gets an indicator for the liquidity of the trade. E.g., "M" for maker or "T" for taker.
    /// </summary>
    public string LiquidityIndicator { get; internal set; }

    /// <summary>
    /// Gets a value indicating whether the size value is in quote currency.
    /// </summary>
    public bool SizeInQuote { get; internal set; }

    /// <summary>
    /// Gets the identifier of the user associated with the fill.
    /// </summary>
    public string UserId { get; internal set; }

    /// <summary>
    /// Gets the side of the trade, e.g., "buy" or "sell".
    /// </summary>
    public OrderSide Side { get; internal set; }
}
