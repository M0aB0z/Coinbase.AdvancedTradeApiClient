using Coinbase.AdvancedTradeApiClient.Enums;
using Coinbase.AdvancedTradeApiClient.Models;
using System.Threading;
using System.Threading.Tasks;

namespace Coinbase.AdvancedTradeApiClient.Interfaces;

/// <summary>
/// Represents the manager for fee-related operations on the Coinbase platform.
/// </summary>
public interface IFeesManager
{
    /// <summary>
    /// Asynchronously retrieves a summary of transactions within a specified date range.
    /// </summary>
    /// <param name="productType">The type of product. Defaults to "SPOT".</param>
    /// <param name="productVenue">The product venue</param>
    /// <param name="contractExpiryType">The contract expiry type</param>
    /// <param name="cancellationToken">Your cancellation token</param>
    /// <returns>A task representing the operation. The result of the task is a summary of the transactions or null if none are found.</returns>
    Task<TransactionsSummary> GetTransactionsSummaryAsync(
    ProductType productType,
    ProductVenue productVenue,
    ContractExpiryType contractExpiryType, CancellationToken cancellationToken);
}
