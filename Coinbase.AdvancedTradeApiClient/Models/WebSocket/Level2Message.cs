using System.Collections.Generic;

namespace Coinbase.AdvancedTradeApiClient.Models.WebSocket.Internal;

/// <summary>
/// Represents a message containing websocket Products status updates data.
/// </summary>
public class Level2Message : WebSocketMessage<Level2Event>
{
}

/// <summary>
/// Represents a specific event within the Products status message.
/// </summary>
public class Level2Event : WebSocketEvent
{
    /// <summary>
    /// Gets or sets the list of products associated with the status event.
    /// </summary>
    public List<Level2Update> Updates { get; set; }
}
