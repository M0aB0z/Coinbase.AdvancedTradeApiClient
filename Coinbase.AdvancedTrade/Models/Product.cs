using System.Collections.Generic;

namespace Coinbase.AdvancedTrade.Models;

/// <summary>
/// Provides details about the trading session status in Coinbase's Futures Commission Merchant (FCM) environment.
/// </summary>
public class FcmTradingSessionDetails
{
    /// <summary>
    /// Gets or sets a value indicating whether the FCM trading session is currently open.
    /// </summary>
    public bool IsSessionOpen { get; set; }

    /// <summary>
    /// Gets or sets the timestamp of when the FCM trading session opens.
    /// </summary>]
    public string OpenTime { get; set; }

    /// <summary>
    /// Gets or sets the timestamp of when the FCM trading session closes.
    /// </summary>
    public string CloseTime { get; set; }
}

/// <summary>
/// Represents product-specific details within the Coinbase trading environment.
/// </summary>
public class Product
{
    /// <summary>
    /// Gets or sets the unique identifier for the product.
    /// </summary>
    public string ProductId { get; set; }

    /// <summary>
    /// Gets or sets the current price of the product.
    /// </summary>
    public double? Price { get; set; }

    /// <summary>
    /// Gets or sets the percentage change in price over the last 24 hours.
    /// </summary>
    public double? PricePercentageChange24h { get; set; }

    /// <summary>
    /// Gets or sets the trading volume of the product over the last 24 hours.
    /// </summary>"
    public double? Volume24h { get; set; }

    /// <summary>
    /// Gets or sets the percentage change in volume over the last 24 hours.
    /// </summary>
    public double? VolumePercentageChange24h { get; set; }

    /// <summary>
    /// Gets or sets the smallest allowable increment in the base currency for this product.
    /// </summary>
    public double? BaseIncrement { get; set; }

    /// <summary>
    /// Gets or sets the smallest allowable increment in the quote currency for this product.
    /// </summary>
    public double? QuoteIncrement { get; set; }

    /// <summary>
    /// Gets or sets the minimum order size in the quote currency for this product.
    /// </summary>
    public double? QuoteMinSize { get; set; }

    /// <summary>
    /// Gets or sets the maximum order size in the quote currency for this product.
    /// </summary>
    public double? QuoteMaxSize { get; set; }

    /// <summary>
    /// Gets or sets the minimum order size in the base currency for this product.
    /// </summary>
    public double? BaseMinSize { get; set; }

    /// <summary>
    /// Gets or sets the maximum order size in the base currency for this product.
    /// </summary>
    public double? BaseMaxSize { get; set; }

    /// <summary>
    /// Gets or sets the human-readable name of the base currency.
    /// </summary>
    public string BaseName { get; set; }

    /// <summary>
    /// Gets or sets the human-readable name of the quote currency.
    /// </summary>
    public string QuoteName { get; set; }

    /// <summary>
    /// Gets or sets the current status of the product (e.g., active, delisted).
    /// </summary>
    public string Status { get; set; }

    /// <summary>
    /// Gets or sets the type of the product (e.g., spot, futures).
    /// </summary>
    public string ProductType { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier for the quote currency.
    /// </summary>
    public string QuoteCurrencyId { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier for the base currency.
    /// </summary>
    public string BaseCurrencyId { get; set; }

    /// <summary>
    /// Gets or sets the details related to the FCM trading session for this product.
    /// </summary>ails")]
    public FcmTradingSessionDetails FcmTradingSessionDetails { get; set; }

    /// <summary>
    /// Gets or sets the midpoint price between the best bid and best ask.
    /// </summary>
    public double? MidMarketPrice { get; set; }

    /// <summary>
    /// Gets or sets an alternate name for the product.
    /// </summary>
    public string Alias { get; set; }

    /// <summary>
    /// Gets or sets a list of all aliases for this product.
    /// </summary>
    public List<string> AliasTo { get; set; }

    /// <summary>
    /// Gets or sets the display symbol for the base currency.
    /// </summary>
    public string BaseDisplaySymbol { get; set; }

    /// <summary>
    /// Gets or sets the display symbol for the quote currency.
    /// </summary>
    public string QuoteDisplaySymbol { get; set; }

    /// <summary>
    /// Gets or sets the allowable price increment for placing orders.
    /// </summary>
    public double? PriceIncrement { get; set; }
}
