using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Coinbase.AdvancedTrade.Models
{
    // Represents an order within the Coinbase AdvancedTrade system.
    /// <summary>
    /// Represents an order within the Coinbase AdvancedTrade system.
    /// </summary>
    public class Order
    {
        /// <summary>
        /// The unique identifier for the order.
        /// </summary>
        [JsonPropertyName("order_id")]
        public string OrderId { get; set; }

        /// <summary>
        /// The identifier for the product associated with the order.
        /// </summary>
        [JsonPropertyName("product_id")]
        public string ProductId { get; set; }

        /// <summary>
        /// The user identifier who created the order.
        /// </summary>
        [JsonPropertyName("user_id")]
        public string UserId { get; set; }

        /// <summary>
        /// Configuration details for the order.
        /// </summary>
        [JsonPropertyName("order_configuration")]
        public OrderConfiguration OrderConfiguration { get; set; }

        /// <summary>
        /// Indicates if the order is a buy or sell.
        /// </summary>
        [JsonPropertyName("side")]
        public string Side { get; set; }

        /// <summary>
        /// The client's custom identifier for the order.
        /// </summary>
        [JsonPropertyName("client_order_id")]
        public string ClientOrderId { get; set; }

        /// <summary>
        /// The current status of the order.
        /// </summary>
        [JsonPropertyName("status")]
        public string Status { get; set; }

        /// <summary>
        /// How long the order will remain active.
        /// </summary>
        [JsonPropertyName("time_in_force")]
        public string TimeInForce { get; set; }

        /// <summary>
        /// The timestamp when the order was created.
        /// </summary>
        [JsonPropertyName("created_time")]
        public DateTime? CreatedTime { get; set; }

        /// <summary>
        /// The percentage of the order that has been completed.
        /// </summary>
        [JsonPropertyName("completion_percentage")]
        public string CompletionPercentage { get; set; }

        /// <summary>
        /// The quantity of the product that has been filled in the order.
        /// </summary>
        [JsonPropertyName("filled_size")]
        public string FilledSize { get; set; }

        /// <summary>
        /// The average price at which the order has been filled.
        /// </summary>
        [JsonPropertyName("average_filled_price")]
        public string AverageFilledPrice { get; set; }

        /// <summary>
        /// The fee associated with the order.
        /// </summary>
        [JsonPropertyName("fee")]
        public string Fee { get; set; }

        /// <summary>
        /// The number of times the order has been filled.
        /// </summary>
        [JsonPropertyName("number_of_fills")]
        public string NumberOfFills { get; set; }

        /// <summary>
        /// The total value of the filled portions of the order.
        /// </summary>
        [JsonPropertyName("filled_value")]
        public string FilledValue { get; set; }

        /// <summary>
        /// Indicates if the order is pending cancellation.
        /// </summary>
        [JsonPropertyName("pending_cancel")]
        public bool? PendingCancel { get; set; }

        /// <summary>
        /// If true, the size of the order is specified in the quote currency.
        /// </summary>
        [JsonPropertyName("size_in_quote")]
        public bool? SizeInQuote { get; set; }

        /// <summary>
        /// The total fees associated with the order.
        /// </summary>
        [JsonPropertyName("total_fees")]
        public string TotalFees { get; set; }

        /// <summary>
        /// If true, the size of the order includes fees.
        /// </summary>
        [JsonPropertyName("size_inclusive_of_fees")]
        public bool? SizeInclusiveOfFees { get; set; }

        /// <summary>
        /// The total value of the order after fees have been deducted.
        /// </summary>
        [JsonPropertyName("total_value_after_fees")]
        public string TotalValueAfterFees { get; set; }

        /// <summary>
        /// The status of the order's trigger if applicable.
        /// </summary>
        [JsonPropertyName("trigger_status")]
        public string TriggerStatus { get; set; }

        /// <summary>
        /// The type of order (e.g. market, limit, stop).
        /// </summary>
        [JsonPropertyName("order_type")]
        public string OrderType { get; set; }

        /// <summary>
        /// Reason for order rejection, if applicable.
        /// </summary>
        [JsonPropertyName("reject_reason")]
        public string RejectReason { get; set; }

        /// <summary>
        /// Indicates if the order has been settled.
        /// </summary>
        [JsonPropertyName("settled")]
        public bool? Settled { get; set; }

        /// <summary>
        /// The type of product associated with the order.
        /// </summary>
        [JsonPropertyName("product_type")]
        public string ProductType { get; set; }

        /// <summary>
        /// A message providing more details about the rejection reason.
        /// </summary>
        [JsonPropertyName("reject_message")]
        public string RejectMessage { get; set; }

        /// <summary>
        /// A message providing more details if the order was canceled.
        /// </summary>
        [JsonPropertyName("cancel_message")]
        public string CancelMessage { get; set; }

        /// <summary>
        /// The source from which the order was placed (e.g. web, API).
        /// </summary>
        [JsonPropertyName("order_placement_source")]
        public string OrderPlacementSource { get; set; }

        /// <summary>
        /// The amount of the order that is currently on hold.
        /// </summary>
        [JsonPropertyName("outstanding_hold_amount")]
        public string OutstandingHoldAmount { get; set; }

        /// <summary>
        /// Indicates if the order is a liquidation order.
        /// </summary>
        [JsonPropertyName("is_liquidation")]
        public bool? IsLiquidation { get; set; }

        /// <summary>
        /// An array of the latest 5 edits per order.
        /// </summary>
        [JsonPropertyName("edit_history")]
        public List<EditHistoryEntry> EditHistory { get; set; }
    }


    /// <summary>
    /// Represents specific configurations for different types of orders.
    /// </summary>
    public class OrderConfiguration
    {
        /// <summary>
        /// Configuration details for market-market IOC orders.
        /// </summary>
        [JsonPropertyName("market_market_ioc")]
        public MarketIoc MarketIoc { get; set; }

        /// <summary>
        /// Configuration details for limit-limit GTC orders.
        /// </summary>
        [JsonPropertyName("limit_limit_gtc")]
        public LimitGtc LimitGtc { get; set; }

        /// <summary>
        /// Configuration details for limit-limit GTD orders.
        /// </summary>
        [JsonPropertyName("limit_limit_gtd")]
        public LimitGtd LimitGtd { get; set; }

        /// <summary>
        /// Configuration details for stop-limit-stop-limit GTC orders.
        /// </summary>
        [JsonPropertyName("stop_limit_stop_limit_gtc")]
        public StopLimitGtc StopLimitGtc { get; set; }

        /// <summary>
        /// Configuration details for stop-limit-stop-limit GTD orders.
        /// </summary>
        [JsonPropertyName("stop_limit_stop_limit_gtd")]
        public StopLimitGtd StopLimitGtd { get; set; }

        /// <summary>
        /// Configuration details for sor-limit-ioc orders.
        /// </summary>
        [JsonPropertyName("sor_limit_ioc")]
        public SorLimitIoc SorLimitIoc { get; set; }
    }

    /// <summary>
    /// Represents configuration details for market-market IOC orders.
    /// </summary>
    public class MarketIoc
    {
        /// <summary>
        /// The size of the order in the quote currency.
        /// </summary>
        [JsonPropertyName("quote_size")]
        public string QuoteSize { get; set; }

        /// <summary>
        /// The size of the order in the base currency.
        /// </summary>
        [JsonPropertyName("base_size")]
        public string BaseSize { get; set; }
    }

    /// <summary>
    /// Represents configuration details for limit-limit GTC orders.
    /// </summary>
    public class LimitGtc
    {
        /// <summary>
        /// The size of the order in the base currency.
        /// </summary>
        [JsonPropertyName("base_size")]
        public string BaseSize { get; set; }

        /// <summary>
        /// The limit price for the order.
        /// </summary>
        [JsonPropertyName("limit_price")]
        public string LimitPrice { get; set; }

        /// <summary>
        /// Indicates if the order can only be posted to the order book.
        /// </summary>
        [JsonPropertyName("post_only")]
        public bool? PostOnly { get; set; }
    }

    /// <summary>
    /// Represents configuration details for limit-limit GTD orders.
    /// </summary>
    public class LimitGtd : LimitGtc
    {
        /// <summary>
        /// The time when the order will expire.
        /// </summary>
        [JsonPropertyName("end_time")]
        public DateTime EndTime { get; set; }
    }

    /// <summary>
    /// Represents configuration details for stop-limit-stop-limit GTC orders.
    /// </summary>
    public class StopLimitGtc
    {
        /// <summary>
        /// The size of the order in the base currency.
        /// </summary>
        [JsonPropertyName("base_size")]
        public string BaseSize { get; set; }

        /// <summary>
        /// The limit price for the order.
        /// </summary>
        [JsonPropertyName("limit_price")]
        public string LimitPrice { get; set; }

        /// <summary>
        /// The stop price for the order.
        /// </summary>
        [JsonPropertyName("stop_price")]
        public string StopPrice { get; set; }

        /// <summary>
        /// The direction in which the stop price is triggered (e.g. 'above', 'below').
        /// </summary>
        [JsonPropertyName("stop_direction")]
        public string StopDirection { get; set; }
    }

    /// <summary>
    /// Represents configuration details for stop-limit-stop-limit GTD orders.
    /// </summary>
    public class StopLimitGtd : StopLimitGtc
    {
        /// <summary>
        /// The time when the order will expire.
        /// </summary>
        [JsonPropertyName("end_time")]
        public DateTime EndTime { get; set; }
    }

    /// <summary>
    /// Represents configuration details for sor-limit-ioc orders.
    /// </summary>
    public class SorLimitIoc
    {
        /// <summary>
        /// The size of the order in the base currency.
        /// </summary>
        [JsonPropertyName("base_size")]
        public string BaseSize { get; set; }

        /// <summary>
        /// The limit price for the order.
        /// </summary>
        [JsonPropertyName("limit_price")]
        public string LimitPrice { get; set; }
    }

    /// <summary>
    /// Represents an edit history entry for an order.
    /// </summary>
    public class EditHistoryEntry
    {
        /// <summary>
        /// The price associated with the edit.
        /// </summary>
        [JsonPropertyName("price")]
        public string Price { get; set; }

        /// <summary>
        /// The size associated with the edit.
        /// </summary>
        [JsonPropertyName("size")]
        public string Size { get; set; }

        /// <summary>
        /// The timestamp when the edit was accepted.
        /// </summary>
        [JsonPropertyName("replace_accept_timestamp")]
        public DateTime? ReplaceAcceptTimestamp { get; set; }
    }


}
