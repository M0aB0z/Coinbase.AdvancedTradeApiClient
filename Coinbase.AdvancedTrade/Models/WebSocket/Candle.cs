using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;

namespace Coinbase.AdvancedTrade.Models.WebSocket
{
    /// <summary>
    /// Represents a message containing websocket candle data.
    /// </summary>
    public class CandleMessage
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
        public List<Event> Events { get; set; }

        /// <summary>
        /// Represents a specific event within the candle message.
        /// </summary>
        public class Event
        {
            /// <summary>
            /// Gets or sets the type of the event.
            /// </summary>
            [JsonPropertyName("type")]
            public string Type { get; set; }

            /// <summary>
            /// Gets or sets the list of candles in the event.
            /// </summary>
            [JsonPropertyName("candles")]
            public List<Candle> Candles { get; set; }
        }
    }

    /// <summary>
    /// Represents a single candle's data.
    /// </summary>
    public class Candle
    {
        /// <summary>
        /// Gets or sets the start time of the candle in Unix timestamp format.
        /// </summary>
        [JsonPropertyName("start")]
        public string StartUnix { get; set; }

        /// <summary>
        /// Gets or sets the highest price during the candle period.
        /// </summary>
        [JsonPropertyName("high")]
        public string High { get; set; }

        /// <summary>
        /// Gets or sets the lowest price during the candle period.
        /// </summary>
        [JsonPropertyName("low")]
        public string Low { get; set; }

        /// <summary>
        /// Gets or sets the opening price of the candle.
        /// </summary>
        [JsonPropertyName("open")]
        public string Open { get; set; }

        /// <summary>
        /// Gets or sets the closing price of the candle.
        /// </summary>
        [JsonPropertyName("close")]
        public string Close { get; set; }

        /// <summary>
        /// Gets or sets the volume of the asset traded during the candle period.
        /// </summary>
        [JsonPropertyName("volume")]
        public string Volume { get; set; }

        /// <summary>
        /// Gets or sets the product identifier.
        /// </summary>
        [JsonPropertyName("product_id")]
        public string ProductId { get; set; }

        /// <summary>
        /// Gets the start time of the candle as a DateTime object.
        /// </summary>
        [JsonIgnore]
        public DateTime StartDate => !string.IsNullOrEmpty(StartUnix) ? UnixTimeStampToDateTime(StartUnix) : DateTime.MinValue;

        /// <summary>
        /// Converts a Unix timestamp string to a DateTime object.
        /// </summary>
        /// <param name="unixTimeStamp">The Unix timestamp string.</param>
        /// <returns>The converted DateTime object.</returns>
        private static DateTime UnixTimeStampToDateTime(string unixTimeStamp)
        {
            if (long.TryParse(unixTimeStamp, out long parsedUnixTime))
            {
                return DateTimeOffset.FromUnixTimeSeconds(parsedUnixTime).UtcDateTime;
            }
            return DateTime.MinValue;
        }
    }
}
