using Coinbase.AdvancedTrade.Enums;
using System;
using System.Collections.Generic;

namespace Coinbase.AdvancedTrade.Models;

// Represents an order within the Coinbase AdvancedTrade system.
/// <summary>
/// Represents an order within the Coinbase AdvancedTrade system.
/// </summary>
public class Order
{
    /// <summary>
    /// The unique identifier for the order.
    /// </summary>
    public string OrderId { get; set; }

    /// <summary>
    /// The identifier for the product associated with the order.
    /// </summary>
    public string ProductId { get; set; }

    /// <summary>
    /// The user identifier who created the order.
    /// </summary>
    public string UserId { get; set; }

    /// <summary>
    /// Configuration details for the order.
    /// </summary>
    public OrderConfiguration OrderConfiguration { get; set; }

    /// <summary>
    /// Indicates if the order is a buy or sell.
    /// </summary>
    public OrderSide Side { get; set; }

    /// <summary>
    /// The client's custom identifier for the order.
    /// </summary>
    public string ClientOrderId { get; set; }

    /// <summary>
    /// The current status of the order.
    /// </summary>
    public string Status { get; set; }

    /// <summary>
    /// How long the order will remain active.
    /// </summary>
    public string TimeInForce { get; set; }

    /// <summary>
    /// The timestamp when the order was created.
    /// </summary>
    public DateTime? CreatedTime { get; set; }

    /// <summary>
    /// The percentage of the order that has been completed.
    /// </summary>
    public double? CompletionPercentage { get; set; }

    /// <summary>
    /// The quantity of the product that has been filled in the order.
    /// </summary>
    public double? FilledSize { get; set; }

    /// <summary>
    /// The average price at which the order has been filled.
    /// </summary>
    public double? AverageFilledPrice { get; set; }

    /// <summary>
    /// The fee associated with the order.
    /// </summary>
    public double? Fee { get; set; }

    /// <summary>
    /// The number of times the order has been filled.
    /// </summary>
    public double? NumberOfFills { get; set; }

    /// <summary>
    /// The total value of the filled portions of the order.
    /// </summary>
    public double? FilledValue { get; set; }

    /// <summary>
    /// Indicates if the order is pending cancellation.
    /// </summary>
    public bool? PendingCancel { get; set; }

    /// <summary>
    /// If true, the size of the order is specified in the quote currency.
    /// </summary>
    public bool? SizeInQuote { get; set; }

    /// <summary>
    /// The total fees associated with the order.
    /// </summary>
    public double? TotalFees { get; set; }

    /// <summary>
    /// If true, the size of the order includes fees.
    /// </summary>
    public bool? SizeInclusiveOfFees { get; set; }

    /// <summary>
    /// The total value of the order after fees have been deducted.
    /// </summary>
    public double? TotalValueAfterFees { get; set; }

    /// <summary>
    /// The status of the order's trigger if applicable.
    /// </summary>
    public string TriggerStatus { get; set; }

    /// <summary>
    /// The type of order (e.g. market, limit, stop).
    /// </summary>
    public OrderType OrderType { get; set; }

    /// <summary>
    /// Reason for order rejection, if applicable.
    /// </summary>
    public string RejectReason { get; set; }

    /// <summary>
    /// Indicates if the order has been settled.
    /// </summary>
    public bool? Settled { get; set; }

    /// <summary>
    /// The type of product associated with the order.
    /// </summary>
    public string ProductType { get; set; }

    /// <summary>
    /// A message providing more details about the rejection reason.
    /// </summary>
    public string RejectMessage { get; set; }

    /// <summary>
    /// A message providing more details if the order was canceled.
    /// </summary>
    public string CancelMessage { get; set; }

    /// <summary>
    /// The source from which the order was placed (e.g. web, API).
    /// </summary>
    public string OrderPlacementSource { get; set; }

    /// <summary>
    /// The amount of the order that is currently on hold.
    /// </summary>
    public string OutstandingHoldAmount { get; set; }

    /// <summary>
    /// Indicates if the order is a liquidation order.
    /// </summary>
    public bool? IsLiquidation { get; set; }

    /// <summary>
    /// An array of the latest 5 edits per order.
    /// </summary>
    public List<EditHistoryEntry> EditHistory { get; set; }
}


/// <summary>
/// Represents specific configurations for different types of orders.
/// </summary>
public class OrderConfiguration
{
    /// <summary>
    /// Configuration details for market-market IOC orders.
    /// </summary>
    public MarketIoc MarketIoc { get; set; }

    /// <summary>
    /// Configuration details for limit-limit GTC orders.
    /// </summary>
    public LimitGtc LimitGtc { get; set; }

    /// <summary>
    /// Configuration details for limit-limit GTD orders.
    /// </summary>
    public LimitGtd LimitGtd { get; set; }

    /// <summary>
    /// Configuration details for stop-limit-stop-limit GTC orders.
    /// </summary>
    public StopLimitGtc StopLimitGtc { get; set; }

    /// <summary>
    /// Configuration details for stop-limit-stop-limit GTD orders.
    /// </summary>
    public StopLimitGtd StopLimitGtd { get; set; }

    /// <summary>
    /// Configuration details for sor-limit-ioc orders.
    /// </summary>
    public SorLimitIoc SorLimitIoc { get; set; }
}

/// <summary>
/// Represents configuration details for market-market IOC orders.
/// </summary>
public class MarketIoc
{
    /// <summary>
    /// The size of the order in the quote currency.
    /// </summary>
    public double? QuoteSize { get; set; }

    /// <summary>
    /// The size of the order in the base currency.
    /// </summary>
    public double? BaseSize { get; set; }
}

/// <summary>
/// Represents configuration details for limit-limit GTC orders.
/// </summary>
public class LimitGtc
{
    /// <summary>
    /// The size of the order in the base currency.
    /// </summary>
    public double? BaseSize { get; set; }

    /// <summary>
    /// The limit price for the order.
    /// </summary>
    public double? LimitPrice { get; set; }

    /// <summary>
    /// Indicates if the order can only be posted to the order book.
    /// </summary>
    public bool? PostOnly { get; set; }
}

/// <summary>
/// Represents configuration details for limit-limit GTD orders.
/// </summary>
public class LimitGtd : LimitGtc
{
    /// <summary>
    /// The time when the order will expire.
    /// </summary>
    public DateTime EndTime { get; set; }
}

/// <summary>
/// Represents configuration details for stop-limit-stop-limit GTC orders.
/// </summary>
public class StopLimitGtc
{
    /// <summary>
    /// The size of the order in the base currency.
    /// </summary>
    public double? BaseSize { get; set; }

    /// <summary>
    /// The limit price for the order.
    /// </summary>
    public double? LimitPrice { get; set; }

    /// <summary>
    /// The stop price for the order.
    /// </summary>
    public double? StopPrice { get; set; }

    /// <summary>
    /// The direction in which the stop price is triggered (e.g. 'above', 'below').
    /// </summary>
    public OrderDirection StopDirection { get; set; }
}

/// <summary>
/// Represents configuration details for stop-limit-stop-limit GTD orders.
/// </summary>
public class StopLimitGtd : StopLimitGtc
{
    /// <summary>
    /// The time when the order will expire.
    /// </summary>
    public DateTime EndTime { get; set; }
}

/// <summary>
/// Represents configuration details for sor-limit-ioc orders.
/// </summary>
public class SorLimitIoc
{
    /// <summary>
    /// The size of the order in the base currency.
    /// </summary>
    public double? BaseSize { get; set; }

    /// <summary>
    /// The limit price for the order.
    /// </summary>
    public double? LimitPrice { get; set; }
}

/// <summary>
/// Represents an edit history entry for an order.
/// </summary>
public class EditHistoryEntry
{
    /// <summary>
    /// The price associated with the edit.
    /// </summary>
    public double? Price { get; set; }

    /// <summary>
    /// The size associated with the edit.
    /// </summary>
    public double? Size { get; set; }

    /// <summary>
    /// The timestamp when the edit was accepted.
    /// </summary>
    public DateTime? ReplaceAcceptTimestamp { get; set; }
}
