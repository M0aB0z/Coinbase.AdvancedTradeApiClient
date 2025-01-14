using System.Collections.Generic;


namespace Coinbase.AdvancedTradeApiClient.Models;

/// <summary>
/// Represents an account page
/// </summary>
public class PaginatedAccounts
{
    /// <summary>
    /// Is there any other page
    /// </summary>
    public bool HasNext { get; internal set; }

    /// <summary>
    ///  Pagination cursor
    /// </summary>
    public string Cursor { get; internal set; }

    /// <summary>
    /// Accounts current page
    /// </summary>
    public IReadOnlyList<Account> Accounts { get; internal set; }

    internal PaginatedAccounts(bool hasNext, string cursor, List<Account> accounts)
    {
        HasNext = hasNext;
        Cursor = cursor;
        Accounts = accounts;
    }

    /// <inheritdoc/>
    public override string ToString() => $"{Accounts.Count} Accounts - HasNext: {HasNext}";
}

