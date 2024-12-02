using Coinbase.AdvancedTrade.Enums;
using Coinbase.AdvancedTrade.Interfaces;
using Coinbase.AdvancedTrade.Models;
using Coinbase.AdvancedTrade.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;


namespace Coinbase.AdvancedTrade.ExchangeManagers;

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
    public async Task<List<Product>> ListProductsAsync(string productType = "SPOT", CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrEmpty(productType))
            throw new ArgumentException("Product type cannot be null or empty", nameof(productType));

        // Assuming SendAuthenticatedRequest becomes asynchronous
        if (_authenticator == null)
            throw new InvalidOperationException("Authenticator is not initialized.");

        var response = await _authenticator.GetAsync(UtilityHelper.BuildParamUri("/api/v3/brokerage/products", new { product_type = productType }), cancellationToken);
        return response.TryGetProperty("products", out JsonElement products) ? products.Deserialize<List<Product>>() : [];
    }

    /// <inheritdoc/>
    public async Task<Product> GetProductAsync(string productId, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(productId))
            throw new ArgumentException("Product ID cannot be null or empty", nameof(productId));

        var response = await _authenticator.GetAsync("/api/v3/brokerage/products", cancellationToken);
        return response.Deserialize<Product>();
    }

    /// <inheritdoc/>
    public async Task<List<Candle>> GetProductCandlesAsync(string productId, string start, string end, Granularity granularity, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(productId) || string.IsNullOrEmpty(start) || string.IsNullOrEmpty(end))
            throw new ArgumentException("Product ID, start, and end time cannot be null or empty");

        var parameters = new { start, end, granularity };
        var response = await _authenticator.GetAsync(UtilityHelper.BuildParamUri($"/api/v3/brokerage/products/{productId}/candles", parameters), cancellationToken);
        return response.TryGetProperty("candles", out JsonElement candles) ? candles.Deserialize<List<Candle>>() : [];
    }

    /// <inheritdoc/>
    public async Task<MarketTrades> GetMarketTradesAsync(string productId, int limit, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(productId) || limit <= 0)
            throw new ArgumentException("Product ID cannot be null or empty, and limit must be greater than 0");

        var parameters = new { limit };

        var response = await _authenticator.GetAsync(UtilityHelper.BuildParamUri($"/api/v3/brokerage/products/{productId}/ticker", parameters), cancellationToken);

        // Extract trades data from response
        if (!response.TryGetProperty("trades", out JsonElement tradesObj) || !(tradesObj is JsonElement tradesObjElt && tradesObjElt.ValueKind == JsonValueKind.Array))
            throw new InvalidOperationException("Invalid 'trades' data in the response");

        List<Trade> trades = JsonSerializer.Deserialize<List<Trade>>(tradesObjElt.ToString());

        // Extract best bid and best ask
        string bestBid = response.TryGetProperty("best_bid", out JsonElement bestBidObj) ? bestBidObj.GetString() : null;
        string bestAsk = response.TryGetProperty("best_ask", out JsonElement bestAskObj) ? bestAskObj.GetString() : null;

        return new MarketTrades { Trades = trades, BestBid = bestBid, BestAsk = bestAsk };
    }

    /// <inheritdoc/>
    public async Task<ProductBook> GetProductBookAsync(string productId, int limit, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(productId))
            throw new ArgumentException("Product ID cannot be null or empty", nameof(productId));

        var parameters = new { product_id = productId, limit };
        var response = await _authenticator.GetAsync(UtilityHelper.BuildParamUri("/api/v3/brokerage/product_book", parameters), cancellationToken);
        return response.TryGetProperty("pricebook", out JsonElement pricebook) ? pricebook.Deserialize<ProductBook>() : null;
    }

    /// <inheritdoc/>
    public async Task<List<ProductBook>> GetBestBidAskAsync(List<string> productIds, CancellationToken cancellationToken)
    {
        if (productIds == null || !productIds.Any())
            throw new ArgumentException("Product IDs list cannot be null or empty", nameof(productIds));

        // Construct the URL with multiple product_ids
        string url = "/api/v3/brokerage/best_bid_ask?" + string.Join("&", productIds.Select(pid => $"product_ids={pid}"));
        var response = await _authenticator.GetAsync(url, cancellationToken);

        if (response.TryGetProperty("pricebooks", out JsonElement pricebooksObj) && pricebooksObj.ValueKind == JsonValueKind.Array)
            return pricebooksObj.Deserialize<List<ProductBook>>();

        return null;
    }
}
