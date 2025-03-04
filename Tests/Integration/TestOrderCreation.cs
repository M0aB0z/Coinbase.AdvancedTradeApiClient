﻿using Coinbase.AdvancedTradeApiClient.Enums;
using Coinbase.AdvancedTradeApiClient.Models;

namespace Coinbase.AdvancedTrade.IntegrationTests;

[TestClass]
public class TestOrderCreation : TestBase
{
    [TestMethod]
    [Description("Test to verify that CreateMarketOrderBuy creates an order for BTC-USDC.")]
    public async Task Test_Orders_CreateMarketOrderBuyAsync()
    {
        await ExecuteRateLimitedTest(async () =>
        {
            Assert.IsNotNull(_coinbaseClient, "Coinbase client should not be null.");
            var orderNumber = await _coinbaseClient!.Orders.CreateMarketOrderAsync("BTC-USDC", OrderSide.Buy, 1, SizeCurrencyType.Quote);
            Assert.IsNotNull(orderNumber, "Order Number should not be null.");
        });
    }

    [TestMethod]
    [Description("Test to verify that CreateMarketOrderBuyReturningOrder creates and retrieves an order object for BTC-USDC.")]
    public async Task Test_Orders_CreateMarketOrderBuyReturningOrderAsync()
    {
        await ExecuteRateLimitedTest(async () =>
        {
            Assert.IsNotNull(_coinbaseClient, "Coinbase client should not be null.");

            // Create market order and retrieve order details
            Order order = await _coinbaseClient!.Orders.CreateMarketOrderAsync(
                productId: "BTC-USDC",
                side: OrderSide.Buy,
                size: 1,
                SizeCurrencyType.Quote,
                returnOrder: true
            );

            // Assert order details
            Assert.IsNotNull(order, "Order should not be null.");
            Assert.IsNotNull(order.OrderId, "Order ID should not be null.");
            Assert.AreEqual("BTC-USDC", order.ProductId, "Product ID should match.");
        });
    }


    [TestMethod]
    [Description("Test to verify that CreateMarketOrderSell creates a sell order for BTC-USDC.")]
    public async Task Test_Orders_CreateMarketOrderSellAsync()
    {
        await ExecuteRateLimitedTest(async () =>
        {
            Assert.IsNotNull(_coinbaseClient, "Coinbase client should not be null.");
            var orderNumber = await _coinbaseClient!.Orders.CreateMarketOrderAsync("BTC-USDC", OrderSide.Sell, 0.00001m, SizeCurrencyType.Quote);
            Assert.IsNotNull(orderNumber, "Order Number should not be null.");
        });
    }


    [TestMethod]
    [Description("Test to verify that CreateMarketOrderSellReturningOrder creates and retrieves a sell order object for BTC-USDC.")]
    public async Task Test_Orders_CreateMarketOrderSellReturningOrderAsync()
    {
        await ExecuteRateLimitedTest(async () =>
        {
            Assert.IsNotNull(_coinbaseClient, "Coinbase client should not be null.");

            // Create market sell order and retrieve order details
            Order order = await _coinbaseClient!.Orders.CreateMarketOrderAsync(
                productId: "BTC-USDC",
                side: OrderSide.Sell,
                size: 0.00001m,
                SizeCurrencyType.Base,
                returnOrder: true
            );

            // Assert order details
            Assert.IsNotNull(order, "Order should not be null.");
            Assert.IsNotNull(order.OrderId, "Order ID should not be null.");
            Assert.AreEqual("BTC-USDC", order.ProductId, "Product ID should match.");
        });
    }




    [TestMethod]
    [Description("Test to verify that CreateLimitOrderGTCBuy creates a limit order for BTC-USDC.")]
    public async Task Test_Orders_CreateLimitOrderGTCBuyAsync()
    {
        await ExecuteRateLimitedTest(async () =>
        {
            Assert.IsNotNull(_coinbaseClient, "Coinbase client should not be null.");
            var orderNumber = await _coinbaseClient!.Orders.CreateLimitOrderGTCAsync("BTC-USDC", OrderSide.Buy, 0.0001m, 10000, true, CancellationToken.None);
            Assert.IsNotNull(orderNumber, "Order number should not be null.");
        });
    }


    [TestMethod]
    [Description("Test to verify that CreateLimitOrderGTCBuyReturningOrder creates and retrieves a limit buy order object for BTC-USDC.")]
    public async Task Test_Orders_CreateLimitOrderGTCBuyReturningOrderAsync()
    {
        await ExecuteRateLimitedTest(async () =>
        {
            Assert.IsNotNull(_coinbaseClient, "Coinbase client should not be null.");

            // Create limit buy order and retrieve order details
            Order order = await _coinbaseClient!.Orders.CreateLimitOrderGTCAsync(
                productId: "BTC-USDC",
                side: OrderSide.Buy,
                baseSize: 0.0001m,
                limitPrice: 10000,
                postOnly: true,
                returnOrder: true
            );

            // Assert order details
            Assert.IsNotNull(order, "Order should not be null.");
            Assert.IsNotNull(order.OrderId, "Order ID should not be null.");
            Assert.AreEqual("BTC-USDC", order.ProductId, "Product ID should match.");
        });
    }


    [TestMethod]
    [Description("Test to verify that CreateLimitOrderGTCSell creates a limit sell order for BTC-USDC.")]
    public async Task Test_Orders_CreateLimitOrderGTCSellAsync()
    {
        await ExecuteRateLimitedTest(async () =>
        {
            Assert.IsNotNull(_coinbaseClient, "Coinbase client should not be null.");
            var orderNumber = await _coinbaseClient!.Orders.CreateLimitOrderGTCAsync("BTC-USDC", OrderSide.Sell, 0.0001m, 75000, true, CancellationToken.None);
            Assert.IsNotNull(orderNumber, "Order number should not be null.");
        });
    }


    [TestMethod]
    [Description("Test to verify that CreateLimitOrderGTCSellReturningOrder creates and retrieves a limit sell order object for BTC-USDC.")]
    public async Task Test_Orders_CreateLimitOrderGTCSellReturningOrderAsync()
    {
        await ExecuteRateLimitedTest(async () =>
        {
            Assert.IsNotNull(_coinbaseClient, "Coinbase client should not be null.");

            // Create limit sell order and retrieve order details
            Order order = await _coinbaseClient!.Orders.CreateLimitOrderGTCAsync(
                productId: "BTC-USDC",
                side: OrderSide.Sell,
                baseSize: 0.0001m,
                limitPrice: 75000,
                postOnly: true,
                returnOrder: true
            );

            // Assert order details
            Assert.IsNotNull(order, "Order should not be null.");
            Assert.IsNotNull(order.OrderId, "Order ID should not be null.");
            Assert.AreEqual("BTC-USDC", order.ProductId, "Product ID should match.");
        });
    }



    [TestMethod]
    [Description("Test to verify that CreateLimitOrderGTDBuy creates a limit order for BTC-USDC.")]
    public async Task Test_Orders_CreateLimitOrderGTDBuyAsync()
    {
        await ExecuteRateLimitedTest(async () =>
        {
            Assert.IsNotNull(_coinbaseClient, "Coinbase client should not be null.");
            var orderNumber = await _coinbaseClient!.Orders.CreateLimitOrderGTDAsync("BTC-USDC", OrderSide.Buy, 0.0001m, 10000, DateTime.UtcNow.AddDays(1), true, CancellationToken.None);
            Assert.IsNotNull(orderNumber, "Order number should not be null.");
        });
    }


    [TestMethod]
    [Description("Test to verify that CreateLimitOrderGTDBuyReturningOrder creates and retrieves a limit buy order object for BTC-USDC.")]
    public async Task Test_Orders_CreateLimitOrderGTDBuyReturningOrderAsync()
    {
        await ExecuteRateLimitedTest(async () =>
        {
            Assert.IsNotNull(_coinbaseClient, "Coinbase client should not be null.");

            // Create limit buy order and retrieve order details
            Order order = await _coinbaseClient!.Orders.CreateLimitOrderGTDAsync(
                productId: "BTC-USDC",
                side: OrderSide.Buy,
                baseSize: 0.0001m,
                limitPrice: 10000,
                endTime: DateTime.UtcNow.AddDays(1),
                postOnly: true,
                returnOrder: true
            );

            // Assert order details
            Assert.IsNotNull(order, "Order should not be null.");
            Assert.IsNotNull(order.OrderId, "Order ID should not be null.");
            Assert.AreEqual("BTC-USDC", order.ProductId, "Product ID should match.");
        });
    }




    [TestMethod]
    [Description("Test to verify that CreateLimitOrderGTDSell creates a limit sell order for BTC-USDC.")]
    public async Task Test_Orders_CreateLimitOrderGTDSellAsync()
    {
        await ExecuteRateLimitedTest(async () =>
        {
            Assert.IsNotNull(_coinbaseClient, "Coinbase client should not be null.");
            var orderNumber = await _coinbaseClient!.Orders.CreateLimitOrderGTDAsync("BTC-USDC", OrderSide.Sell, 0.0001m, 75000, DateTime.UtcNow.AddDays(1), true, CancellationToken.None);
            Assert.IsNotNull(orderNumber, "Order number should not be null.");
        });
    }


    [TestMethod]
    [Description("Test to verify that CreateLimitOrderGTDSellReturningOrder creates and retrieves a limit sell order object for BTC-USDC.")]
    public async Task Test_Orders_CreateLimitOrderGTDSellReturningOrderAsync()
    {
        await ExecuteRateLimitedTest(async () =>
        {
            Assert.IsNotNull(_coinbaseClient, "Coinbase client should not be null.");

            // Create limit sell order and retrieve order details
            Order order = await _coinbaseClient!.Orders.CreateLimitOrderGTDAsync(
                productId: "BTC-USDC",
                side: OrderSide.Sell,
                baseSize: 0.0001m,
                limitPrice: 75000,
                endTime: DateTime.UtcNow.AddDays(1),
                postOnly: true,
                returnOrder: true
            );

            // Assert order details
            Assert.IsNotNull(order, "Order should not be null.");
            Assert.IsNotNull(order.OrderId, "Order ID should not be null.");
            Assert.AreEqual("BTC-USDC", order.ProductId, "Product ID should match.");
        });
    }




    [TestMethod]
    [Description("Test to verify that CreateStopLimitOrderGTCBuy creates a stop-limit order with GTC for BTC-USDC.")]
    public async Task Test_Orders_CreateStopLimitOrderGTCBuyAsync()
    {
        await ExecuteRateLimitedTest(async () =>
        {
            Assert.IsNotNull(_coinbaseClient, "Coinbase client should not be null.");
            var orderNumber = await _coinbaseClient!.Orders.CreateStopLimitOrderGTCAsync("BTC-USDC", OrderSide.Buy, 0.0001m, 72000.00m, 71900.00m);
            Assert.IsNotNull(orderNumber, "Order number should not be null.");
        });
    }


    [TestMethod]
    [Description("Test to verify that CreateStopLimitOrderGTCBuyReturningOrder creates and retrieves a stop-limit buy order object with GTC for BTC-USDC.")]
    public async Task Test_Orders_CreateStopLimitOrderGTCBuyReturningOrderAsync()
    {
        await ExecuteRateLimitedTest(async () =>
        {
            Assert.IsNotNull(_coinbaseClient, "Coinbase client should not be null.");

            // Create stop-limit buy order with GTC and retrieve order details
            Order order = await _coinbaseClient!.Orders.CreateStopLimitOrderGTCAsync(
                productId: "BTC-USDC",
                side: OrderSide.Buy,
                baseSize: 0.0001m,
                limitPrice: 72000.00m,
                stopPrice: 71900.00m,
                returnOrder: true
            );

            // Assert order details
            Assert.IsNotNull(order, "Order should not be null.");
            Assert.IsNotNull(order.OrderId, "Order ID should not be null.");
            Assert.AreEqual("BTC-USDC", order.ProductId, "Product ID should match.");
        });
    }



    [TestMethod]
    [Description("Test to verify that CreateStopLimitOrderGTCSell creates a stop-limit sell order with GTC for BTC-USDC.")]
    public async Task Test_Orders_CreateStopLimitOrderGTCSellAsync()
    {
        await ExecuteRateLimitedTest(async () =>
        {
            Assert.IsNotNull(_coinbaseClient, "Coinbase client should not be null.");
            var orderNumber = await _coinbaseClient!.Orders.CreateStopLimitOrderGTCAsync("BTC-USDC", OrderSide.Sell, 0.0001m, 40000.00m, 40500.00m);
            //var orderNumber = await _coinbaseClient!.Orders.CreateStopLimitOrderGTCAsync("BTC-USDC", OrderSide.SELL, "5", "43200.00", "43500.00");
            Assert.IsNotNull(orderNumber, "Order number should not be null.");
        });
    }


    [TestMethod]
    [Description("Test to verify that CreateStopLimitOrderGTCSellReturningOrder creates and retrieves a stop-limit sell order object with GTC for BTC-USDC.")]
    public async Task Test_Orders_CreateStopLimitOrderGTCSellReturningOrderAsync()
    {
        await ExecuteRateLimitedTest(async () =>
        {
            Assert.IsNotNull(_coinbaseClient, "Coinbase client should not be null.");

            // Create stop-limit sell order with GTC and retrieve order details
            Order order = await _coinbaseClient!.Orders.CreateStopLimitOrderGTCAsync(
                productId: "BTC-USDC",
                side: OrderSide.Sell,
                baseSize: 0.0001m,
                limitPrice: 40000.00m,
                stopPrice: 40500.00m,
                returnOrder: true
            );

            // Assert order details
            Assert.IsNotNull(order, "Order should not be null.");
            Assert.IsNotNull(order.OrderId, "Order ID should not be null.");
            Assert.AreEqual("BTC-USDC", order.ProductId, "Product ID should match.");
        });
    }



    [TestMethod]
    [Description("Test to verify that CreateStopLimitOrderGTDBuy creates a limit order for BTC-USDC.")]
    public async Task Test_Orders_CreateStopLimitOrderGTDBuyAsync()
    {
        await ExecuteRateLimitedTest(async () =>
        {
            Assert.IsNotNull(_coinbaseClient, "Coinbase client should not be null.");
            var orderNumber = await _coinbaseClient!.Orders.CreateStopLimitOrderGTDAsync(
                "BTC-USDC", OrderSide.Buy, 0.0001m, 72000.00m, 71900.00m, DateTime.UtcNow.AddDays(1));
            Assert.IsNotNull(orderNumber, "Order number should not be null.");
        });
    }


    [TestMethod]
    [Description("Test to verify that CreateStopLimitOrderGTDBuyReturningOrder creates and retrieves a stop-limit buy order object with GTD for BTC-USDC.")]
    public async Task Test_Orders_CreateStopLimitOrderGTDBuyReturningOrderAsync()
    {
        await ExecuteRateLimitedTest(async () =>
        {
            Assert.IsNotNull(_coinbaseClient, "Coinbase client should not be null.");

            // Create stop-limit buy order with GTD and retrieve order details
            Order order = await _coinbaseClient!.Orders.CreateStopLimitOrderGTDAsync(
                productId: "BTC-USDC",
                side: OrderSide.Buy,
                baseSize: 0.0001m,
                limitPrice: 72000.00m,
                stopPrice: 71900.00m,
                endTime: DateTime.UtcNow.AddDays(1),
                returnOrder: true
            );

            // Assert order details
            Assert.IsNotNull(order, "Order should not be null.");
            Assert.IsNotNull(order.OrderId, "Order ID should not be null.");
            Assert.AreEqual("BTC-USDC", order.ProductId, "Product ID should match.");
        });
    }


    [TestMethod]
    [Description("Test to verify that CreateStopLimitOrderGTDSell creates a stop-limit sell order for BTC-USDC.")]
    public async Task Test_Orders_CreateStopLimitOrderGTDSellAsync()
    {
        await ExecuteRateLimitedTest(async () =>
        {
            Assert.IsNotNull(_coinbaseClient, "Coinbase client should not be null.");
            var orderNumber = await _coinbaseClient!.Orders.CreateStopLimitOrderGTDAsync(
                "BTC-USDC", OrderSide.Sell, 0.0001m, 40000.00m, 40500.00m, DateTime.UtcNow.AddDays(1));
            Assert.IsNotNull(orderNumber, "Order number should not be null.");
        });
    }

    [TestMethod]
    [Description("Test to verify that CreateStopLimitOrderGTDSellReturningOrder creates and retrieves a stop-limit sell order object with GTD for BTC-USDC.")]
    public async Task Test_Orders_CreateStopLimitOrderGTDSellReturningOrderAsync()
    {
        await ExecuteRateLimitedTest(async () =>
        {
            Assert.IsNotNull(_coinbaseClient, "Coinbase client should not be null.");

            // Create stop-limit sell order with GTD and retrieve order details
            Order order = await _coinbaseClient!.Orders.CreateStopLimitOrderGTDAsync(
                productId: "BTC-USDC",
                side: OrderSide.Sell,
                baseSize: 0.0001m,
                limitPrice: 40000.00m,
                stopPrice: 40500.00m,
                endTime: DateTime.UtcNow.AddDays(1),
                returnOrder: true
            );

            // Assert order details
            Assert.IsNotNull(order, "Order should not be null.");
            Assert.IsNotNull(order.OrderId, "Order ID should not be null.");
            Assert.AreEqual("BTC-USDC", order.ProductId, "Product ID should match.");
        });
    }



    [TestMethod]
    [Description("Test to verify that CreateSORLimitIOCOrderBuyAsync creates a SOR limit IOC Buy order for BTC-USDC.")]
    public async Task Test_Orders_CreateSORLimitIOCOrderBuyAsync()
    {
        await ExecuteRateLimitedTest(async () =>
        {
            Assert.IsNotNull(_coinbaseClient, "Coinbase client should not be null.");
            var orderNumber = await _coinbaseClient!.Orders.CreateSORLimitIOCOrderAsync("BTC-USDC", OrderSide.Buy, 0.0001m, 65000.00m);
            Assert.IsNotNull(orderNumber, "Order number should not be null.");
        });
    }


    [TestMethod]
    [Description("Test to verify that CreateSORLimitIOCOrderBuyReturningOrder creates and retrieves a SOR limit IOC buy order object for BTC-USDC.")]
    public async Task Test_Orders_CreateSORLimitIOCOrderBuyReturningOrderAsync()
    {
        await ExecuteRateLimitedTest(async () =>
        {
            Assert.IsNotNull(_coinbaseClient, "Coinbase client should not be null.");

            // Create SOR limit IOC buy order and retrieve order details
            Order order = await _coinbaseClient!.Orders.CreateSORLimitIOCOrderAsync(
                productId: "BTC-USDC",
                side: OrderSide.Buy,
                baseSize: 0.0001m,
                limitPrice: 65000.00m,
                returnOrder: true
            );

            // Assert order details
            Assert.IsNotNull(order, "Order should not be null.");
            Assert.IsNotNull(order.OrderId, "Order ID should not be null.");
            Assert.AreEqual("BTC-USDC", order.ProductId, "Product ID should match.");
        });
    }



    [TestMethod]
    [Description("Test to verify that CreateSORLimitIOCOrderSellAsync creates a SOR limit IOC Sell order for BTC-USDC.")]
    public async Task Test_Orders_CreateSORLimitIOCOrderSellAsync()
    {
        await ExecuteRateLimitedTest(async () =>
        {
            Assert.IsNotNull(_coinbaseClient, "Coinbase client should not be null.");
            var orderNumber = await _coinbaseClient!.Orders.CreateSORLimitIOCOrderAsync("BTC-USDC", OrderSide.Sell, 0.0001m, 70000);
            Assert.IsNotNull(orderNumber, "Order number should not be null.");
        });
    }


    [TestMethod]
    [Description("Test to verify that CreateSORLimitIOCOrderSellReturningOrder creates and retrieves a SOR limit IOC sell order object for BTC-USDC.")]
    public async Task Test_Orders_CreateSORLimitIOCOrderSellReturningOrderAsync()
    {
        await ExecuteRateLimitedTest(async () =>
        {
            Assert.IsNotNull(_coinbaseClient, "Coinbase client should not be null.");

            // Create SOR limit IOC sell order and retrieve order details
            Order order = await _coinbaseClient!.Orders.CreateSORLimitIOCOrderAsync(
                productId: "BTC-USDC",
                side: OrderSide.Sell,
                baseSize: 0.0001m,
                limitPrice: 70000,
                returnOrder: true
            );

            // Assert order details
            Assert.IsNotNull(order, "Order should not be null.");
            Assert.IsNotNull(order.OrderId, "Order ID should not be null.");
            Assert.AreEqual("BTC-USDC", order.ProductId, "Product ID should match.");
        });
    }

}
