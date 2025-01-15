using Coinbase.AdvancedTradeApiClient.Enums;
using System;

namespace Coinbase.AdvancedTradeApiClient.Models.Internal;

/// <summary>
/// User Order event details
/// </summary>
public class UserOrder
{
    /// <summary>
    /// Gets or sets the ID of the order.
    /// </summary>
    public string OrderId { get; set; }

    /// <summary>
    /// Gets or sets the client-specific order ID.
    /// </summary>
    public string ClientOrderId { get; set; }

    /// <summary>
    /// Gets or sets the cumulative quantity of the order.
    /// </summary>
    public decimal CumulativeQuantity { get; set; }

    /// <summary>
    /// Gets or sets the remaining quantity of the order.
    /// </summary>
    public decimal LeavesQuantity { get; set; }

    /// <summary>
    /// Gets or sets the average price of the order.
    /// </summary>
    public decimal AvgPrice { get; set; }

    /// <summary>
    /// Gets or sets the total fees associated with the order.
    /// </summary>
    public decimal TotalFees { get; set; }

    /// <summary>
    /// Gets or sets the status of the order (e.g., "completed", "pending").
    /// </summary>
    public string Status { get; set; }

    /// <summary>
    /// Gets or sets the ID of the product associated with the order.
    /// </summary>
    public string ProductId { get; set; }

    /// <summary>
    /// Gets or sets the creation time of the order.
    /// </summary>
    public DateTime CreationTime { get; set; }

    /// <summary>
    /// Gets or sets the side of the order (e.g., "buy" or "sell").
    /// </summary>
    public OrderSide OrderSide { get; set; }

    /// <summary>
    /// Gets or sets the type of the order (e.g., "limit", "market").
    /// </summary>
    public OrderType OrderType { get; set; }
}
