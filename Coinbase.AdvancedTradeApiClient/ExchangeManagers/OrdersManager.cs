using Coinbase.AdvancedTradeApiClient.Enums;
using Coinbase.AdvancedTradeApiClient.Interfaces;
using Coinbase.AdvancedTradeApiClient.Models;
using Coinbase.AdvancedTradeApiClient.Models.Internal;
using Coinbase.AdvancedTradeApiClient.Utilities;
using Coinbase.AdvancedTradeApiClient.Utilities.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Coinbase.AdvancedTradeApiClient.ExchangeManagers;

/// <summary>
/// Manages order-related activities for the Coinbase Advanced Trade API.
/// </summary>
public class OrdersManager : BaseManager, IOrdersManager
{
    /// <summary>
    /// Initializes a new instance of the <see cref="OrdersManager"/> class.
    /// </summary>
    /// <param name="authenticator">The authenticator for Coinbase API requests.</param>
    /// <exception cref="ArgumentNullException">Thrown if the provided authenticator is null.</exception>
    public OrdersManager(CoinbaseAuthenticator authenticator) : base(authenticator)
    {
        if (authenticator == null)
            throw new ArgumentNullException(nameof(authenticator), "Authenticator cannot be null.");
    }


    /// <inheritdoc/>
    public async Task<IReadOnlyList<Order>> ListOrdersAsync(
        string productId = null,
        OrderStatus[] orderStatus = null,
        DateTime? startDate = null,
        DateTime? endDate = null,
        OrderType? orderType = null,
        OrderSide? orderSide = null,
        CancellationToken cancellationToken = default)
    {
        // Guard against invalid OrderStatus combinations
        ValidateOrderStatus(orderStatus);

        // Use utility methods for conversion
        string[] orderStatusStrings = UtilityHelper.EnumToStringArray(orderStatus);
        string startDateString = startDate?.FormatDateToISO8601();
        string endDateString = endDate?.FormatDateToISO8601();
        string orderTypeString = orderType?.GetDescription();
        string orderSideString = orderSide?.GetDescription();

        // Create an anonymous object with the parameters
        var paramsObj = new
        {
            product_id = productId,
            order_status = orderStatusStrings,
            start_date = startDateString,
            end_date = endDateString,
            order_type = orderTypeString,
            order_side = orderSideString
        };

        try
        {
            var response = await _authenticator.GetAsync(UtilityHelper.BuildParamUri("/api/v3/brokerage/orders/historical/batch", paramsObj), cancellationToken);
            return response.As<InternalOrder[]>("orders").ToModel();
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Failed to list orders", ex);
        }
    }

    private static void ValidateOrderStatus(OrderStatus[] orderStatus)
    {
        if (orderStatus != null && orderStatus.Contains(OrderStatus.OPEN) && orderStatus.Length > 1)
        {
            throw new ArgumentException("Cannot pair OPEN orders with other order types.");
        }
    }


    /// <inheritdoc/>
    public async Task<IReadOnlyList<Fill>> ListFillsAsync(
        string orderId = null,
        string productId = null,
        DateTime? startSequenceTimestamp = null,
        DateTime? endSequenceTimestamp = null,
        CancellationToken cancellationToken = default)
    {
        // Convert DateTime to the desired ISO8601 format
        string startSequenceTimestampString = startSequenceTimestamp?.FormatDateToISO8601();
        string endSequenceTimestampString = endSequenceTimestamp?.FormatDateToISO8601();

        // Prepare request parameters using anonymous type
        var paramsObj = new
        {
            order_id = orderId,
            product_id = productId,
            start_sequence_timestamp = startSequenceTimestampString,
            end_sequence_timestamp = endSequenceTimestampString
        };

        try
        {
            // Send authenticated request to the API and obtain response
            var response = await _authenticator.GetAsync(UtilityHelper.BuildParamUri("/api/v3/brokerage/orders/historical/fills", paramsObj), cancellationToken);

            // Deserialize response to obtain fills
            return response.As<InternalFill[]>("fills").ToModel();
        }
        catch (Exception ex)
        {
            // Rethrow exception with additional context
            throw new InvalidOperationException("Failed to list fills", ex);
        }
    }


    /// <inheritdoc/>
    public async Task<Order> GetOrderAsync(string orderId, CancellationToken cancellationToken = default)
    {
        // Validate input parameters
        if (string.IsNullOrWhiteSpace(orderId))
            throw new ArgumentException("Order ID cannot be null, empty, or consist only of white-space characters.", nameof(orderId));

        try
        {
            var response = await _authenticator.GetAsync($"/api/v3/brokerage/orders/historical/{orderId}", cancellationToken);
            return response.As<InternalOrder>("order").ToModel();
        }
        catch (Exception ex)
        {
            // Rethrow exception with additional context
            throw new InvalidOperationException("Failed to get the order", ex);
        }
    }

    /// <inheritdoc/>
    public async Task<IReadOnlyList<CancelOrderResult>> CancelOrdersAsync(string[] orderIds, CancellationToken cancellationToken)
    {
        // Validate the input parameter
        if (orderIds == null || orderIds.Length == 0)
            throw new ArgumentException("Order IDs array cannot be null or empty.", nameof(orderIds));

        try
        {
            // Set up the request body with the provided order IDs
            var requestBody = new { order_ids = orderIds };

            // Send authenticated request to the API to cancel the orders and obtain response
            var response = await _authenticator.PostAsync("/api/v3/brokerage/orders/historical/fills", requestBody, cancellationToken);
            return response.As<InternalCancelOrderResult[]>("results").ToModel();
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Failed to cancel orders", ex);
        }
    }


    /// <summary>
    /// Creates an order based on the provided configurations.
    /// </summary>
    /// <param name="productId">The ID of the product for the order.</param>
    /// <param name="side">Specifies whether to buy or sell.</param>
    /// <param name="orderConfiguration">Configuration details for the order.</param>
    /// <param name="cancellationToken"></param>
    /// <returns>Order ID upon successful order creation; otherwise, null.</returns>
    private async Task<string> CreateOrderAsync(string productId, OrderSide side, InternalOrderConfiguration orderConfiguration, CancellationToken cancellationToken)
    {
        // Validate the provided product ID
        if (string.IsNullOrWhiteSpace(productId))
            throw new ArgumentException("Product ID cannot be null, empty, or consist only of white-space characters.", nameof(productId));


        // Ensure order configuration is provided
        if (orderConfiguration is null)
            throw new ArgumentNullException(nameof(orderConfiguration), "Order configuration cannot be null.");

        try
        {
            // Generate a unique client order ID
            var clientOrderId = Guid.NewGuid().ToString();

            // Construct the order request payload
            var orderRequest = new
            {
                client_order_id = clientOrderId,
                product_id = productId,
                side = side.GetDescription(),
                order_configuration = orderConfiguration
            };

            // Send a POST request to create the order
            var response = await _authenticator.PostAsync("/api/v3/brokerage/orders", orderRequest, cancellationToken);

            // Check if we have a 'success_response' in the received response
            if (response.TryGetProperty("success_response", out var successResponse))
            {
                // If 'order_id' is present in the success response, return it
                if (successResponse.TryGetProperty("order_id", out var orderId) == true)
                    return orderId.GetString();
            }

            // If there's an 'error_response', handle it
            else if (response.TryGetProperty("error_response", out var errorResponseObj))
            {
                // Assuming errorResponseObj is a JsonElement
                if (errorResponseObj is JsonElement errorResponseObject) // Check if the errorResponseObj is a JSON object
                {
                    var error = errorResponseObject.TryGetProperty("error", out var errorValue) ? errorValue.GetString() : "Unknown Error";
                    var message = errorResponseObject.TryGetProperty("message", out var messageValue) ? messageValue.GetString() : "No Message";
                    var errorDetails = errorResponseObject.TryGetProperty("error_details", out var errorDetailsValue) ? errorDetailsValue.GetString() : "No Details";

                    throw new Exception($"Order creation failed. Error: {error}. Message: {message}. Details: {errorDetails}");
                }

                // If error response is not in the expected format or is empty, return string representation
                return errorResponseObj.ToString();
            }

            // If we reach here, the order creation was not successful
            return null;
        }
        catch (Exception ex)
        {
            // If an exception occurred, wrap it with additional information and rethrow
            throw new InvalidOperationException($"Failed to create order: {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Attempts to retrieve an order by its ID with retry logic, allowing for retries if the order is not immediately available.
    /// </summary>
    /// <param name="orderId">The ID of the order to retrieve.</param>
    /// <param name="maxRetries">The maximum number of retry attempts. Default is 20.</param>
    /// <param name="delay">The delay in milliseconds between retries. Default is 500ms.</param>
    /// <returns>The <see cref="InternalOrder"/> object if successfully retrieved; otherwise, null.</returns>
    /// <exception cref="ArgumentException">Thrown when the order ID is null.</exception>
    private async Task<Order> GetOrderWithRetryAsync(string orderId, int maxRetries = 20, int delay = 500)
    {
        // Validate input parameters
        if (string.IsNullOrWhiteSpace(orderId))
            throw new ArgumentException("Order ID cannot be null", nameof(orderId));

        Order order = null;
        int retryCount = 0;

        // Loop to retry fetching the order details
        while (retryCount < maxRetries)
        {
            // Attempt to get the order details
            order = await GetOrderAsync(orderId);
            if (order != null)
            {
                // Break the loop if the order is successfully retrieved
                break;
            }

            // Increment the retry count
            retryCount++;

            // Wait for the specified delay before retrying
            await Task.Delay(delay);
        }

        return order; // Return the order (or null if not retrieved within the retries)
    }


    /// <inheritdoc/>
    public async Task<string> CreateMarketOrderAsync(string productId, OrderSide side, decimal size, SizeCurrencyType sizeCurrencyType, CancellationToken cancellationToken)
    {
        // Ensure the product ID is provided and not empty
        if (string.IsNullOrEmpty(productId))
            throw new ArgumentException("Product ID cannot be null or empty.", nameof(productId));

        // Determine the market order details (size regarding asset or quote ?)
        InternalMarketIoc marketDetails = new InternalMarketIoc
        {
            QuoteSize = sizeCurrencyType == SizeCurrencyType.Quote ? size.ToApiString() : default,
            BaseSize = sizeCurrencyType == SizeCurrencyType.Base ? size.ToApiString() : default
        };

        // Create the order configuration using the determined market details
        var orderConfiguration = new InternalOrderConfiguration
        {
            MarketIoc = marketDetails
        };

        // Call the underlying order creation method with the prepared configuration
        return await CreateOrderAsync(productId, side, orderConfiguration, cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<Order> CreateMarketOrderAsync(string productId, OrderSide side, decimal size, SizeCurrencyType sizeCurrency, bool returnOrder = true, CancellationToken cancellationToken = default)
    {
        if (!returnOrder)
            throw new ArgumentException("returnOrder must be true to return an Order object.", nameof(returnOrder));

        string orderId = await CreateMarketOrderAsync(productId, side, size, sizeCurrency, cancellationToken);

        return await GetOrderWithRetryAsync(orderId);
    }


    /// <inheritdoc/>
    public async Task<string> CreateLimitOrderGTCAsync(string productId, OrderSide side, decimal baseSize, decimal limitPrice, bool postOnly, CancellationToken cancellationToken)
    {
        // Validate input parameters
        if (string.IsNullOrEmpty(productId))
            throw new ArgumentException("Product ID cannot be null or empty.", nameof(productId));

        // Prepare the order configuration for a Limit Order with GTC (Good Till Cancel)
        // This defines the parameters of the limit order, such as the amount (baseSize),
        // the price at which it's willing to trade (limitPrice), and whether it should only
        // post this order to the order book (postOnly).
        var orderConfiguration = new InternalOrderConfiguration
        {
            LimitGtc = new InternalLimitGtc
            {
                BaseSize = baseSize.ToApiString(),
                LimitPrice = limitPrice.ToApiString(),
                PostOnly = postOnly
            }
        };

        // Delegate the actual order creation to a more general-purpose method, 
        // passing in the prepared configuration.
        return await CreateOrderAsync(productId, side, orderConfiguration, cancellationToken);
    }


    /// <inheritdoc/>
    public async Task<Order> CreateLimitOrderGTCAsync(string productId, OrderSide side, decimal baseSize, decimal limitPrice, bool postOnly, bool returnOrder = true, CancellationToken cancellationToken = default)
    {
        if (!returnOrder)
            throw new ArgumentException("returnOrder must be true to return an Order object.", nameof(returnOrder));

        string orderId = await CreateLimitOrderGTCAsync(productId, side, baseSize, limitPrice, postOnly, cancellationToken);

        return await GetOrderWithRetryAsync(orderId);
    }


    /// <inheritdoc/>
    public async Task<string> CreateLimitOrderGTDAsync(string productId, OrderSide side, decimal baseSize, decimal limitPrice, DateTime endTime, bool postOnly = true, CancellationToken cancellationToken = default)
    {
        // Validate input parameters
        if (string.IsNullOrEmpty(productId))
            throw new ArgumentException("Product ID cannot be null or empty.", nameof(productId));

        if (endTime <= DateTime.UtcNow)
            throw new ArgumentException("End time should be in the future.", nameof(endTime));

        // Construct the order configuration for a Limit Order with GTD (Good Till Date)
        // This sets the parameters like the size of the order (baseSize), the desired trade price (limitPrice),
        // the time until the order remains active (endTime), and if the order should only be posted to the order book (postOnly).
        var orderConfig = new InternalOrderConfiguration
        {
            LimitGtd = new InternalLimitGtd
            {
                BaseSize = baseSize.ToApiString(),
                LimitPrice = limitPrice.ToApiString(),
                EndTime = endTime,
                PostOnly = postOnly
            }
        };

        // Delegate the actual order creation to the general-purpose method with the constructed configuration
        return await CreateOrderAsync(productId, side, orderConfig, cancellationToken);
    }


    /// <inheritdoc/>
    public async Task<Order> CreateLimitOrderGTDAsync(string productId, OrderSide side, decimal baseSize, decimal limitPrice, DateTime endTime, bool postOnly = true, bool returnOrder = true, CancellationToken cancellationToken = default)
    {
        if (!returnOrder)
            throw new ArgumentException("returnOrder must be true to return an Order object.", nameof(returnOrder));

        string orderId = await CreateLimitOrderGTCAsync(productId, side, baseSize, limitPrice, postOnly, cancellationToken);

        return await GetOrderWithRetryAsync(orderId);
    }


    /// <inheritdoc/>
    public async Task<string> CreateStopLimitOrderGTCAsync(string productId, OrderSide side, decimal baseSize, decimal limitPrice, decimal stopPrice, CancellationToken cancellationToken)
    {
        // Validate input parameters
        if (string.IsNullOrEmpty(productId))
            throw new ArgumentException("Product ID cannot be null or empty.", nameof(productId));

        // Determine stop direction based on the side of the order (BUY or SELL)
        OrderDirection stopDirection = side switch
        {
            OrderSide.Buy => OrderDirection.StopDirectionStopUp,
            OrderSide.Sell => OrderDirection.StopDirectionStopDown,
            _ => throw new ArgumentException($"Invalid order side provided: {side}.")
        };

        // Construct the order configuration for a Stop Limit Order with GTC (Good Till Cancel)
        var orderConfig = new InternalOrderConfiguration
        {
            StopLimitGtc = new InternalStopLimitGtc
            {
                BaseSize = baseSize.ToApiString(),
                LimitPrice = limitPrice.ToApiString(),
                StopPrice = stopPrice.ToApiString(),
                StopDirection = stopDirection.GetDescription()
            }
        };

        // Delegate the actual order creation to the general-purpose method with the constructed configuration
        return await CreateOrderAsync(productId, side, orderConfig, cancellationToken);
    }


    /// <inheritdoc/>
    public async Task<Order> CreateStopLimitOrderGTCAsync(string productId, OrderSide side, decimal baseSize, decimal limitPrice, decimal stopPrice, bool returnOrder = true, CancellationToken cancellationToken = default)
    {
        if (!returnOrder)
            throw new ArgumentException("returnOrder must be true to return an Order object.", nameof(returnOrder));

        string orderId = await CreateStopLimitOrderGTCAsync(productId, side, baseSize, limitPrice, stopPrice, cancellationToken);

        return await GetOrderWithRetryAsync(orderId);
    }



    /// <inheritdoc/>
    public async Task<string> CreateStopLimitOrderGTDAsync(string productId, OrderSide side, decimal baseSize, decimal limitPrice, decimal stopPrice, DateTime endTime, CancellationToken cancellationToken)
    {
        if (endTime <= DateTime.UtcNow)
            throw new ArgumentException("End time should be in the future.", nameof(endTime));

        // Determine stop direction based on the side of the order (BUY or SELL)
        OrderDirection stopDirection = side switch
        {
            OrderSide.Buy => OrderDirection.StopDirectionStopUp,
            OrderSide.Sell => OrderDirection.StopDirectionStopDown,
            _ => throw new ArgumentException($"Invalid order side provided: {side}.")
        };

        // Construct the order configuration for a Stop Limit Order with GTD (Good Till Date)
        var orderConfig = new InternalOrderConfiguration
        {
            StopLimitGtd = new InternalStopLimitGtd
            {
                BaseSize = baseSize.ToApiString(),
                LimitPrice = limitPrice.ToApiString(),
                StopPrice = stopPrice.ToApiString(),
                StopDirection = stopDirection.GetDescription(),
                EndTime = endTime
            }
        };

        // Delegate the actual order creation to the general-purpose method with the constructed configuration
        return await CreateOrderAsync(productId, side, orderConfig, cancellationToken);
    }


    /// <inheritdoc/>
    public async Task<Order> CreateStopLimitOrderGTDAsync(string productId, OrderSide side, decimal baseSize, decimal limitPrice, decimal stopPrice, DateTime endTime, bool returnOrder = true, CancellationToken cancellationToken = default)
    {
        if (!returnOrder)
            throw new ArgumentException("returnOrder must be true to return an Order object.", nameof(returnOrder));

        string orderId = await CreateStopLimitOrderGTDAsync(productId, side, baseSize, limitPrice, stopPrice, endTime, cancellationToken);

        return await GetOrderWithRetryAsync(orderId);
    }


    /// <inheritdoc/>
    public async Task<string> CreateSORLimitIOCOrderAsync(string productId, OrderSide side, decimal baseSize, decimal limitPrice, CancellationToken cancellationToken)
    {
        // Prepare the order configuration for a SOR Limit IOC order
        var orderConfiguration = new InternalOrderConfiguration
        {
            SorLimitIoc = new InternalSorLimitIoc
            {
                BaseSize = baseSize.ToApiString(),
                LimitPrice = limitPrice.ToApiString()
            }
        };

        // Delegate the actual order creation to the general-purpose method with the prepared configuration
        return await CreateOrderAsync(productId, side, orderConfiguration, cancellationToken);
    }


    /// <inheritdoc/>
    public async Task<Order> CreateSORLimitIOCOrderAsync(string productId, OrderSide side, decimal baseSize, decimal limitPrice, bool returnOrder = true, CancellationToken cancellationToken = default)
    {
        if (!returnOrder)
            throw new ArgumentException("returnOrder must be true to return an Order object.", nameof(returnOrder));

        string orderId = await CreateSORLimitIOCOrderAsync(productId, side, baseSize, limitPrice, cancellationToken);

        return await GetOrderWithRetryAsync(orderId);
    }



    /// <inheritdoc/>
    public async Task<bool> EditOrderAsync(string orderId, decimal? price = null, decimal? size = null, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrEmpty(orderId))
            throw new ArgumentException("Order ID cannot be null or empty.", nameof(orderId));

        var requestBody = new
        {
            order_id = orderId,
            price,
            size
        };

        try
        {
            var response = await _authenticator.PostAsync("/api/v3/brokerage/orders/edit", requestBody, cancellationToken);
            if (response.TryGetProperty("success", out var successValue) && successValue.GetBoolean())// Operation was successful
                return true;

            // Start constructing the error message
            var errorMessage = "Failed to edit order.";

            if (response.TryGetProperty("errors", out var errorsValue) && errorsValue.ValueKind == JsonValueKind.Array && errorsValue.GetArrayLength() > 0)
            {
                var errorDetails = errorsValue.EnumerateArray().First();

                if (errorDetails.TryGetProperty("edit_failure_reason", out var editFailureReason))
                    errorMessage += $" Reason: {editFailureReason}";
            }

            throw new InvalidOperationException(errorMessage);
        }
        catch (Exception ex)
        {
            // Log or handle the exception as needed
            throw new InvalidOperationException("Failed to edit order due to an exception.", ex);
        }
    }


    /// <inheritdoc/>
    public async Task<EditOrderPreviewResult> EditOrderPreviewAsync(string orderId, decimal? price, decimal? size, CancellationToken cancellationToken)
    {
        // Validation of input parameters
        if (string.IsNullOrEmpty(orderId))
            throw new ArgumentException("Order ID cannot be null or empty.", nameof(orderId));

        var requestBody = new
        {
            order_id = orderId,
            price,
            size
        };

        try
        {
            var response = await _authenticator.PostAsync("/api/v3/brokerage/orders/edit_preview", requestBody, cancellationToken);
            //var responseObject = UtilityHelper.DeserializeDictionary<Dictionary<string, JsonElement>>(response);

            // Check if there are errors
            if (response.TryGetProperty("errors", out var errorsValue) && errorsValue.ValueKind == JsonValueKind.Array && errorsValue.GetArrayLength() > 0)
            {
                // Convert errorsValue to a JArray and check if it's not empty
                var errorsList = errorsValue.EnumerateArray().ToList();

                var errorMessage = "Failed to preview order edit.";

                // Iterate through error objects
                foreach (var errorObj in errorsList)
                {
                    if (errorObj is JsonElement errorObject)
                    {
                        // Check for specific properties within each error object
                        if (errorObject.TryGetProperty("edit_failure_reason", out var editFailureReason))
                            errorMessage += $" Edit Failure Reason: {editFailureReason.ToString()}.";
                        if (errorObject.TryGetProperty("preview_failure_reason", out var previewFailureReason))
                            errorMessage += $" Preview Failure Reason: {previewFailureReason.ToString()}.";
                    }
                }

                throw new InvalidOperationException(errorMessage);
            }

            // Assuming no errors or empty error array, populate the EditOrderPreviewResult from responseObject
            var result = new InternalEditOrderPreviewResult
            {
                Slippage = response.As<string>("slippage"),
                OrderTotal = response.As<string>("order_total"),
                CommissionTotal = response.As<string>("commission_total"),
                QuoteSize = response.As<string>("quote_size"),
                BaseSize = response.As<string>("base_size"),
                BestBid = response.As<string>("best_bid"),
                BestAsk = response.As<string>("best_ask"),
                AverageFilledPrice = response.As<string>("average_filled_price"),
            };


            return result.ToModel();
        }
        catch (Exception ex)
        {
            // Log or handle the exception as needed
            throw new InvalidOperationException("Failed to preview order edit due to an exception.", ex);
        }
    }

}
