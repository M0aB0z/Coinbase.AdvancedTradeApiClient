namespace Coinbase.AdvancedTrade.Models;

/// <summary>
/// Represents the result from previewing an order edit within the Coinbase system.
/// </summary>
public class EditOrderPreviewResult
{
    /// <summary>
    /// Gets the estimated slippage for the edited order.
    /// </summary>
    public double Slippage { get; internal set; }

    /// <summary>
    /// Gets the total order value after the edit.
    /// </summary>
    public double OrderTotal { get; internal set; }

    /// <summary>
    /// Gets the total commission for the edited order.
    /// </summary>
    public double CommissionTotal { get; internal set; }

    /// <summary>
    /// Gets the size of the order in quote currency after the edit.
    /// </summary>
    public double QuoteSize { get; internal set; }

    /// <summary>
    /// Gets the size of the order in base currency after the edit.
    /// </summary>
    public double BaseSize { get; internal set; }

    /// <summary>
    /// Gets the best bid price available for the order after the edit.
    /// </summary>
    public double BestBid { get; internal set; }

    /// <summary>
    /// Gets the best ask price available for the order after the edit.
    /// </summary>
    public double BestAsk { get; internal set; }

    /// <summary>
    /// Gets the average price at which the order was filled after the edit.
    /// </summary>
    public double AverageFilledPrice { get; internal set; }
}
