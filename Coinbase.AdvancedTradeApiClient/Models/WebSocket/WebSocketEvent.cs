using System.Text.Json.Serialization;

namespace Coinbase.AdvancedTradeApiClient.Models.WebSocket;

/// <summary>
/// Represents a generic event from the Coinbase WebSocket API.
/// </summary>
public abstract class WebSocketEvent
{
    /// <summary>
    /// Socket event type
    /// </summary>
    [JsonPropertyName("type")]
    public string Type { get; set; }
}
