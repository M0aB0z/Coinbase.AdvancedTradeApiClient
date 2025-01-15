using System.Collections.Generic;

namespace Coinbase.AdvancedTradeApiClient.Models.WebSocket.Internal;

/// <summary>
/// Represents a message containing websocket Ticker data.
/// </summary>
public class MarketTradeMessage : WebSocketMessage<MarketTradeEvent>
{
}

/// <summary>
/// Represents a specific event within the Ticker message.
/// </summary>
public class MarketTradeEvent : WebSocketEvent
{
    /// <summary>
    /// Gets or sets the list of Tickers in the event.
    /// </summary>
    public List<Trade> Trades { get; set; }
}

