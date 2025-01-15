using Coinbase.AdvancedTradeApiClient.Models.Internal;
using Coinbase.AdvancedTradeApiClient.Utilities;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace Coinbase.AdvancedTradeApiClient.Models.WebSocket.Internal;

/// <summary>
/// Represents a message containing websocket MarketTrade data.
/// </summary>
internal class InternalUserOrderMessage : WebSocketMessage<InternalUserOrderEvent>, IModelMapper<UserOrderMessage>
{
    public UserOrderMessage ToModel()
    {
        return new UserOrderMessage
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
/// Represents a specific event within the User's order event message.
/// </summary>
internal class InternalUserOrderEvent : WebSocketEvent, IModelMapper<UserOrderEvent>
{
    /// <summary>
    /// Gets or sets the list of user's orders in the event.
    /// </summary>
    [JsonPropertyName("orders")]
    public List<InternalUserOrder> Orders { get; set; }

    public UserOrderEvent ToModel()
    {
        return new UserOrderEvent
        {
            Type = Type,
            Orders = Orders?.Select(c => c.ToModel()).ToList()
        };
    }
}

