using System.ComponentModel;

namespace Coinbase.AdvancedTradeApiClient.Enums;

/// <summary>
/// Represents the status of an order.
/// </summary>
public enum OrderStatus
{
    /// <summary>
    /// The order is open.
    /// </summary>
    OPEN,

    /// <summary>
    /// The order is cancelled.
    /// </summary>
    CANCELLED,

    /// <summary>
    /// The order is expired.
    /// </summary>
    EXPIRED
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
