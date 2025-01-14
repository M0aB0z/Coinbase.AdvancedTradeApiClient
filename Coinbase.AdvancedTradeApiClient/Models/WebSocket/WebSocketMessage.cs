using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Coinbase.AdvancedTradeApiClient.Models.WebSocket
{
    /// <summary>
    /// Represents a message containing websocket data.
    /// </summary>
    /// <typeparam name="TEventType"></typeparam>
    public class WebSocketMessage<TEventType> where TEventType : WebSocketEvent
    {
        /// <summary>
        /// Gets or sets the channel for the message.
        /// </summary>
        [JsonPropertyName("channel")]
        public string Channel { get; set; }

        /// <summary>
        /// Gets or sets the client identifier.
        /// </summary>
        [JsonPropertyName("client_id")]
        public string ClientId { get; set; }

        /// <summary>
        /// Gets or sets the timestamp of the message.
        /// </summary>
        [JsonPropertyName("timestamp")]
        public string Timestamp { get; set; }

        /// <summary>
        /// Gets or sets the sequence number of the message.
        /// </summary>
        [JsonPropertyName("sequence_num")]
        public long SequenceNumber { get; set; }

        /// <summary>
        /// Gets or sets the list of events contained in the message.
        /// </summary>
        [JsonPropertyName("events")]
        public List<TEventType> Events { get; set; }
    }
}
