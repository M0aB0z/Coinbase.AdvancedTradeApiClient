using System.ComponentModel;

namespace Coinbase.AdvancedTradeApiClient.Enums;

/// <summary>
/// Represents the type of product.
/// </summary>
public enum ProductType
{
    /// <summary>
    /// Unknown product type.
    /// </summary>
    [Description("UNKNOWN_PRODUCT_TYPE")]
    Unknown,

    /// <summary>
    /// Represents a spot product.
    /// </summary>
    [Description("SPOT")]
    Spot,

    /// <summary>
    /// Represents a future product.
    /// </summary>
    [Description("FUTURE")]
    Future,
}
