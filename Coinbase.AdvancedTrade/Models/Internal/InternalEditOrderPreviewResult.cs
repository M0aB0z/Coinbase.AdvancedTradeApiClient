using Coinbase.AdvancedTrade.Utilities;
using System.Text.Json.Serialization;

namespace Coinbase.AdvancedTrade.Models.Internal;

/// <summary>
/// Represents the result from previewing an order edit within the Coinbase system.
/// </summary>
public class InternalEditOrderPreviewResult : IModelMapper<EditOrderPreviewResult>
{
    /// <summary>
    /// Gets or sets the estimated slippage for the edited order.
    /// </summary>
    [JsonPropertyName("slippage")]
    public string Slippage { get; set; }

    /// <summary>
    /// Gets or sets the total order value after the edit.
    /// </summary>
    [JsonPropertyName("order_total")]
    public string OrderTotal { get; set; }

    /// <summary>
    /// Gets or sets the total commission for the edited order.
    /// </summary>
    [JsonPropertyName("commission_total")]
    public string CommissionTotal { get; set; }

    /// <summary>
    /// Gets or sets the size of the order in quote currency after the edit.
    /// </summary>
    [JsonPropertyName("quote_size")]
    public string QuoteSize { get; set; }

    /// <summary>
    /// Gets or sets the size of the order in base currency after the edit.
    /// </summary>
    [JsonPropertyName("base_size")]
    public string BaseSize { get; set; }

    /// <summary>
    /// Gets or sets the best bid price available for the order after the edit.
    /// </summary>
    [JsonPropertyName("best_bid")]
    public string BestBid { get; set; }

    /// <summary>
    /// Gets or sets the best ask price available for the order after the edit.
    /// </summary>
    [JsonPropertyName("best_ask")]
    public string BestAsk { get; set; }

    /// <summary>
    /// Gets or sets the average price at which the order was filled after the edit.
    /// </summary>
    [JsonPropertyName("average_filled_price")]
    public string AverageFilledPrice { get; set; }

    /// <summary>
    /// Converts this internal model to its public counterpart.
    /// </summary>
    /// <returns></returns>
    /// <exception cref="System.NotImplementedException"></exception>
    public EditOrderPreviewResult ToModel()
    {
        return new EditOrderPreviewResult
        {
            Slippage = double.Parse(Slippage),
            OrderTotal = double.Parse(OrderTotal),
            CommissionTotal = double.Parse(CommissionTotal),
            QuoteSize = double.Parse(QuoteSize),
            BaseSize = double.Parse(BaseSize),
            BestBid = double.Parse(BestBid),
            BestAsk = double.Parse(BestAsk),
            AverageFilledPrice = double.Parse(AverageFilledPrice)
        };
    }
}
