using Coinbase.AdvancedTradeApiClient.Utilities;
using Coinbase.AdvancedTradeApiClient.Utilities.Extensions;
using System.Text.Json.Serialization;

namespace Coinbase.AdvancedTradeApiClient.Models.Internal;

/// <summary>
/// Represents a candlestick data point for a specific time frame in a trading chart.
/// </summary>
internal class InternalCandle : IModelMapper<Candle>

{
    /// <summary>
    /// Gets or sets the start time of the candlestick in UNIX timestamp format.
    /// </summary>
    [JsonPropertyName("start")]
    public string StartUnix { get; set; }

    /// <summary>
    /// Gets or sets the lowest traded price of the asset during the time interval represented by the candlestick.
    /// </summary>
    [JsonPropertyName("low")]
    public string Low { get; set; }

    /// <summary>
    /// Gets or sets the highest traded price of the asset during the time interval represented by the candlestick.
    /// </summary>
    [JsonPropertyName("high")]
    public string High { get; set; }

    /// <summary>
    /// Gets or sets the opening price of the asset at the beginning of the time interval represented by the candlestick.
    /// </summary>
    [JsonPropertyName("open")]
    public string Open { get; set; }

    /// <summary>
    /// Gets or sets the closing price of the asset at the end of the time interval represented by the candlestick.
    /// </summary>
    [JsonPropertyName("close")]
    public string Close { get; set; }

    /// <summary>
    /// Gets or sets the trading volume of the asset during the time interval represented by the candlestick.
    /// </summary>
    [JsonPropertyName("volume")]
    public string Volume { get; set; }

    /// <summary>
    /// Maps the internal model to the public model.
    /// </summary>
    /// <returns></returns>
    public virtual Candle ToModel()
    {
        return new Candle
        {
            StartUnix = StartUnix,
            Low = Low.ToDecimal(),
            High = High.ToDecimal(),
            Open = Open.ToDecimal(),
            Close = Close.ToDecimal(),
            Volume = Volume.ToDecimal(),
        };
    }
}
