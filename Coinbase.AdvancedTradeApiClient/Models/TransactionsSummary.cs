namespace Coinbase.AdvancedTradeApiClient.Models;

/// <summary>
/// Represents a tiered fee structure based on trade volume.
/// </summary>
public class FeeTier
{
    /// <summary>
    /// Gets or sets the pricing tier identifier.
    /// </summary>
    public decimal PricingTier { get; internal set; }

    /// <summary>
    /// Gets or sets the starting USD value for this tier.
    /// </summary>
    public decimal UsdFrom { get; internal set; }

    /// <summary>
    /// Gets or sets the ending USD value for this tier.
    /// </summary>
    public decimal UsdTo { get; internal set; }

    /// <summary>
    /// Gets or sets the fee rate for takers in this tier.
    /// </summary>
    public decimal TakerFeeRate { get; internal set; }

    /// <summary>
    /// Gets or sets the fee rate for makers in this tier.
    /// </summary>
    public decimal MakerFeeRate { get; internal set; }
}

/// <summary>
/// Represents the rate at which margin is applied.
/// </summary>
public class MarginRate
{
    /// <summary>
    /// Gets or sets the value of the margin rate.
    /// </summary>
    public decimal Value { get; internal set; }
}

/// <summary>
/// Represents the tax rate for goods and services.
/// </summary>
public class GoodsAndServicesTax
{
    /// <summary>
    /// Gets or sets the tax rate value.
    /// </summary>
    public decimal Rate { get; internal set; }

    /// <summary>
    /// Gets or sets the type of tax applied (e.g., GST, VAT).
    /// </summary>
    public string Type { get; internal set; }
}

/// <summary>
/// Represents a summary of trading transactions.
/// </summary>
public class TransactionsSummary
{
    /// <summary>
    /// Gets or sets the total trade volume.
    /// </summary>
    public decimal TotalVolume { get; internal set; }

    /// <summary>
    /// Gets or sets the total fees accumulated from trades.
    /// </summary>
    public decimal TotalFees { get; internal set; }

    /// <summary>
    /// Gets or sets the fee tier information for the trades.
    /// </summary>
    public FeeTier FeeTier { get; internal set; }

    /// <summary>
    /// Gets or sets the margin rate applied to the trades.
    /// </summary>
    public MarginRate MarginRate { get; internal set; }

    /// <summary>
    /// Gets or sets the goods and services tax information.
    /// </summary>
    public GoodsAndServicesTax GoodsAndServicesTax { get; internal set; }

    /// <summary>
    /// Gets or sets the trade volume specific to Advanced Trade.
    /// </summary>
    public decimal AdvancedTradeOnlyVolume { get; internal set; }

    /// <summary>
    /// Gets or sets the total fees specific to Advanced Trade.
    /// </summary>
    public decimal AdvancedTradeOnlyFees { get; internal set; }

    /// <summary>
    /// Gets or sets the trade volume specific to Coinbase Pro.
    /// </summary>
    public decimal CoinbaseProVolume { get; internal set; }

    /// <summary>
    /// Gets or sets the total fees specific to Coinbase Pro.
    /// </summary>
    public decimal CoinbaseProFees { get; internal set; }

    /// <summary>
    /// Gets or sets the low value for a given period.
    /// </summary>
    public decimal Low { get; internal set; }
}
