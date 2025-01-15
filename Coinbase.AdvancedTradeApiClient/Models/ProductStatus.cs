namespace Coinbase.AdvancedTradeApiClient.Models;

/// <summary>
/// Product Status update event details.
/// </summary>
public class ProductStatus
{
    /// <summary>
    /// Gets or sets the type of the product.
    /// </summary>
    public string ProductType { get; set; }

    /// <summary>
    /// Gets or sets the ID of the product.
    /// </summary>
    public string Id { get; set; }

    /// <summary>
    /// Gets or sets the base currency of the product.
    /// </summary>
    public string BaseCurrency { get; set; }

    /// <summary>
    /// Gets or sets the quote currency of the product.
    /// </summary>
    public string QuoteCurrency { get; set; }

    /// <summary>
    /// Gets or sets the base increment of the product.
    /// </summary>
    public decimal BaseIncrement { get; set; }

    /// <summary>
    /// Gets or sets the quote increment of the product.
    /// </summary>
    public decimal QuoteIncrement { get; set; }

    /// <summary>
    /// Gets or sets the display name of the product.
    /// </summary>
    public string DisplayName { get; set; }

    /// <summary>
    /// Gets or sets the status of the product.
    /// </summary>
    public string Status { get; set; }

    /// <summary>
    /// Gets or sets any status message associated with the product.
    /// </summary>
    public string StatusMessage { get; set; }

    /// <summary>
    /// Gets or sets the minimum market funds for the product.
    /// </summary>
    public decimal MinMarketFunds { get; set; }
}
