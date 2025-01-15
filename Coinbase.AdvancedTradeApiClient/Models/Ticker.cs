namespace Coinbase.AdvancedTradeApiClient.Models.Internal;


/// <summary>
/// Represents details about a specific ticker.
/// </summary>
public class Ticker
{
    /// <summary>
    /// Gets or sets the type of the ticker.
    /// </summary>
    public string Type { get; set; }

    /// <summary>
    /// Gets or sets the ID of the product associated with the ticker.
    /// </summary>
    public string ProductId { get; set; }

    /// <summary>
    /// Gets or sets the price of the ticker.
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// Gets or sets the volume of the product over the last 24 hours.
    public decimal Volume24H { get; set; }

    /// <summary>
    /// Gets or sets the lowest price of the product over the last 24 hours.
    /// </summary>
    public decimal Low24H { get; set; }

    /// <summary>
    /// Gets or sets the highest price of the product over the last 24 hours.
    /// </summary>
    public decimal High24H { get; set; }

    /// <summary>
    /// Gets or sets the lowest price of the product over the last 52 weeks.
    /// </summary>
    public decimal Low52W { get; set; }

    /// <summary>
    /// Gets or sets the highest price of the product over the last 52 weeks.
    /// </summary>
    public decimal High52W { get; set; }

    /// <summary>
    /// Gets or sets the percentage change in price of the product over the last 24 hours.
    /// </summary>
    public decimal PricePercentChg24H { get; set; }
}
