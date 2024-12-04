namespace Coinbase.AdvancedTrade.Models;

/// <summary>
/// Represents the result from previewing an order edit within the Coinbase system.
/// </summary>
public class EditOrderPreviewResult
{
    /// <summary>
    /// Gets or sets the estimated slippage for the edited order.
    /// </summary>
    public double Slippage { get; set; }

    /// <summary>
    /// Gets or sets the total order value after the edit.
    /// </summary>
    public double OrderTotal { get; set; }

    /// <summary>
    /// Gets or sets the total commission for the edited order.
    /// </summary>
    public double CommissionTotal { get; set; }

    /// <summary>
    /// Gets or sets the size of the order in quote currency after the edit.
    /// </summary>
    public double QuoteSize { get; set; }

    /// <summary>
    /// Gets or sets the size of the order in base currency after the edit.
    /// </summary>
    public double BaseSize { get; set; }

    /// <summary>
    /// Gets or sets the best bid price available for the order after the edit.
    /// </summary>
    public double BestBid { get; set; }

    /// <summary>
    /// Gets or sets the best ask price available for the order after the edit.
    /// </summary>
    public double BestAsk { get; set; }

    /// <summary>
    /// Gets or sets the average price at which the order was filled after the edit.
    /// </summary>
    public double AverageFilledPrice { get; set; }
}
