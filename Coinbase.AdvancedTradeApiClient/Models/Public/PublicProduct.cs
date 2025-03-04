﻿using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Coinbase.AdvancedTradeApiClient.Models.Public;

/// <summary>
/// Represents a public product in the Coinbase trading environment.
/// </summary>
public class PublicProduct
{
    /// <summary>
    /// Gets or sets the unique identifier for the product.
    /// </summary>
    [JsonPropertyName("product_id")]
    public string ProductId { get; set; }

    /// <summary>
    /// Gets or sets the current price of the product.
    /// </summary>
    [JsonPropertyName("price")]
    public string Price { get; set; }

    /// <summary>
    /// Gets or sets the percentage change in price over the last 24 hours.
    /// </summary>
    [JsonPropertyName("price_percentage_change_24h")]
    public string PricePercentageChange24h { get; set; }

    /// <summary>
    /// Gets or sets the trading volume of the product over the last 24 hours.
    /// </summary>
    [JsonPropertyName("volume_24h")]
    public string Volume24h { get; set; }

    /// <summary>
    /// Gets or sets the percentage change in volume over the last 24 hours.
    /// </summary>
    [JsonPropertyName("volume_percentage_change_24h")]
    public string VolumePercentageChange24h { get; set; }

    /// <summary>
    /// Gets or sets the smallest allowable increment in the base currency for this product.
    /// </summary>
    [JsonPropertyName("base_increment")]
    public string BaseIncrement { get; set; }

    /// <summary>
    /// Gets or sets the smallest allowable increment in the quote currency for this product.
    /// </summary>
    [JsonPropertyName("quote_increment")]
    public string QuoteIncrement { get; set; }

    /// <summary>
    /// Gets or sets the minimum order size in the quote currency for this product.
    /// </summary>
    [JsonPropertyName("quote_min_size")]
    public string QuoteMinSize { get; set; }

    /// <summary>
    /// Gets or sets the maximum order size in the quote currency for this product.
    /// </summary>
    [JsonPropertyName("quote_max_size")]
    public string QuoteMaxSize { get; set; }

    /// <summary>
    /// Gets or sets the minimum order size in the base currency for this product.
    /// </summary>
    [JsonPropertyName("base_min_size")]
    public string BaseMinSize { get; set; }

    /// <summary>
    /// Gets or sets the maximum order size in the base currency for this product.
    /// </summary>
    [JsonPropertyName("base_max_size")]
    public string BaseMaxSize { get; set; }

    /// <summary>
    /// Gets or sets the name of the base currency.
    /// </summary>
    [JsonPropertyName("base_name")]
    public string BaseName { get; set; }

    /// <summary>
    /// Gets or sets the name of the quote currency.
    /// </summary>
    [JsonPropertyName("quote_name")]
    public string QuoteName { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the product is watched.
    /// </summary>
    [JsonPropertyName("watched")]
    public bool Watched { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the product is disabled.
    /// </summary>
    [JsonPropertyName("is_disabled")]
    public bool IsDisabled { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the product is new.
    /// </summary>
    [JsonPropertyName("new")]
    public bool New { get; set; }

    /// <summary>
    /// Gets or sets the current status of the product.
    /// </summary>
    [JsonPropertyName("status")]
    public string Status { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the product is cancel-only.
    /// </summary>
    [JsonPropertyName("cancel_only")]
    public bool CancelOnly { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the product is limit-only.
    /// </summary>
    [JsonPropertyName("limit_only")]
    public bool LimitOnly { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the product is post-only.
    /// </summary>
    [JsonPropertyName("post_only")]
    public bool PostOnly { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether trading is disabled for the product.
    /// </summary>
    [JsonPropertyName("trading_disabled")]
    public bool TradingDisabled { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the product is in auction mode.
    /// </summary>
    [JsonPropertyName("auction_mode")]
    public bool AuctionMode { get; set; }

    /// <summary>
    /// Gets or sets the type of the product (e.g., spot, futures).
    /// </summary>
    [JsonPropertyName("product_type")]
    public string ProductType { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier for the quote currency.
    /// </summary>
    [JsonPropertyName("quote_currency_id")]
    public string QuoteCurrencyId { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier for the base currency.
    /// </summary>
    [JsonPropertyName("base_currency_id")]
    public string BaseCurrencyId { get; set; }

    /// <summary>
    /// Gets or sets the details related to the FCM trading session for this product.
    /// </summary>
    [JsonPropertyName("fcm_trading_session_details")]
    public object FcmTradingSessionDetails { get; set; }

    /// <summary>
    /// Gets or sets the midpoint price between the best bid and best ask.
    /// </summary>
    [JsonPropertyName("mid_market_price")]
    public string MidMarketPrice { get; set; }

    /// <summary>
    /// Gets or sets an alternate name for the product.
    /// </summary>
    [JsonPropertyName("alias")]
    public string Alias { get; set; }

    /// <summary>
    /// Gets or sets a list of all aliases for this product.
    /// </summary>
    [JsonPropertyName("alias_to")]
    public List<string> AliasTo { get; set; }

    /// <summary>
    /// Gets or sets the display symbol for the base currency.
    /// </summary>
    [JsonPropertyName("base_display_symbol")]
    public string BaseDisplaySymbol { get; set; }

    /// <summary>
    /// Gets or sets the display symbol for the quote currency.
    /// </summary>
    [JsonPropertyName("quote_display_symbol")]
    public string QuoteDisplaySymbol { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the product is view-only.
    /// </summary>
    [JsonPropertyName("view_only")]
    public bool ViewOnly { get; set; }

    /// <summary>
    /// Gets or sets the allowable price increment for placing orders.
    /// </summary>
    [JsonPropertyName("price_increment")]
    public string PriceIncrement { get; set; }

    /// <summary>
    /// Gets or sets the display name of the product.
    /// </summary>
    [JsonPropertyName("display_name")]
    public string DisplayName { get; set; }

    /// <summary>
    /// Gets or sets the venue where the product is traded.
    /// </summary>
    [JsonPropertyName("product_venue")]
    public string ProductVenue { get; set; }

    /// <summary>
    /// Gets or sets the approximate trading volume in the quote currency over the last 24 hours.
    /// </summary>
    [JsonPropertyName("approximate_quote_24h_volume")]
    public string ApproximateQuote24hVolume { get; set; }
}
