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


WebSocketManager? webSocketManager = coinbaseClient.WebSocket;

AppDomain.CurrentDomain.ProcessExit += async (s, e) => await CleanupAsync(webSocketManager);
Console.CancelKeyPress += async (s, e) =>
{
    e.Cancel = true;  // Prevent the process from terminating immediately
    await CleanupAsync(webSocketManager);
};

webSocketManager.CandleMessageReceived += (sender, candleData) =>
{
    Console.WriteLine($"Received candle data at {DateTime.UtcNow} [{candleData.Message.Events.Last().Candles.Last()}]");
};

try
{
    Console.WriteLine("Connecting to the WebSocket...");
    await webSocketManager.ConnectAsync(CancellationToken.None);

    var state = webSocketManager.WebSocketState;

    Console.WriteLine("Subscribing to candles...");
    await webSocketManager.SubscribeAsync(["BTC-USDC", "DOGE-USDC"], ChannelType.Candles, CancellationToken.None);

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
    await webSocketManager!.UnsubscribeAsync(["BTC-USDC", "DOGE-USDC"], ChannelType.Candles, CancellationToken.None);

    Console.WriteLine("Disconnecting...");
    await webSocketManager.DisconnectAsync(CancellationToken.None);

    _isCleanupDone = true;  // Set the flag to indicate cleanup has been done
}