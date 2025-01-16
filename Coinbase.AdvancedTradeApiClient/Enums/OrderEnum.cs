using System.ComponentModel;

namespace Coinbase.AdvancedTradeApiClient.Enums;

/// <summary>
/// Represents the status of an order.
/// </summary>
public enum OrderStatus
{
    /// <summary>
    /// The order is pending.
    /// </summary>
    PENDING,

    /// <summary>
    /// The order is open.
    /// </summary>
    OPEN,

    /// <summary>
    /// The order is filled.
    /// </summary>
    FILLED,

    /// <summary>
    /// The order is cancelled.
    /// </summary>
    CANCELLED,

    /// <summary>
    /// The order is expired.
    /// </summary>
    EXPIRED ,

    /// <summary>
    /// The order has failed.
    /// </summary>
    FAILED,

    /// <summary>
    /// The order has an unknown status.
    /// </summary>
    UNKNOWN_ORDER_STATUS,

    /// <summary>
    /// The order is queued.
    /// </summary>
    QUEUED,

    /// <summary>
    /// The order is cancelled.
    /// </summary>
    CANCEL_QUEUED,
}

/// <summary>
/// Represents the type of an order.
/// </summary>
public enum OrderType
{
    /// <summary>
    /// A market order.
    /// </summary>
    [Description("MARKET")]
    Market,

    /// <summary>
    /// A limit order.
    /// </summary>
    [Description("LIMIT")]
    Limit,

    /// <summary>
    /// A stop order.
    /// </summary>
    [Description("STOP")]
    Stop,

    /// <summary>
    /// A stop limit order.
    /// </summary>
    [Description("STOP_LIMIT")]
    StopLimit,
}

/// <summary>
/// Represents the side of an order (buy or sell).
/// </summary>
public enum OrderSide
{
    /// <summary>
    /// A buy order.
    /// </summary>
    [Description("BUY")]
    Buy,

    /// <summary>
    /// A sell order.
    /// </summary>
    [Description("SELL")]
    Sell
}
