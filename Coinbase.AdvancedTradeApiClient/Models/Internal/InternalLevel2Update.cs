using Coinbase.AdvancedTradeApiClient.Utilities;
using Coinbase.AdvancedTradeApiClient.Utilities.Extensions;
using System;
using System.Text.Json.Serialization;

namespace Coinbase.AdvancedTradeApiClient.Models.Internal;

internal class InternalLevel2Update : IModelMapper<Level2Update>
{
    /// <summary>
    /// Gets or sets the side (e.g. "buy" or "sell") of the Level 2 update.
    /// </summary>
    [JsonPropertyName("side")]
    public string Side { get; set; }

    /// <summary>
    /// Gets or sets the time when the Level 2 update occurred.
    /// </summary>
    [JsonPropertyName("event_time")]
    public DateTime EventTime { get; set; }

    /// <summary>
    /// Gets or sets the price level for the Level 2 update.
    /// </summary>
    [JsonPropertyName("price_level")]
    public string PriceLevel { get; set; }

    /// <summary>
    /// Gets or sets the new quantity for the Level 2 update.
    /// </summary>
    [JsonPropertyName("new_quantity")]
    public string NewQuantity { get; set; }

    public Level2Update ToModel()
    {
        return new Level2Update
        {
            Side = Side,
            EventTime = EventTime,
            PriceLevel = PriceLevel.ToDecimal(),
            NewQuantity = NewQuantity.ToDecimal()
        };
    }
}
