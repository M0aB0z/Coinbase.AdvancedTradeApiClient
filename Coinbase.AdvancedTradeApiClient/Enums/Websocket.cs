using System.ComponentModel;

namespace Coinbase.AdvancedTradeApiClient.Enums;

/// <summary>
/// Represents different types of channels for data streams.
/// </summary>
/// 

/*
             ChannelType.Candles => "candles",
            ChannelType.Matches => "match",
            ChannelType.Heartbeats => "heartbeats",
            ChannelType.MarketTrades => "market_trades",
            ChannelType.Status => "status",
            ChannelType.Ticker => "ticker",
            ChannelType.TickerBatch => "ticker_batch",
            ChannelType.Level2 => "level2",
            ChannelType.User => "user",
 */

public enum ChannelType
{
    /// <summary>
    /// Channel for candle data.
    /// </summary>
    [Description("candles")]
    Candles,

    /// <summary>
    /// Channel for heartbeat signals.
    /// </summary>
    [Description("heartbeats")]
    Heartbeats,

    /// <summary>
    /// Channel for market trades.
    /// </summary>
    [Description("market_trades")]
    MarketTrades,

    /// <summary>
    /// Channel for status updates.
    /// </summary>
    [Description("status")]
    Status,

    /// <summary>
    /// Channel for ticker information.
    /// </summary>
    [Description("ticker")]
    Ticker,

    /// <summary>
    /// Channel for batch ticker information.
    /// </summary>
    [Description("ticker_batch")]
    TickerBatch,

    /// <summary>
    /// Channel for level 2 order book data.
    /// </summary>
    [Description("level2")]
    Level2,

    /// <summary>
    /// Channel for user-specific data.
    /// </summary>
    [Description("user")]
    User,

    /// <summary>
    /// Channel for matches data.
    /// </summary>
    ///
    [Description("match")]
    Matches,

}

/// <summary>
/// Represents the state of a WebSocket connection.
/// </summary>
public enum WebSocketState
{
    /// <summary>
    /// No state set.
    /// </summary>
    None,

    /// <summary>
    /// The WebSocket connection is in the process of connecting.
    /// </summary>
    Connecting,

    /// <summary>
    /// The WebSocket connection is open and ready for communication.
    /// </summary>
    Open,

    /// <summary>
    /// A close frame has been sent to the WebSocket server.
    /// </summary>
    CloseSent,

    /// <summary>
    /// A close frame has been received from the WebSocket server.
    /// </summary>
    CloseReceived,

    /// <summary>
    /// The WebSocket connection is closed.
    /// </summary>
    Closed,

    /// <summary>
    /// The WebSocket connection was aborted due to an error.
    /// </summary>
    Aborted
}
