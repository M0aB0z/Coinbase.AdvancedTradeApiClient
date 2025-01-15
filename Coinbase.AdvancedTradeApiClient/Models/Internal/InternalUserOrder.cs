using Coinbase.AdvancedTradeApiClient.Enums;
using Coinbase.AdvancedTradeApiClient.Utilities;
using Coinbase.AdvancedTradeApiClient.Utilities.Extensions;
using System;
using System.Text.Json.Serialization;

namespace Coinbase.AdvancedTradeApiClient.Models.Internal;

/// <summary>
/// User Order event details
/// </summary>
internal class InternalUserOrder : IModelMapper<UserOrder>
{
    /// <summary>
    /// Gets or sets the ID of the order.
    /// </summary>
    [JsonPropertyName("order_id")]
    public string OrderId { get; set; }

    /// <summary>
    /// Gets or sets the client-specific order ID.
    /// </summary>
    [JsonPropertyName("client_order_id")]
    public string ClientOrderId { get; set; }

    /// <summary>
    /// Gets or sets the cumulative quantity of the order.
    /// </summary>
    [JsonPropertyName("cumulative_quantity")]
    public string CumulativeQuantity { get; set; }

    /// <summary>
    /// Gets or sets the remaining quantity of the order.
    /// </summary>
    [JsonPropertyName("leaves_quantity")]
    public string LeavesQuantity { get; set; }

    /// <summary>
    /// Gets or sets the average price of the order.
    /// </summary>
    [JsonPropertyName("avg_price")]
    public string AvgPrice { get; set; }

    /// <summary>
    /// Gets or sets the total fees associated with the order.
    /// </summary>
    [JsonPropertyName("total_fees")]
    public string TotalFees { get; set; }

    /// <summary>
    /// Gets or sets the status of the order (e.g., "completed", "pending").
    /// </summary>
    [JsonPropertyName("status")]
    public string Status { get; set; }

    /// <summary>
    /// Gets or sets the ID of the product associated with the order.
    /// </summary>
    [JsonPropertyName("product_id")]
    public string ProductId { get; set; }

    /// <summary>
    /// Gets or sets the creation time of the order.
    /// </summary>
    [JsonPropertyName("creation_time")]
    public DateTime CreationTime { get; set; }

    /// <summary>
    /// Gets or sets the side of the order (e.g., "buy" or "sell").
    /// </summary>
    [JsonPropertyName("order_side")]
    public string OrderSide { get; set; }

    /// <summary>
    /// Gets or sets the type of the order (e.g., "limit", "market").
    /// </summary>
    [JsonPropertyName("order_type")]
    public string OrderType { get; set; }

    public UserOrder ToModel()
    {
        return new UserOrder
        {
            OrderId = OrderId,
            ClientOrderId = ClientOrderId,
            CumulativeQuantity = CumulativeQuantity.ToDecimal(),
            LeavesQuantity = LeavesQuantity.ToDecimal(),
            AvgPrice = AvgPrice.ToDecimal(),
            TotalFees = TotalFees.ToDecimal(),
            Status = Status,
            ProductId = ProductId,
            CreationTime = CreationTime,
            OrderSide = Enum.Parse<OrderSide>(OrderSide, true),
            OrderType = Enum.Parse<OrderType>(OrderType, true),
        };
    }
}
