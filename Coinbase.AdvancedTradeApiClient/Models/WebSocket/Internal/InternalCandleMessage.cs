using Coinbase.AdvancedTradeApiClient.Models.Internal;
using Coinbase.AdvancedTradeApiClient.Utilities;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace Coinbase.AdvancedTradeApiClient.Models.WebSocket.Internal;

/// <summary>
/// Represents a message containing websocket candle data.
/// </summary>
internal class InternalCandleMessage : WebSocketMessage<InternalCandleEvent>, IModelMapper<CandleMessage>
{
    public CandleMessage ToModel()
    {
        return new CandleMessage
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
/// Represents a specific event within the candle message.
/// </summary>
internal class InternalCandleEvent : WebSocketEvent, IModelMapper<CandleEvent>
{
    /// <summary>
    /// Gets or sets the list of candles in the event.
    /// </summary>
    [JsonPropertyName("candles")]
    public List<InternalCandle> Candles { get; set; }

    public CandleEvent ToModel()
    {
        return new CandleEvent
        {
            Type = Type,
            Candles = Candles?.Select(c => c.ToModel()).ToList()
        };
    }
}

