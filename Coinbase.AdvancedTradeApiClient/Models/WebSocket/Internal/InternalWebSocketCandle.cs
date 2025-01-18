using Coinbase.AdvancedTradeApiClient.Models.Internal;
using Coinbase.AdvancedTradeApiClient.Utilities;
using System.Text.Json.Serialization;

namespace Coinbase.AdvancedTradeApiClient.Models.WebSocket;

internal class InternalWebSocketCandle : InternalCandle, IModelMapper<WebSocketCandle>
{
    /// <summary>
    /// Gets or sets the product identifier.
    /// </summary>
    [JsonPropertyName("product_id")]
    public string ProductId { get; set; }

    public new WebSocketCandle ToModel()
    {
        var baseCandle = base.ToModel();

        return new WebSocketCandle
        {
            ProductId = ProductId,
            Close = baseCandle.Close,
            High = baseCandle.High,
            Low = baseCandle.Low,
            Open = baseCandle.Open,
            StartUnix = baseCandle.StartUnix,
            Volume = baseCandle.Volume,
        };
    }
}
