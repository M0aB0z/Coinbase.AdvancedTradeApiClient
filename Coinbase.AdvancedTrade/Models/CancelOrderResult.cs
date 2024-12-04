namespace Coinbase.AdvancedTrade.Models;

/// <summary>
/// Represents the result of an order cancellation request.
/// </summary>
public class CancelOrderResult
{
    /// <summary>
    /// Gets or sets a value indicating whether the order cancellation was successful.
    /// </summary>
    public bool Success { get; internal set; }

    /// <summary>
    /// Gets or sets the reason for the failure, if applicable.
    /// </summary>
    public string FailureReason { get; internal set; }

    /// <summary>
    /// Gets or sets the unique identifier of the order that was requested to be canceled.
    /// </summary>
    public string OrderId { get; internal set; }
}
