using Coinbase.AdvancedTradeApiClient.Utilities;
using Coinbase.AdvancedTradeApiClient.Utilities.Extensions;
using System.Text.Json.Serialization;

namespace Coinbase.AdvancedTradeApiClient.Models.Internal;

/// <summary>
/// Represents a tiered fee structure based on trade volume.
/// </summary>
internal class InternalFeeTier : IModelMapper<FeeTier>
{
    /// <summary>
    /// Gets or sets the pricing tier identifier.
    /// </summary>
    [JsonPropertyName("pricing_tier")]
    public string PricingTier { get; set; }

    /// <summary>
    /// Gets or sets the starting USD value for this tier.
    /// </summary>
    [JsonPropertyName("usd_from")]
    public string UsdFrom { get; set; }

    /// <summary>
    /// Gets or sets the ending USD value for this tier.
    /// </summary>
    [JsonPropertyName("usd_to")]
    public string UsdTo { get; set; }

    /// <summary>
    /// Gets or sets the fee rate for takers in this tier.
    /// </summary>
    [JsonPropertyName("taker_fee_rate")]
    public string TakerFeeRate { get; set; }

    /// <summary>
    /// Gets or sets the fee rate for makers in this tier.
    /// </summary>
    [JsonPropertyName("maker_fee_rate")]
    public string MakerFeeRate { get; set; }

    /// <summary>
    /// Maps the internal model to the public model.
    /// </summary>
    /// <returns></returns>
    public FeeTier ToModel()
    {
        return new FeeTier
        {
            PricingTier = PricingTier.ToDouble(),
            UsdFrom = UsdFrom.ToDouble(),
            UsdTo = UsdTo.ToDouble(),
            TakerFeeRate = TakerFeeRate.ToDouble(),
            MakerFeeRate = MakerFeeRate.ToDouble()
        };
    }
}

/// <summary>
/// Represents the rate at which margin is applied.
/// </summary>
internal class InternalMarginRate : IModelMapper<MarginRate>
{
    /// <summary>
    /// Gets or sets the value of the margin rate.
    /// </summary>
    [JsonPropertyName("value")]
    public string Value { get; set; }

    /// <summary>
    /// Maps the internal model to the public model.
    /// </summary>
    /// <returns></returns>
    public MarginRate ToModel()
    {
        return new MarginRate
        {
            Value = Value.ToDouble()
        };
    }
}

/// <summary>
/// Represents the tax rate for goods and services.
/// </summary>
internal class InternalGoodsAndServicesTax : IModelMapper<GoodsAndServicesTax>
{
    /// <summary>
    /// Gets or sets the tax rate value.
    /// </summary>
    [JsonPropertyName("rate")]
    public string Rate { get; set; }

    /// <summary>
    /// Gets or sets the type of tax applied (e.g., GST, VAT).
    /// </summary>
    [JsonPropertyName("type")]
    public string Type { get; set; }

    /// <summary>
    /// Maps the internal model to the public model.
    /// </summary>
    /// <returns></returns>
    public GoodsAndServicesTax ToModel()
    {
        return new GoodsAndServicesTax
        {
            Rate = Rate.ToDouble(),
            Type = Type
        };
    }
}

/// <summary>
/// Represents a summary of trading transactions.
/// </summary>
internal class InternalTransactionsSummary : IModelMapper<TransactionsSummary>
{
    /// <summary>
    /// Gets or sets the total trade volume.
    /// </summary>
    [JsonPropertyName("total_volume")]
    public double TotalVolume { get; set; }

    /// <summary>
    /// Gets or sets the total fees accumulated from trades.
    /// </summary>
    [JsonPropertyName("total_fees")]
    public double TotalFees { get; set; }

    /// <summary>
    /// Gets or sets the fee tier information for the trades.
    /// </summary>
    [JsonPropertyName("fee_tier")]
    public InternalFeeTier FeeTier { get; set; }

    /// <summary>
    /// Gets or sets the margin rate applied to the trades.
    /// </summary>
    [JsonPropertyName("margin_rate")]
    public InternalMarginRate MarginRate { get; set; }

    /// <summary>
    /// Gets or sets the goods and services tax information.
    /// </summary>
    [JsonPropertyName("goods_and_services_tax")]
    public InternalGoodsAndServicesTax GoodsAndServicesTax { get; set; }

    /// <summary>
    /// Gets or sets the trade volume specific to Advanced Trade.
    /// </summary>
    [JsonPropertyName("advanced_trade_only_volume")]
    public double AdvancedTradeOnlyVolume { get; set; }

    /// <summary>
    /// Gets or sets the total fees specific to Advanced Trade.
    /// </summary>
    [JsonPropertyName("advanced_trade_only_fees")]
    public double AdvancedTradeOnlyFees { get; set; }

    /// <summary>
    /// Gets or sets the trade volume specific to Coinbase Pro.
    /// </summary>
    [JsonPropertyName("coinbase_pro_volume")]
    public double CoinbaseProVolume { get; set; }

    /// <summary>
    /// Gets or sets the total fees specific to Coinbase Pro.
    /// </summary>
    [JsonPropertyName("coinbase_pro_fees")]
    public double CoinbaseProFees { get; set; }

    /// <summary>
    /// Gets or sets the low value for a given period.
    /// </summary>
    [JsonPropertyName("low")]
    public double Low { get; set; }

    /// <summary>
    /// Maps the internal model to the public model.
    /// </summary>
    /// <returns></returns>
    /// <exception cref="System.NotImplementedException"></exception>
    public TransactionsSummary ToModel()
    {
        return new TransactionsSummary
        {
            TotalVolume = TotalVolume,
            TotalFees = TotalFees,
            FeeTier = FeeTier.ToModel(),
            MarginRate = MarginRate.ToModel(),
            GoodsAndServicesTax = GoodsAndServicesTax.ToModel(),
            AdvancedTradeOnlyVolume = AdvancedTradeOnlyVolume,
            AdvancedTradeOnlyFees = AdvancedTradeOnlyFees,
            CoinbaseProVolume = CoinbaseProVolume,
            CoinbaseProFees = CoinbaseProFees,
            Low = Low
        };
    }
}
