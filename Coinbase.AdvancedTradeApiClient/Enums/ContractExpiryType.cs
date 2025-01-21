using System.ComponentModel;

namespace Coinbase.AdvancedTradeApiClient.Enums;

/// <summary>
/// Represents the contract expiry type.
/// </summary>
public enum ContractExpiryType
{
    /// <summary>
    /// Unknown product expiry.
    /// </summary>
    [Description("UNKNOWN_CONTRACT_EXPIRY_TYPE")]
    Unknown,

    /// <summary>
    /// Represents a expiring contract.
    /// </summary>
    [Description("EXPIRING")]
    Expiring,

    /// <summary>
    /// Represents a perpetual contract.
    /// </summary>
    [Description("PERPETUAL")]
    Perpetual,

}
