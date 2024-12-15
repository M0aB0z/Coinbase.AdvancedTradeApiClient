namespace Coinbase.AdvancedTradeApiClient.Models;

/// <summary>
/// Represents the result from previewing an order edit within the Coinbase system.
/// </summary>
public class EditOrderPreviewResult
{
    /// <summary>
    /// Gets the estimated slippage for the edited order.
    /// </summary>
    public decimal Slippage { get; internal set; }

    /// <summary>
    /// Gets the total order value after the edit.
    /// </summary>
    public decimal OrderTotal { get; internal set; }

    /// <summary>
    /// Gets the total commission for the edited order.
    /// </summary>
    public decimal CommissionTotal { get; internal set; }

    /// <summary>
    /// Gets the size of the order in quote currency after the edit.
    /// </summary>
    public decimal QuoteSize { get; internal set; }

    /// <summary>
    /// Gets the size of the order in base currency after the edit.
    /// </summary>
    public decimal BaseSize { get; internal set; }

    /// <summary>
    /// Gets the best bid price available for the order after the edit.
    /// </summary>
    public decimal BestBid { get; internal set; }

    /// <summary>
    /// Gets the best ask price available for the order after the edit.
    /// </summary>
    public decimal BestAsk { get; internal set; }

    /// <summary>
    /// Gets the average price at which the order was filled after the edit.
    /// </summary>
    public decimal AverageFilledPrice { get; internal set; }

}
