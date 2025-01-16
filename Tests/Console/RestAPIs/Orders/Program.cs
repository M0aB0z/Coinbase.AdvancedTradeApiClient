// Coinbase Cloud Trading Keys
using Coinbase.AdvancedTradeApiClient;
using Coinbase.AdvancedTradeApiClient.Enums;

var apiKey = Environment.GetEnvironmentVariable("COINBASE_CLOUD_TRADING_API_KEY", EnvironmentVariableTarget.User)
             ?? throw new InvalidOperationException("API Key not found");
var apiSecret = Environment.GetEnvironmentVariable("COINBASE_CLOUD_TRADING_API_SECRET", EnvironmentVariableTarget.User)
               ?? throw new InvalidOperationException("API Secret not found");
var coinbaseClient = new CoinbaseClient(apiKey, apiSecret);

Console.WriteLine("Retrieving all orders...");
var orders = await coinbaseClient.Orders.ListOrdersAsync("DOGE-USDC", [OrderStatus.FILLED]);
Console.WriteLine(orders.Count + " orders retrieved");

Console.WriteLine("Retrieving all filled orders...");
var filledOrders = await coinbaseClient.Orders.ListFillsAsync();
Console.WriteLine(filledOrders.Count + " filled orders retrieved");

Console.ReadLine();
