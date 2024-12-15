using Coinbase.AdvancedTradeApiClient.Enums;
using Coinbase.AdvancedTradeApiClient.Interfaces;
using Coinbase.AdvancedTradeApiClient.Models;
using Coinbase.AdvancedTradeApiClient.Models.Internal;
using Coinbase.AdvancedTradeApiClient.Utilities;
using Coinbase.AdvancedTradeApiClient.Utilities.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;


namespace Coinbase.AdvancedTradeApiClient.ExchangeManagers;

/// <summary>
/// Provides methods to manage products on the Coinbase Advanced Trade API.
/// </summary>
public class ProductsManager : BaseManager, IProductsManager
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ProductsManager"/> class.
    /// </summary>
    /// <param name="authenticator">The authenticator for Coinbase API requests.</param>
    public ProductsManager(CoinbaseAuthenticator authenticator) : base(authenticator) { }

    /// <inheritdoc/>
    public async Task<IReadOnlyList<Product>> ListProductsAsync(string productType = "SPOT", CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrEmpty(productType))
            throw new ArgumentException("Product type cannot be null or empty", nameof(productType));

        // Assuming SendAuthenticatedRequest becomes asynchronous
        if (_authenticator == null)
            throw new InvalidOperationException("Authenticator is not initialized.");

        var response = await _authenticator.GetAsync(UtilityHelper.BuildParamUri("/api/v3/brokerage/products", new { product_type = productType }), cancellationToken);

        return response.As<InternalProduct[]>("products").ToModel();
    }

    /// <inheritdoc/>
    public async Task<Product> GetProductAsync(string productId, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(productId))
            throw new ArgumentException("Product ID cannot be null or empty", nameof(productId));

        var response = await _authenticator.GetAsync($"/api/v3/brokerage/products/{productId}", cancellationToken);
        return response.As<InternalProduct>().ToModel();
    }

    /// <inheritdoc/>
    public async Task<IReadOnlyList<Candle>> GetProductCandlesAsync(string productId, DateTime startTimeUtc, DateTime endTimeUtc, Granularity granularity, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(productId))
            throw new ArgumentException("Product ID cannot be null or empty");

        if (startTimeUtc > endTimeUtc)
            throw new ArgumentException("Start date must be before end date");

        var start = (long)startTimeUtc.Subtract(DateTime.UnixEpoch).TotalSeconds;
        var end = (long)endTimeUtc.Subtract(DateTime.UnixEpoch).TotalSeconds;

        var parameters = new { start, end, granularity };
        var response = await _authenticator.GetAsync(UtilityHelper.BuildParamUri($"/api/v3/brokerage/products/{productId}/candles", parameters), cancellationToken);
        return response.As<InternalCandle[]>("candles").ToModel();
    }

    /// <inheritdoc/>
    public async Task<MarketTrades> GetMarketTradesAsync(string productId, int limit, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(productId) || limit <= 0)
            throw new ArgumentException("Product ID cannot be null or empty, and limit must be greater than 0");

        var parameters = new { limit };

        var response = await _authenticator.GetAsync(UtilityHelper.BuildParamUri($"/api/v3/brokerage/products/{productId}/ticker", parameters), cancellationToken);

        // Extract trades data from response
        var trades = response.As<InternalTrade[]>("trades").ToModel();

        // Extract best bid and ask
        var bestBidStr = response.As<string>("best_bid");
        var bestAskStr = response.As<string>("best_ask");

        return new MarketTrades { Trades = trades, BestBid = bestBidStr.ToDecimal(), BestAsk = bestAskStr.ToDecimal() };
    }

    /// <inheritdoc/>
    public async Task<ProductBook> GetProductBookAsync(string productId, int limit, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(productId))
            throw new ArgumentException("Product ID cannot be null or empty", nameof(productId));

        var parameters = new { product_id = productId, limit };
        var response = await _authenticator.GetAsync(UtilityHelper.BuildParamUri("/api/v3/brokerage/product_book", parameters), cancellationToken);
        return response.As<InternalProductBook>("pricebook").ToModel();
    }

    /// <inheritdoc/>
    public async Task<IReadOnlyList<ProductBook>> GetBestBidAskAsync(List<string> productIds, CancellationToken cancellationToken)
    {
        if (productIds == null || !productIds.Any())
            throw new ArgumentException("Product IDs list cannot be null or empty", nameof(productIds));

        // Construct the URL with multiple product_ids
        string url = "/api/v3/brokerage/best_bid_ask?" + string.Join("&", productIds.Select(pid => $"product_ids={pid}"));
        var response = await _authenticator.GetAsync(url, cancellationToken);

        return response.As<InternalProductBook[]>("pricebooks").ToModel();
    }
}
