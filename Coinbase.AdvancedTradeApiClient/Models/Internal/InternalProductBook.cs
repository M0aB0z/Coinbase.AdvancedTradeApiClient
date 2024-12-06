using Coinbase.AdvancedTradeApiClient.Utilities;
using Coinbase.AdvancedTradeApiClient.Utilities.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace Coinbase.AdvancedTradeApiClient.Models.Internal;

/// <summary>
/// Represents the order book for a specific product on Coinbase.
/// </summary>
internal class InternalProductBook : IModelMapper<ProductBook>
{
    /// <summary>
    /// Gets or sets the product identifier associated with the order book.
    /// </summary>
    [JsonPropertyName("product_id")]
    public string ProductId { get; set; }

    /// <summary>
    /// Gets or sets the list of buy orders for the product.
    /// </summary>
    [JsonPropertyName("bids")]
    public List<InternalOffer> Bids { get; set; }

    /// <summary>
    /// Gets or sets the list of sell orders for the product.
    /// </summary>
    [JsonPropertyName("asks")]
    public List<InternalOffer> Asks { get; set; }

    /// <summary>
    /// Gets or sets the timestamp when the order book was captured.
    /// </summary>
    [JsonPropertyName("time")]
    public DateTime Time { get; set; }

    /// <summary>
    /// Converts to public model
    /// </summary>
    /// <returns></returns>
    public ProductBook ToModel()
    {
        return new ProductBook
        {
            ProductId = ProductId,
            Bids = Bids?.Select(x => x.ToModel())?.ToList(),
            Asks = Asks?.Select(x => x.ToModel())?.ToList(),
            Time = Time
        };
    }
}

/// <summary>
/// Represents an individual offer (bid or ask) in the order book.
/// </summary>
internal class InternalOffer : IModelMapper<Offer>
{
    /// <summary>
    /// Gets or sets the price at which the offer is made.
    /// </summary>
    [JsonPropertyName("price")]
    public string Price { get; set; }

    /// <summary>
    /// Gets or sets the size or quantity of the offer.
    /// </summary>
    [JsonPropertyName("size")]
    public string Size { get; set; }

    /// <summary>
    /// Converts to public model
    /// </summary>
    /// <returns></returns>
    public Offer ToModel()
    {
        return new Offer
        {
            Price = Price?.ToNullableDouble(),
            Size = Size?.ToNullableDouble()
        };
    }
}
