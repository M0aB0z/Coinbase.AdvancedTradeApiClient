namespace Coinbase.AdvancedTradeApiClient.Enums;

/// <summary>
/// Determine which size type is used for MArket order creation
/// </summary>
public enum SizeCurrencyType
{
    /// <summary>
    /// The amount of the first Asset in the Trading Pair. For example, on the BTC-USD Order Book, BTC is the Base Asset.
    /// </summary>
    Base = 1,

    /// <summary>
    /// The amount of the second Asset in the Trading Pair. For example, on the BTC/USD Order Book, USD is the Quote Asset.
    /// </summary>
    Quote = 2,
}
