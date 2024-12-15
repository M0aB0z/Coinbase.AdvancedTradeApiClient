using System;
using System.Collections.Generic;

namespace Coinbase.AdvancedTradeApiClient.Models;

/// <summary>
/// Provides details about the trading session status in Coinbase's Futures Commission Merchant (FCM) environment.
/// </summary>
public class FcmTradingSessionDetails
{
    /// <summary>
    /// Gets a value indicating whether the FCM trading session is currently open.
    /// </summary>
    public bool IsSessionOpen { get; internal set; }

    /// <summary>
    /// Gets the timestamp of when the FCM trading session opens.
    /// </summary>]
    public string OpenTime { get; internal set; }

    /// <summary>
    /// Gets the timestamp of when the FCM trading session closes.
    /// </summary>
    public string CloseTime { get; internal set; }
}

/// <summary>
/// Represents product-specific details within the Coinbase trading environment.
/// </summary>
public class Product
{
    /// <summary>
    /// Gets the unique identifier for the product.
    /// </summary>
    public string ProductId { get; internal set; }

    /// <summary>
    /// Gets the current price of the product.
    /// </summary>
    public decimal Price { get; internal set; }

    /// <summary>
    /// Gets the percentage change in price over the last 24 hours.
    /// </summary>
    public decimal? PricePercentageChange24h { get; internal set; }

    /// <summary>
    /// Gets the trading volume of the product over the last 24 hours.
    /// </summary>"
    public decimal? Volume24h { get; internal set; }

    /// <summary>
    /// Gets the percentage change in volume over the last 24 hours.
    /// </summary>
    public decimal? VolumePercentageChange24h { get; internal set; }

    /// <summary>
    /// Gets the smallest allowable increment in the base currency for this product.
    /// </summary>
    public decimal BaseIncrement { get; internal set; }

    /// <summary>
    /// Gets the smallest allowable increment in the quote currency for this product.
    /// </summary>
    public decimal QuoteIncrement { get; internal set; }

    /// <summary>
    /// Gets the minimum order size in the quote currency for this product.
    /// </summary>
    public decimal QuoteMinSize { get; internal set; }

    /// <summary>
    /// Gets the maximum order size in the quote currency for this product.
    /// </summary>
    public decimal QuoteMaxSize { get; internal set; }

    /// <summary>
    /// Gets the minimum order size in the base currency for this product.
    /// </summary>
    public decimal BaseMinSize { get; internal set; }

    /// <summary>
    /// Gets the maximum order size in the base currency for this product.
    /// </summary>
    public decimal BaseMaxSize { get; internal set; }

    /// <summary>
    /// Gets the human-readable name of the base currency.
    /// </summary>
    public string BaseName { get; internal set; }

    /// <summary>
    /// Gets the human-readable name of the quote currency.
    /// </summary>
    public string QuoteName { get; internal set; }

    /// <summary>
    /// Gets the current status of the product (e.g., active, delisted).
    /// </summary>
    public string Status { get; internal set; }

    /// <summary>
    /// Gets the type of the product (e.g., spot, futures).
    /// </summary>
    public string ProductType { get; internal set; }

    /// <summary>
    /// Gets the unique identifier for the quote currency.
    /// </summary>
    public string QuoteCurrencyId { get; internal set; }

    /// <summary>
    /// Gets the unique identifier for the base currency.
    /// </summary>
    public string BaseCurrencyId { get; internal set; }

    /// <summary>
    /// Gets the details related to the FCM trading session for this product.
    /// </summary>ails")]
    public FcmTradingSessionDetails FcmTradingSessionDetails { get; internal set; }

    /// <summary>
    /// Gets the midpoint price between the best bid and best ask.
    /// </summary>
    public decimal? MidMarketPrice { get; internal set; }

    /// <summary>
    /// Gets an alternate name for the product.
    /// </summary>
    public string Alias { get; internal set; }

    /// <summary>
    /// Gets a list of all aliases for this product.
    /// </summary>
    public List<string> AliasTo { get; internal set; }

    /// <summary>
    /// Gets the display symbol for the base currency.
    /// </summary>
    public string BaseDisplaySymbol { get; internal set; }

    /// <summary>
    /// Gets the display symbol for the quote currency.
    /// </summary>
    public string QuoteDisplaySymbol { get; internal set; }

    /// <summary>
    /// Gets the allowable price increment for placing orders.
    /// </summary>
    public decimal PriceIncrement { get; internal set; }


    ///<inheritDoc/>
    public override string ToString()
    => $"{BaseName} {ProductId} @{Math.Round(Price, 4)} {ProductType}";
}
