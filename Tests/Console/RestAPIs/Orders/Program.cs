// Coinbase Cloud Trading Keys
using Coinbase.AdvancedTradeApiClient;
using Coinbase.AdvancedTradeApiClient.Enums;
using Coinbase.AdvancedTradeApiClient.Models.Queries;

var apiKey = Environment.GetEnvironmentVariable("CB_API_KEY_ALBUS", EnvironmentVariableTarget.User)
             ?? throw new InvalidOperationException("API Key not found");
var apiSecret = Environment.GetEnvironmentVariable("CB_API_SECRET_ALBUS", EnvironmentVariableTarget.User)
               ?? throw new InvalidOperationException("API Secret not found");
var coinbaseClient = new CoinbaseClient(apiKey, apiSecret);

Console.WriteLine("Retrieving all orders...");
var paginatedOrders = await coinbaseClient.Orders.ListOrdersAsync(new OrderQueryFilter
{
    OrderStatus = [OrderStatus.FILLED],
    Limit = 100,
    //Cursor = "2",
    SortingType = OrderSortingType.LastFillTime,
});
Console.WriteLine(paginatedOrders.orders.Count + " orders retrieved");

//Console.WriteLine("Retrieving all filled orders...");
//var filledOrders = await coinbaseClient.Orders.ListFillsAsync(null, "SOL-USDC");
//Console.WriteLine(filledOrders.Count + " filled orders retrieved");


//var orderCreationOpe = await coinbaseClient.Orders.CreateLimitOrderGTCAsync("DOGE-USDC", OrderSide.Buy, 1m, 0.30m, false, CancellationToken.None);

Console.ReadLine();
