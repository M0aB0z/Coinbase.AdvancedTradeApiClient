using Coinbase.AdvancedTradeApiClient.Interfaces;
using Coinbase.AdvancedTradeApiClient.Models;
using Coinbase.AdvancedTradeApiClient.Models.Internal;
using Coinbase.AdvancedTradeApiClient.Utilities;
using Coinbase.AdvancedTradeApiClient.Utilities.Extensions;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Coinbase.AdvancedTradeApiClient.ExchangeManagers;

/// <summary>
/// Manages account-related activities for the Coinbase Advanced Trade API.
/// </summary>
public class AccountsManager : BaseManager, IAccountsManager
{
    /// <summary>
    /// Initializes a new instance of the AccountsManager class.
    /// </summary>
    /// <param name="authenticator">The Coinbase authenticator.</param>
    public AccountsManager(CoinbaseAuthenticator authenticator) : base(authenticator) { }

    /// <inheritdoc/>
    public async Task<PaginatedAccounts> ListAccountsAsync(int size = 49, string cursor = null, CancellationToken cancellationToken = default)
    {
        try
        {
            var accounts = new List<Account>();
            var hasNext = true;
            var maxPageSize = 200;
            var pageSize = Math.Min(size, maxPageSize);

            while (hasNext && accounts.Count < size)
            {
                var limit = Math.Min(size - accounts.Count, maxPageSize);
                var parameters = new { limit, cursor };

                var response = await _authenticator.GetAsync(UtilityHelper.BuildParamUri("/api/v3/brokerage/accounts", parameters));
                accounts.AddRange(response.As<InternalAccount[]>("accounts").ToModel());

                hasNext = response.As<bool>("has_next");
                cursor = response.As<string>("cursor");
            }

            return new PaginatedAccounts(hasNext, cursor, accounts);
        }
        catch (Exception ex)
        {
            // Wrap and rethrow exceptions to provide more context.
            throw new InvalidOperationException("Failed to list accounts", ex);
        }
    }

    /// <summary>
    /// List all accounts
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    public async Task<IReadOnlyList<Account>> ListAllAccountsAsync(CancellationToken cancellationToken)
    {
        try
        {
            var accounts = new List<Account>();
            var hasNext = true;
            var pageSize = 200;

            string cursor = null;

            while (hasNext)
            {
                var limit = pageSize;
                var parameters = new { limit, cursor };

                var response = await _authenticator.GetAsync(UtilityHelper.BuildParamUri("/api/v3/brokerage/accounts", parameters));
                accounts.AddRange(response.As<InternalAccount[]>("accounts").ToModel());

                hasNext = response.As<bool>("has_next");
                cursor = response.As<string>("cursor");
            }

            return accounts;
        }
        catch (Exception ex)
        {
            // Wrap and rethrow exceptions to provide more context.
            throw new InvalidOperationException("Failed to list accounts", ex);
        }
    }


    /// <inheritdoc/>
    public async Task<Account> GetAccountAsync(string accountUuid, CancellationToken cancellationToken = default)
    {
        // Check if the provided UUID is valid.
        if (string.IsNullOrEmpty(accountUuid))
            throw new ArgumentException("Account UUID cannot be null or empty", nameof(accountUuid));

        try
        {
            var response = await _authenticator.GetAsync($"/api/v3/brokerage/accounts/{accountUuid}", cancellationToken);
            return response.As<InternalAccount>("account").ToModel();
        }
        catch (Exception ex)
        {
            // Wrap and rethrow exceptions to provide more context.
            throw new InvalidOperationException($"Failed to get account with UUID {accountUuid}", ex);
        }
    }
}
