using Coinbase.AdvancedTradeApiClient.Utilities;
using Coinbase.AdvancedTradeApiClient.Utilities.Extensions;
using System.Text.Json.Serialization;

namespace Coinbase.AdvancedTradeApiClient.Models.Internal;



/// <summary>
/// Represents an individual status event within a <see cref="StatusMessage"/>.
/// </summary>

/// <summary>
/// Represents product details in the status event.
/// </summary>
public class InternalProductStatus : IModelMapper<ProductStatus>
{
    /// <summary>
    /// Gets or sets the type of the product.
    /// </summary>
    [JsonPropertyName("product_type")]
    public string ProductType { get; set; }

    /// <summary>
    /// Gets or sets the ID of the product.
    /// </summary>
    [JsonPropertyName("id")]
    public string Id { get; set; }

    /// <summary>
    /// Gets or sets the base currency of the product.
    /// </summary>
    [JsonPropertyName("base_currency")]
    public string BaseCurrency { get; set; }

    /// <summary>
    /// Gets or sets the quote currency of the product.
    /// </summary>
    [JsonPropertyName("quote_currency")]
    public string QuoteCurrency { get; set; }

    /// <summary>
    /// Gets or sets the base increment of the product.
    /// </summary>
    [JsonPropertyName("base_increment")]
    public string BaseIncrement { get; set; }

    /// <summary>
    /// Gets or sets the quote increment of the product.
    /// </summary>
    [JsonPropertyName("quote_increment")]
    public string QuoteIncrement { get; set; }

    /// <summary>
    /// Gets or sets the display name of the product.
    /// </summary>
    [JsonPropertyName("display_name")]
    public string DisplayName { get; set; }

    /// <summary>
    /// Gets or sets the status of the product.
    /// </summary>
    [JsonPropertyName("status")]
    public string Status { get; set; }

    /// <summary>
    /// Gets or sets any status message associated with the product.
    /// </summary>
    [JsonPropertyName("status_message")]
    public string StatusMessage { get; set; }

    /// <summary>
    /// Gets or sets the minimum market funds for the product.
    /// </summary>
    [JsonPropertyName("min_market_funds")]
    public string MinMarketFunds { get; set; }

    /// <summary>
    /// Maps the internal model to the public model.
    /// </summary>
    /// <returns></returns>
    public ProductStatus ToModel()
    {
        return new ProductStatus
        {
            ProductType = ProductType,
            Id = Id,
            BaseCurrency = BaseCurrency,
            QuoteCurrency = QuoteCurrency,
            BaseIncrement = BaseIncrement.ToDecimal(),
            QuoteIncrement = QuoteIncrement.ToDecimal(),
            DisplayName = DisplayName,
            Status = Status,
            StatusMessage = StatusMessage,
            MinMarketFunds = MinMarketFunds.ToDecimal()
        };
    }
}
