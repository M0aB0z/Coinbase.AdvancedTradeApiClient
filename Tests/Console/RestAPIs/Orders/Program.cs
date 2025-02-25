// Coinbase Cloud Trading Keys
using Coinbase.AdvancedTradeApiClient;
using Coinbase.AdvancedTradeApiClient.Enums;

var apiKey = Environment.GetEnvironmentVariable("CB_API_KEY_ALBUS", EnvironmentVariableTarget.User)
             ?? throw new InvalidOperationException("API Key not found");
var apiSecret = Environment.GetEnvironmentVariable("CB_API_SECRET_ALBUS", EnvironmentVariableTarget.User)
               ?? throw new InvalidOperationException("API Secret not found");
var coinbaseClient = new CoinbaseClient(apiKey, apiSecret);

Console.WriteLine("Retrieving all orders...");
var orders = await coinbaseClient.Orders.ListOrdersAsync("DOGE-USDC", [OrderStatus.FILLED]);
Console.WriteLine(orders.Count + " orders retrieved");

//Console.WriteLine("Retrieving all filled orders...");
//var filledOrders = await coinbaseClient.Orders.ListFillsAsync(null, "SOL-USDC");
//Console.WriteLine(filledOrders.Count + " filled orders retrieved");


//var orderCreationOpe = await coinbaseClient.Orders.CreateLimitOrderGTCAsync("DOGE-USDC", OrderSide.Buy, 1m, 0.30m, false, CancellationToken.None);

Console.ReadLine();
