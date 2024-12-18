using Coinbase.AdvancedTradeApiClient;
using Coinbase.AdvancedTradeApiClient.Enums;
using Coinbase.AdvancedTradeApiClient.ExchangeManagers;
bool _isCleanupDone = false;

// Coinbase Cloud Trading Keys
var apiKey = Environment.GetEnvironmentVariable("COINBASE_CLOUD_TRADING_API_KEY", EnvironmentVariableTarget.User)
             ?? throw new InvalidOperationException("API Key not found");
var apiSecret = Environment.GetEnvironmentVariable("COINBASE_CLOUD_TRADING_API_SECRET", EnvironmentVariableTarget.User)
               ?? throw new InvalidOperationException("API Secret not found");
var coinbaseClient = new CoinbaseClient(apiKey, apiSecret);

var start = new DateTime(2024, 11, 01, 00, 00, 00);
var end = new DateTime(2024, 11, 01, 01, 00, 00);

var candles = await coinbaseClient.Products.GetProductCandlesAsync("BTC-USDC", start, end, Granularity.ONE_MINUTE, CancellationToken.None);
var accountsPage = await coinbaseClient.Accounts.ListAccountsAsync(10);
var allAccounts = await coinbaseClient.Accounts.ListAllAccountsAsync();
var orders = await coinbaseClient.Orders.ListOrdersAsync();
var products = await coinbaseClient.Products.ListProductsAsync();
var trades = await coinbaseClient.Products.GetMarketTradesAsync("BTC-USDC", 50, CancellationToken.None);
var product = await coinbaseClient.Products.GetProductAsync("BTC-USDC", CancellationToken.None);

WebSocketManager? webSocketManager = coinbaseClient.WebSocket;

AppDomain.CurrentDomain.ProcessExit += async (s, e) => await CleanupAsync(webSocketManager);
Console.CancelKeyPress += async (s, e) =>
{
    e.Cancel = true;  // Prevent the process from terminating immediately
    await CleanupAsync(webSocketManager);
};

webSocketManager!.CandleMessageReceived += (sender, candleData) =>
{
    Console.WriteLine($"Received new data at {DateTime.UtcNow}");
};

webSocketManager.MessageReceived += (sender, e) =>
{
    Console.WriteLine($"Raw message received at {DateTime.UtcNow}: {e.StringData}");
};

try
{
    Console.WriteLine("Connecting to the WebSocket...");
    await webSocketManager.ConnectAsync();

    Console.WriteLine("Subscribing to candles...");
    await webSocketManager.SubscribeAsync(["BTC-USDC"], ChannelType.Candles);

    Console.WriteLine("Press any key to unsubscribe and exit.");
    Console.ReadKey();
}
catch (Exception ex)
{
    Console.WriteLine($"An error occurred: {ex.Message}");
}
finally
{
    if (!_isCleanupDone)
    {
        await CleanupAsync(webSocketManager);
    }
}

async Task CleanupAsync(WebSocketManager? webSocketManager)
{
    if (_isCleanupDone) return;  // Return immediately if cleanup has been done

    Console.WriteLine("Unsubscribing...");
    await webSocketManager!.UnsubscribeAsync(["BTC-USDC"], ChannelType.Candles);

    Console.WriteLine("Disconnecting...");
    await webSocketManager.DisconnectAsync();

    _isCleanupDone = true;  // Set the flag to indicate cleanup has been done
}