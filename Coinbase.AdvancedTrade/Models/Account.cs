using System;


namespace Coinbase.AdvancedTrade.Models;

/// <summary>
/// Represents an account within the Coinbase system.
/// </summary>
public class Account
{
    /// <summary>
    /// Gets or sets the unique identifier for the account.
    /// </summary>
    public string Uuid { get; internal set; }

    /// <summary>
    /// Gets or sets the name associated with the account.
    /// </summary>
    public string Name { get; internal set; }

    /// <summary>
    /// Gets or sets the currency code for the account.
    /// </summary>
    public string Currency { get; internal set; }

    /// <summary>
    /// Gets or sets the available balance within the account.
    /// </summary>
    public Balance AvailableBalance { get; internal set; }

    /// <summary>
    /// Indicates if this account is the default one for the user.
    /// </summary>
    public bool Default { get; internal set; }

    /// <summary>
    /// Indicates if this account is active.
    /// </summary>
    public bool Active { get; internal set; }

    /// <summary>
    /// Gets or sets the date and time when the account was created.
    /// </summary>
    public DateTime CreatedAt { get; internal set; }

    /// <summary>
    /// Gets or sets the date and time when the account was last updated.
    /// </summary>
    public DateTime UpdatedAt { get; internal set; }

    /// <summary>
    /// Gets or sets the date and time when the account was deleted, if applicable.
    /// </summary>
    public DateTime? DeletedAt { get; internal set; }

    /// <summary>
    /// Gets or sets the type of the account.
    /// </summary>
    public string Type { get; internal set; }

    /// <summary>
    /// Indicates if this account is ready for transactions.
    /// </summary>
    public bool Ready { get; internal set; }

    /// <summary>
    /// Gets or sets the funds on hold within the account.
    /// </summary>
    public Balance Hold { get; internal set; }
}

/// <summary>
/// Represents a balance value and its associated currency.
/// </summary>
public class Balance
{
    /// <summary>
    /// Gets or sets the value of the balance.
    /// </summary>
    public double Value { get; internal set; }  // Consider converting to Decimal if working with financial data

    /// <summary>
    /// Gets or sets the currency code associated with the balance.
    /// </summary>
    public string Currency { get; internal set; }
}
