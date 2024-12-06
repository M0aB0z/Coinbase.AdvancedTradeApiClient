using Coinbase.AdvancedTradeApiClient.Enums;
using Coinbase.AdvancedTradeApiClient.Models.Public;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Coinbase.AdvancedTradeApiClient.Interfaces;

/// <summary>
/// Interface for managing public endpoints of Coinbase Advanced Trade API.
/// </summary>
public interface IPublicManager
{
    /// <summary>
    /// Asynchronously retrieves the current server time from Coinbase.
    /// </summary>
    /// <returns>The server time details including ISO 8601 formatted date and time,
    /// number of seconds since Unix epoch, and number of milliseconds since Unix epoch,
    /// or null if the information is not available.</returns>
    Task<ServerTime> GetCoinbaseServerTimeAsync(CancellationToken cancellationToken);

    /// <summary>
    /// Asynchronously retrieves a list of public products from Coinbase.
    /// </summary>
    /// <param name="limit">The maximum number of products to retrieve. Null by default.</param>
    /// <param name="offset">The number of products to offset before returning results. Null by default.</param>
    /// <param name="productType">The type of products to retrieve (e.g., SPOT). Null by default.</param>
    /// <param name="productIds">A list of specific product IDs to retrieve. Null by default.</param>
    /// <param name="cancellationToken"></param>
    /// <returns>A list of public products or an empty list if none are found.</returns>
    Task<IReadOnlyList<PublicProduct>> ListPublicProductsAsync(int? limit = null, int? offset = null, string productType = null, List<string> productIds = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously retrieves details for a specific public product by product ID.
    /// </summary>
    /// <param name="productId">The ID of the product to retrieve details for.</param>
    /// <param name="cancellationToken"></param>
    /// <returns>The details of the specified public product or null if not found.</returns>
    Task<PublicProduct> GetPublicProductAsync(string productId, CancellationToken cancellationToken);

    /// <summary>
    /// Asynchronously retrieves the order book for a specific public product.
    /// </summary>
    /// <param name="productId">The ID of the product to retrieve the order book for.</param>
    /// <param name="limit">The maximum number of bids/asks to retrieve. Null by default.</param>
    /// <param name="cancellationToken"></param>
    /// <returns>The order book details of the specified public product or null if not found.</returns>
    Task<PublicProductBook> GetPublicProductBookAsync(string productId, int? limit = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously retrieves market trades for a specific public product.
    /// </summary>
    /// <param name="productId">The ID of the product to retrieve market trades for.</param>
    /// <param name="limit">The maximum number of trades to retrieve.</param>
    /// <param name="startTimeUtc">The starting timestamp for the range of trades to retrieve. Null by default.</param>
    /// <param name="endTimeUtc">The ending timestamp for the range of trades to retrieve. Null by default.</param>
    /// <param name="cancellationToken"></param>
    /// <returns>The market trades details of the specified public product or null if not found.</returns>
    Task<PublicMarketTrades> GetPublicMarketTradesAsync(string productId, int limit, DateTime? startTimeUtc = null, DateTime? endTimeUtc = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously retrieves candlestick data for a specific public product.
    /// </summary>
    /// <param name="productId">The ID of the product to retrieve candlestick data for.</param>
    /// <param name="startTimeUtc">The starting timestamp for the range of candlestick data to retrieve.</param>
    /// <param name="endTimeUtc">The ending timestamp for the range of candlestick data to retrieve.</param>
    /// <param name="granularity">The granularity of the candlestick data.</param>
    /// <param name="cancellationToken"></param>
    /// <returns>A list of candlestick data or an empty list if none are found.</returns>
    Task<IReadOnlyList<PublicCandle>> GetPublicProductCandlesAsync(string productId, DateTime startTimeUtc, DateTime endTimeUtc, Granularity granularity, CancellationToken cancellationToken);
}
