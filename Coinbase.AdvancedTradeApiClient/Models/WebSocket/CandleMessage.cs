using System.Collections.Generic;

namespace Coinbase.AdvancedTradeApiClient.Models.WebSocket.Internal;

/// <summary>
/// Represents a message containing websocket candle data.
/// </summary>
public class CandleMessage : WebSocketMessage<CandleEvent>
{
    /// <summary>
    /// The candle's product id
    /// </summary>
    public string ProductId { get; internal set; }
}

/// <summary>
/// Represents a specific event within the candle message.
/// </summary>
public class CandleEvent : WebSocketEvent
{
    /// <summary>
    /// Gets or sets the list of candles in the event.
    /// </summary>
    public List<WebSocketCandle> Candles { get; set; }
}

