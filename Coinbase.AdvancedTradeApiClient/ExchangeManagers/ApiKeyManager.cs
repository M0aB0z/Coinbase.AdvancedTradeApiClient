using Coinbase.AdvancedTradeApiClient.Interfaces;
using Coinbase.AdvancedTradeApiClient.Models;
using Coinbase.AdvancedTradeApiClient.Utilities.Extensions;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Coinbase.AdvancedTradeApiClient.ExchangeManagers;

/// <summary>
/// Manages order-related activities for the Coinbase Advanced Trade API.
/// </summary>
public class ApiKeyManager : BaseManager, IApiKeyManager
{
    /// <summary>
    /// Initializes a new instance of the <see cref="OrdersManager"/> class.
    /// </summary>
    /// <param name="authenticator">The authenticator for Coinbase API requests.</param>
    /// <exception cref="ArgumentNullException">Thrown if the provided authenticator is null.</exception>
    public ApiKeyManager(CoinbaseAuthenticator authenticator) : base(authenticator)
    {
        if (authenticator == null)
            throw new ArgumentNullException(nameof(authenticator), "Authenticator cannot be null.");
    }

    /// <summary>
    /// Retrieves the details of the API key used for authentication.
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public async Task<ApiKeyDetails> GetApiKeyDetailsAsync(CancellationToken cancellationToken)
    {
        try
        {
            // Send authenticated request to the API and obtain response
            var response = await _authenticator.GetAsync("/api/v3/brokerage/key_permissions", cancellationToken);

            // Deserialize response to obtain api key details
            return response.As<InternalApiKeyDetails>().ToModel();
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while retrieving API key details.", ex);
        }
    }
}
