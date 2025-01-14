using Coinbase.AdvancedTradeApiClient.Utilities;
using Coinbase.AdvancedTradeApiClient.Utilities.Extensions;
using System.Text.Json.Serialization;

namespace Coinbase.AdvancedTradeApiClient.Models.Internal;


/// <summary>
/// Represents details about a specific ticker.
/// </summary>
public class InternalTicker : IModelMapper<Ticker>
{
    /// <summary>
    /// Gets or sets the type of the ticker.
    /// </summary>
    [JsonPropertyName("type")]
    public string Type { get; set; }

    /// <summary>
    /// Gets or sets the ID of the product associated with the ticker.
    /// </summary>
    [JsonPropertyName("product_id")]
    public string ProductId { get; set; }

    /// <summary>
    /// Gets or sets the price of the ticker.
    /// </summary>
    [JsonPropertyName("price")]
    public string Price { get; set; }

    /// <summary>
    /// Gets or sets the volume of the product over the last 24 hours.
    /// </summary>
    [JsonPropertyName("volume_24_h")]
    public string Volume24H { get; set; }

    /// <summary>
    /// Gets or sets the lowest price of the product over the last 24 hours.
    /// </summary>
    [JsonPropertyName("low_24_h")]
    public string Low24H { get; set; }

    /// <summary>
    /// Gets or sets the highest price of the product over the last 24 hours.
    /// </summary>
    [JsonPropertyName("high_24_h")]
    public string High24H { get; set; }

    /// <summary>
    /// Gets or sets the lowest price of the product over the last 52 weeks.
    /// </summary>
    [JsonPropertyName("low_52_w")]
    public string Low52W { get; set; }

    /// <summary>
    /// Gets or sets the highest price of the product over the last 52 weeks.
    /// </summary>
    [JsonPropertyName("high_52_w")]
    public string High52W { get; set; }

    /// <summary>
    /// Gets or sets the percentage change in price of the product over the last 24 hours.
    /// </summary>
    [JsonPropertyName("price_percent_chg_24_h")]
    public string PricePercentChg24H { get; set; }

    public Ticker ToModel()
    {
        return new Ticker
        {
            ProductId = ProductId,
            Price = Price.ToDecimal(),
            Volume24H = Volume24H.ToDecimal(),
            Low24H = Low24H.ToDecimal(),
            High24H = High24H.ToDecimal(),
            Low52W = Low52W.ToDecimal(),
            High52W = High52W.ToDecimal(),
            PricePercentChg24H = PricePercentChg24H.ToDecimal(),
        };
    }
}
