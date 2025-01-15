using System.Collections.Generic;

namespace Coinbase.AdvancedTradeApiClient.Models.WebSocket.Internal;

/// <summary>
/// Represents a message containing websocket Products status updates data.
/// </summary>
public class ProductStatusMessage : WebSocketMessage<ProductStatusEvent>
{
}

/// <summary>
/// Represents a specific event within the Products status message.
/// </summary>
public class ProductStatusEvent : WebSocketEvent
{
    /// <summary>
    /// Gets or sets the list of products associated with the status event.
    /// </summary>
    public List<ProductStatus> Products { get; set; }
}
