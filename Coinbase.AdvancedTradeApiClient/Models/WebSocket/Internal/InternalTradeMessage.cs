using Coinbase.AdvancedTradeApiClient.Models.Internal;
using Coinbase.AdvancedTradeApiClient.Utilities;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace Coinbase.AdvancedTradeApiClient.Models.WebSocket.Internal;

/// <summary>
/// Represents a message containing websocket MarketTrade data.
/// </summary>
internal class InternalMarketTradeMessage : WebSocketMessage<InternalMarketTradeEvent>, IModelMapper<MarketTradeMessage>
{
    public MarketTradeMessage ToModel()
    {
        return new MarketTradeMessage
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
/// Represents a specific event within the MarketTrade message.
/// </summary>
internal class InternalMarketTradeEvent : WebSocketEvent, IModelMapper<MarketTradeEvent>
{
    /// <summary>
    /// Gets or sets the list of MarketTrades in the event.
    /// </summary>
    [JsonPropertyName("trades")]
    public List<InternalTrade> Trades { get; set; }

    public MarketTradeEvent ToModel()
    {
        return new MarketTradeEvent
        {
            Type = Type,
            Trades = Trades?.Select(c => c.ToModel()).ToList()
        };
    }
}

