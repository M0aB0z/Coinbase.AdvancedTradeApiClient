﻿using Coinbase.AdvancedTradeApiClient.Enums;
using Coinbase.AdvancedTradeApiClient.ExchangeManagers;

bool _isCleanupDone = false;

// Coinbase Developer Platform Keys
var apiKey = Environment.GetEnvironmentVariable("COINBASE_CLOUD_TRADING_API_KEY", EnvironmentVariableTarget.User)
             ?? throw new InvalidOperationException("API Key not found");
var apiSecret = Environment.GetEnvironmentVariable("COINBASE_CLOUD_TRADING_API_SECRET", EnvironmentVariableTarget.User)
               ?? throw new InvalidOperationException("API Secret not found");
var coinbaseClient = new CoinbaseClient(apiKey, apiSecret);


// Coinbase Legacy Keys
//var apiKey = Environment.GetEnvironmentVariable("COINBASE_LEGACY_API_KEY", EnvironmentVariableTarget.User)
//         ?? throw new InvalidOperationException("API Key not found");
//var apiSecret = Environment.GetEnvironmentVariable("COINBASE_LEGACY_API_SECRET", EnvironmentVariableTarget.User)
//           ?? throw new InvalidOperationException("API Secret not found");
//var coinbaseClient = new CoinbaseClient(apiKey: apiKey, apiSecret: apiSecret, apiKeyType: ApiKeyType.Legacy);

WebSocketManager? webSocketManager = coinbaseClient.WebSocket;

AppDomain.CurrentDomain.ProcessExit += async (s, e) => await CleanupAsync(webSocketManager);
Console.CancelKeyPress += async (s, e) =>
{
    e.Cancel = true;  // Prevent the process from terminating immediately
    await CleanupAsync(webSocketManager);
};

webSocketManager!.TickerMessageReceived += (sender, tickerData) =>
{
    Console.WriteLine($"Received ticker data at {DateTime.UtcNow}");
};

webSocketManager.MessageReceived += (sender, e) =>
{
    Console.WriteLine($"Raw message received at {DateTime.UtcNow}: {e.StringData}");
};

try
{
    Console.WriteLine("Connecting to the WebSocket...");
    await webSocketManager.ConnectAsync();

    Console.WriteLine("Subscribing to ticker...");
    await webSocketManager.SubscribeAsync(["BTC-USDC"], ChannelType.Ticker);

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

    Console.WriteLine("Unsubscribing from ticker...");
    await webSocketManager!.UnsubscribeAsync(["BTC-USDC"], ChannelType.Ticker);

    Console.WriteLine("Disconnecting...");
    await webSocketManager.DisconnectAsync();

    _isCleanupDone = true;  // Set the flag to indicate cleanup has been done
}
