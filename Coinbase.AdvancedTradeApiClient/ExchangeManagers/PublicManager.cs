using Coinbase.AdvancedTrade.Enums;
using Coinbase.AdvancedTrade.Interfaces;
using Coinbase.AdvancedTrade.Models.Public;
using Coinbase.AdvancedTrade.Utilities;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Coinbase.AdvancedTrade.ExchangeManagers;

/// <summary>
/// Manages public activities, including server time retrieval, for the Coinbase Advanced Trade API.
/// </summary>
public class PublicManager : BaseManager, IPublicManager
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PublicManager"/> class with an authenticator.
    /// </summary>
    /// <param name="authenticator">The Coinbase authenticator.</param>
    public PublicManager(CoinbaseAuthenticator authenticator) : base(authenticator) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="PublicManager"/> class without authentication.
    /// </summary>
    public PublicManager() : base(null) { }

    /// <inheritdoc/>
    public async Task<ServerTime> GetCoinbaseServerTimeAsync(CancellationToken cancellationToken)
    {
        try
        {
            var response = await _client.GetAsync("/api/v3/brokerage/time", cancellationToken);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<ServerTime>(content);
            }
            else
            {
                throw new InvalidOperationException($"Failed to get server time. Status: {response.StatusCode}, Content: {response.Content}");
            }
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Failed to get server time", ex);
        }
    }

    /// <inheritdoc/>
    public async Task<IReadOnlyList<PublicProduct>> ListPublicProductsAsync(int? limit = null, int? offset = null, string productType = null, List<string> productIds = null, CancellationToken cancellationToken = default)
    {
        try
        {
            var parameters = new Dictionary<string, object>();
            if (limit.HasValue)
                parameters.Add("limit", limit.Value);

            if (offset.HasValue)
                parameters.Add("offset", offset.Value);

            if (!string.IsNullOrEmpty(productType))
                parameters.Add("product_type", productType);

            if (productIds != null && productIds.Count != 0)
            {
                foreach (var productId in productIds)
                    parameters.Add("product_ids", productId);
            }

            var response = await _client.GetAsync(UtilityHelper.BuildParamUri("/api/v3/brokerage/market/products", parameters), cancellationToken);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var jsonDoc = JsonDocument.Parse(content);
                return jsonDoc.As<PublicProduct[]>("products");
            }
            else
            {
                throw new InvalidOperationException($"Failed to list public products. Status: {response.StatusCode}, Content: {response.Content}");
            }
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Failed to list public products", ex);
        }
    }

    /// <inheritdoc/>
    public async Task<PublicProduct> GetPublicProductAsync(string productId, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(productId))
            throw new ArgumentException("Product ID cannot be null or empty", nameof(productId));
        try
        {
            var response = await _client.GetAsync($"/api/v3/brokerage/market/products/{productId}", cancellationToken);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var publicProduct = JsonSerializer.Deserialize<PublicProduct>(content);
                return publicProduct;
            }
            else
                throw new InvalidOperationException($"Failed to get public product. Status: {response.StatusCode}, Content: {response.Content}");
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Failed to get public product", ex);
        }
    }

    /// <inheritdoc/>
    public async Task<PublicProductBook> GetPublicProductBookAsync(string productId, int? limit = null, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrEmpty(productId))
            throw new ArgumentException("Product ID cannot be null or empty", nameof(productId));

        try
        {
            var parameters = new Dictionary<string, object> { { "product_id", productId } };
            if (limit.HasValue)
                parameters.Add("limit", limit.Value);

            var response = await _client.GetAsync(UtilityHelper.BuildParamUri("/api/v3/brokerage/market/product_book", parameters), cancellationToken);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var jsonDoc = JsonDocument.Parse(content);
                return jsonDoc.As<PublicProductBook>("pricebook");
            }
            else
                throw new InvalidOperationException($"Failed to get public product book. Status: {response.StatusCode}, Content: {response.Content}");
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Failed to get public product book", ex);
        }
    }

    /// <inheritdoc/>
    public async Task<PublicMarketTrades> GetPublicMarketTradesAsync(string productId, int limit, DateTime? startTimeUtc = null, DateTime? endTimeUtc = null, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrEmpty(productId))
            throw new ArgumentException("Product ID cannot be null or empty", nameof(productId));

        try
        {
            var parameters = new Dictionary<string, object> { { "limit", limit } };

            if (startTimeUtc.HasValue)
                parameters.Add("start", (long)startTimeUtc.Value.Subtract(DateTime.UnixEpoch).TotalSeconds);

            if (endTimeUtc.HasValue)
                parameters.Add("end", (long)endTimeUtc.Value.Subtract(DateTime.UnixEpoch).TotalSeconds);

            var response = await _client.GetAsync(UtilityHelper.BuildParamUri($"/api/v3/brokerage/market/products/{productId}/ticker", parameters), cancellationToken);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<PublicMarketTrades>(content);
            }
            else
            {
                throw new InvalidOperationException($"Failed to get public market trades. Status: {response.StatusCode}, Content: {response.Content}");
            }
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Failed to get public market trades", ex);
        }
    }

    /// <inheritdoc/>
    public async Task<IReadOnlyList<PublicCandle>> GetPublicProductCandlesAsync(string productId, DateTime startTimeUtc, DateTime endTimeUtc, Granularity granularity, CancellationToken cancellationToken)
    {
        try
        {
            var startStr = (long)startTimeUtc.Subtract(DateTime.UnixEpoch).TotalSeconds;
            var endStr = (long)endTimeUtc.Subtract(DateTime.UnixEpoch).TotalSeconds;

            var parameters = new Dictionary<string, object>
            {
                { "start", startStr },
                { "end", endStr },
                { "granularity", granularity }
            };

            var response = await _client.GetAsync(UtilityHelper.BuildParamUri($"/api/v3/brokerage/market/products/{productId}/candles", parameters), cancellationToken);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var jsonDoc = JsonDocument.Parse(content);
                return jsonDoc.As<PublicCandle[]>("candles");
            }
            else
            {
                throw new InvalidOperationException($"Failed to get public product candles. Status: {response.StatusCode}, Content: {response.Content}");
            }
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Failed to get public product candles", ex);
        }
    }
}
