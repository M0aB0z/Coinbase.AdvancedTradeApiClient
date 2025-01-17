using Coinbase.AdvancedTradeApiClient.Models;
using System.Threading;
using System.Threading.Tasks;

namespace Coinbase.AdvancedTradeApiClient.Interfaces;

/// <summary>
/// Provides asynchronous methods for managing and interacting with orders.
/// </summary>
public interface IApiKeyManager
{
    /// <summary>
    /// Asynchronously retrieves the details of the API key.
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<ApiKeyDetails> GetApiKeyDetailsAsync(CancellationToken cancellationToken);
}
