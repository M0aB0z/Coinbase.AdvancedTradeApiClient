﻿using Coinbase.AdvancedTrade.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Coinbase.AdvancedTrade.Interfaces;

/// <summary>
/// Provides methods to manage accounts on the Coinbase Advanced Trade API.
/// </summary>
public interface IAccountsManager
{
    /// <summary>
    /// Asynchronously retrieves a list of accounts.
    /// </summary>
    /// <param name="limit">The maximum number of accounts to retrieve. Default is 49.</param>
    /// <param name="cursor">The cursor for pagination. Null by default.</param>
    /// <param name="cancellationToken">Request cancellationToken</param>
    /// <returns>A list of accounts or null if none are found.</returns>
    Task<IReadOnlyList<Account>> ListAccountsAsync(int limit = 49, string cursor = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously retrieves a specific account by its UUID.
    /// </summary>
    /// <param name="accountUuid">The UUID of the account.</param>
    /// <param name="cancellationToken"></param>
    /// <returns>The account corresponding to the given UUID or null if not found.</returns>
    Task<Account> GetAccountAsync(string accountUuid, CancellationToken cancellationToken = default);
}