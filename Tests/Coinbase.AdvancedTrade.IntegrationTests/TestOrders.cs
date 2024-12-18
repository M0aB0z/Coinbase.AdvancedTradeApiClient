﻿using Coinbase.AdvancedTradeApiClient.Enums;

namespace Coinbase.AdvancedTrade.IntegrationTests;

[TestClass]
public class TestOrders : TestBase
{
    [TestMethod]
    [Description("Test to verify that ListOrders returns a list of valid orders.")]
    public async Task Test_Orders_ListOrdersAsync()
    {
        await ExecuteRateLimitedTest(async () =>
        {
            var result = await _coinbaseClient!.Orders.ListOrdersAsync(
                "BTC-USDC",
                [OrderStatus.CANCELLED],
                new(2023, 10, 1),
                new(2023, 10, 31),
                OrderType.Limit,
                OrderSide.Buy
            );

            Assert.IsNotNull(result, "Result should not be null.");
            Assert.IsTrue(result.Count > 0, "Should return at least one order.");
            Assert.IsTrue(result.All(r => r.OrderType == OrderType.Limit), "All orders should be of type LIMIT.");
            Assert.IsTrue(result.All(r => r.Side == OrderSide.Buy), "All orders should be of side BUY.");
        });
    }




    [TestMethod]
    [Description("Test to verify that ListFills returns a list of valid fills.")]
    public async Task Test_Orders_ListFillsAsync()
    {
        await ExecuteRateLimitedTest(async () =>
        {
            var result = await _coinbaseClient!.Orders.ListFillsAsync(
                null,
                "BTC-USDC",
                new(2023, 10, 1),
                new(2023, 10, 31)
            );

            Assert.IsNotNull(result, "Result should not be null.");
            Assert.IsTrue(result.Count > 0, "Should return at least one fill.");
        });
    }

    [TestMethod]
    [Description("Test to verify that GetOrder returns a valid order.")]
    public async Task Test_Orders_GetOrderAsync()
    {
        await ExecuteRateLimitedTest(async () =>
        {
            string testOrderId = "75e5d09c-c0c7-4089-802e-69f6d672ec75";
            var result = await _coinbaseClient!.Orders.GetOrderAsync(testOrderId, CancellationToken.None);

            Assert.IsNotNull(result, "Result should not be null.");
            Assert.AreEqual(testOrderId, result?.OrderId, "Returned order ID should match the test order ID.");
        });
    }



    [TestMethod]
    [Description("Test to verify that CancelOrders cancels the provided orders and returns their results.")]
    public async Task Test_Orders_CancelOrdersAsync()
    {
        await ExecuteRateLimitedTest(async () =>
        {
            string[] testOrderIds = ["bff541d3-9991-4ec8-960a-8183a551ee57"];
            var result = await _coinbaseClient!.Orders.CancelOrdersAsync(testOrderIds, CancellationToken.None);

            Assert.IsNotNull(result, "Result should not be null.");
            Assert.IsTrue(result.Count > 0, "Should return at least one cancel order result.");
            Assert.IsTrue(result.All(r => r.Success), "All cancel requests should be successful.");
        });
    }



    [TestMethod]
    [Description("Test to verify that EditOrder successfully edits an existing order.")]
    public async Task Test_Orders_EditOrderAsync()
    {
        await ExecuteRateLimitedTest(async () =>
        {
            string existingOrderId = "4a4445ef-3203-43f9-b5dd-933eeb145417";

            decimal newPrice = 74000.00m;
            decimal? newSize = 0.0001m;

            // Attempt to edit the order
            var result = await _coinbaseClient!.Orders.EditOrderAsync(existingOrderId, newPrice, newSize, CancellationToken.None);

            // Assert that the edit operation was reported as successful
            Assert.IsTrue(result, "Edit operation should be reported as successful.");
        });
    }


    [TestMethod]
    [Description("Test to verify that EditOrderPreview successfully previewed the edit of an existing order.")]
    public async Task Test_Orders_EditOrderPreviewAsync()
    {
        await ExecuteRateLimitedTest(async () =>
        {
            string existingOrderId = "4a4445ef-3203-43f9-b5dd-933eeb145417";

            decimal newPrice = 74000.00m;
            decimal? newSize = 0.0001m;

            var result = await _coinbaseClient!.Orders.EditOrderPreviewAsync(existingOrderId, newPrice, newSize, CancellationToken.None);

            Assert.IsNotNull(result, "Preview result should not be null.");
        });
    }

}
