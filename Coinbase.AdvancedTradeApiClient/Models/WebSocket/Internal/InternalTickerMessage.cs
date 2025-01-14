using Coinbase.AdvancedTradeApiClient.Models.Internal;
using Coinbase.AdvancedTradeApiClient.Utilities;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace Coinbase.AdvancedTradeApiClient.Models.WebSocket.Internal;

/// <summary>
/// Represents a message containing websocket Ticker data.
/// </summary>
internal class InternalTickerMessage : WebSocketMessage<InternalTickerEvent>, IModelMapper<TickerMessage>
{
    public TickerMessage ToModel()
    {
        return new TickerMessage
        {
            Channel = Channel,
            ClientId = ClientId,
            Timestamp = Timestamp,
            SequenceNumber = SequenceNumber,
            Events = Events?.Select(e => e.ToModel()).ToList()
        };
    }
}

/// <summary>
/// Represents a specific event within the Ticker message.
/// </summary>
internal class InternalTickerEvent : WebSocketEvent, IModelMapper<TickerEvent>
{
    /// <summary>
    /// Gets or sets the list of Tickers in the event.
    /// </summary>
    [JsonPropertyName("tickers")]
    public List<InternalTicker> Tickers { get; set; }

    public TickerEvent ToModel()
    {
        return new TickerEvent
        {
            Type = Type,
            Tickers = Tickers?.Select(c => c.ToModel()).ToList()
        };
    }
}

