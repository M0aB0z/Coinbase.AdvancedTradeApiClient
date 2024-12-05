using Coinbase.AdvancedTrade.Utilities;
using System.Text.Json.Serialization;

namespace Coinbase.AdvancedTrade.Models.Internal;

/// <summary>
/// Represents the result of an order cancellation request.
/// </summary>
internal class InternalCancelOrderResult : IModelMapper<CancelOrderResult>
{
    /// <summary>
    /// Gets or sets a value indicating whether the order cancellation was successful.
    /// </summary>
    [JsonPropertyName("success")]
    public bool Success { get; set; }

    /// <summary>
    /// Gets or sets the reason for the failure, if applicable.
    /// </summary>
    [JsonPropertyName("failure_reason")]
    public string FailureReason { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier of the order that was requested to be canceled.
    /// </summary>
    [JsonPropertyName("order_id")]
    public string OrderId { get; set; }

    /// <summary>
    /// Converts this internal model to its public counterpart.
    /// </summary>
    /// <returns></returns>
    public CancelOrderResult ToModel()
    {
        return new CancelOrderResult
        {
            Success = Success,
            FailureReason = FailureReason,
            OrderId = OrderId
        };
    }
}
