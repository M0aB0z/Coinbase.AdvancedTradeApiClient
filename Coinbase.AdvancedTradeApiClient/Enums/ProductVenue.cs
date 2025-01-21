using System.ComponentModel;

namespace Coinbase.AdvancedTradeApiClient.Enums;

/// <summary>
/// Represents the product venue.
/// </summary>
public enum ProductVenue
{
    /// <summary>
    /// Unknown product venue.
    /// </summary>
    [Description("UNKNOWN_VENUE_TYPE")]
    Unknown,

    /// <summary>
    /// Represents a CBE product venue.
    /// </summary>
    [Description("CBE")]
    CBE,

    /// <summary>
    /// Represents a FCM product venue.
    /// </summary>
    [Description("FCM")]
    FCM,

    /// <summary>
    /// Represents a INTX product venue.
    /// </summary>
    [Description("INTX")]
    INTX,
}
