using Coinbase.AdvancedTradeApiClient.Models.Internal;
using System.Collections.Generic;

namespace Coinbase.AdvancedTradeApiClient.Models.WebSocket.Internal;

/// <summary>
/// Represents a message containing websocket Ticker data.
/// </summary>
public class TickerMessage : WebSocketMessage<TickerEvent>
{
}

/// <summary>
/// Represents a specific event within the Ticker message.
/// </summary>
public class TickerEvent : WebSocketEvent
{
    /// <summary>
    /// Gets or sets the list of Tickers in the event.
    /// </summary>
    public List<Ticker> Tickers { get; set; }
}

