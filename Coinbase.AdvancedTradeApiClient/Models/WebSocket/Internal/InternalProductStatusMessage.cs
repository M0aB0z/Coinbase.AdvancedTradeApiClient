using Coinbase.AdvancedTradeApiClient.Models.Internal;
using Coinbase.AdvancedTradeApiClient.Utilities;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace Coinbase.AdvancedTradeApiClient.Models.WebSocket.Internal;

/// <summary>
/// Represents a message containing websocket MarketTrade data.
/// </summary>
internal class InternalProductStatusMessage : WebSocketMessage<InternalProductStatusEvent>, IModelMapper<ProductStatusMessage>
{
    public ProductStatusMessage ToModel()
    {
        return new ProductStatusMessage
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
internal class InternalProductStatusEvent : WebSocketEvent, IModelMapper<ProductStatusEvent>
{
    /// <summary>
    /// Gets or sets the list of MarketTrades in the event.
    /// </summary>
    [JsonPropertyName("products")]
    public List<InternalProductStatus> Products { get; set; }

    public ProductStatusEvent ToModel()
    {
        return new ProductStatusEvent
        {
            Type = Type,
            Products = Products?.Select(c => c.ToModel()).ToList()
        };
    }
}

