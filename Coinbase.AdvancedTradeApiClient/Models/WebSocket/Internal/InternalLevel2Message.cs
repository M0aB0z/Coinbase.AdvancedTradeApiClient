using Coinbase.AdvancedTradeApiClient.Models.Internal;
using Coinbase.AdvancedTradeApiClient.Utilities;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace Coinbase.AdvancedTradeApiClient.Models.WebSocket.Internal;

/// <summary>
/// Represents a message containing websocket Level2 data.
/// </summary>
internal class InternalLevel2Message : WebSocketMessage<InternalLevel2Event>, IModelMapper<Level2Message>
{
    public Level2Message ToModel()
    {
        return new Level2Message
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
/// Represents a specific event within the Level2 message.
/// </summary>
internal class InternalLevel2Event : WebSocketEvent, IModelMapper<Level2Event>
{
    /// <summary>
    /// Gets or sets the product ID associated with the Level 2 event.
    /// </summary>
    [JsonPropertyName("product_id")]
    public string ProductId { get; set; }

    /// <summary>
    /// Gets or sets the list of Level 2 updates.
    /// </summary>
    [JsonPropertyName("updates")]
    public List<InternalLevel2Update> Updates { get; set; }

    public Level2Event ToModel()
    {
        return new Level2Event
        {
            Type = Type,
            Updates = Updates?.Select(c => c.ToModel()).ToList()
        };
    }
}

