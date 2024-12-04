using Coinbase.AdvancedTrade.Utilities;
using System;
using System.Text.Json.Serialization;


namespace Coinbase.AdvancedTrade.Models.Internal;

/// <summary>
/// Represents an account within the Coinbase system.
/// </summary>
public class InternalAccount : IModelMapper<Account>
{
    /// <summary>
    /// Gets or sets the unique identifier for the account.
    /// </summary>
    public string Uuid
    {
        get; internal set;
    }

    /// <summary>
    /// Gets or sets the name associated with the account.
    /// </summary>
    public string Name
    {
        get; internal set;
    }

    /// <summary>
    /// Gets or sets the currency code for the account.
    /// </summary>
    public string Currency
    {
        get; internal set;
    }

    /// <summary>
    /// Gets or sets the available balance within the account.
    /// </summary>
    [JsonPropertyName("available_balance")]
    public InternalBalance AvailableBalance { get; set; }

    /// <summary>
    /// Indicates if this account is the default one for the user.
    /// </summary>
    [JsonPropertyName("default")]
    public bool Default { get; set; }

    /// <summary>
    /// Indicates if this account is active.
    /// </summary>
    [JsonPropertyName("active")]
    public bool Active { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the account was created.
    /// </summary>
    [JsonPropertyName("created_at")]
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the account was last updated.
    /// </summary>
    [JsonPropertyName("updated_at")]
    public DateTime UpdatedAt { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the account was deleted, if applicable.
    /// </summary>
    [JsonPropertyName("deleted_at")]
    public DateTime? DeletedAt { get; set; }

    /// <summary>
    /// Gets or sets the type of the account.
    /// </summary>
    public string Type
    {
        get; internal set;
    }

    /// <summary>
    /// Indicates if this account is ready for transactions.
    /// </summary>
    [JsonPropertyName("ready")]
    public bool Ready { get; set; }

    /// <summary>
    /// Gets or sets the funds on hold within the account.
    /// </summary>
    [JsonPropertyName("hold")]
    public InternalBalance Hold { get; set; }

    /// <summary>
    /// Maps the internal model to the public model.
    /// </summary>
    /// <returns></returns>
    public Account ToModel()
    {
        return new Account
        {
            Uuid = Uuid,
            Name = Name,
            Currency = Currency,
            AvailableBalance = AvailableBalance.ToModel(),
            Default = Default,
            Active = Active,
            CreatedAt = CreatedAt,
            UpdatedAt = UpdatedAt,
            DeletedAt = DeletedAt,
            Type = Type,
            Ready = Ready,
            Hold = Hold.ToModel()
        };
    }
}

/// <summary>
/// Represents a balance value and its associated currency.
/// </summary>
public class InternalBalance : IModelMapper<Balance>
{
    /// <summary>
    /// Gets or sets the value of the balance.
    /// </summary>
    public string Value
    {
        get; internal set;
    }  // Consider converting to Decimal if working with financial data

    /// <summary>
    /// Gets or sets the currency code associated with the balance.
    /// </summary>
    public string Currency
    {
        get; internal set;
    }

    /// <summary>
    /// Maps the internal model to the public model.
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public Balance ToModel()
    {
        return new Balance
        {
            Value = double.Parse(Value),
            Currency = Currency
        };
    }
}
