﻿using Coinbase.AdvancedTradeApiClient;
using Coinbase.AdvancedTradeApiClient.Enums;
using Coinbase.AdvancedTradeApiClient.ExchangeManagers;

bool _isCleanupDone = false;

// Coinbase Developer Platform Keys
var apiKey = Environment.GetEnvironmentVariable("COINBASE_CLOUD_TRADING_API_KEY", EnvironmentVariableTarget.User)
             ?? throw new InvalidOperationException("API Key not found");
var apiSecret = Environment.GetEnvironmentVariable("COINBASE_CLOUD_TRADING_API_SECRET", EnvironmentVariableTarget.User)
               ?? throw new InvalidOperationException("API Secret not found");

var buffer10MegaBytes = 10 * 1024 * 1024; // 10 MB

var coinbaseClient = new CoinbaseClient(apiKey, apiSecret, buffer10MegaBytes);

WebSocketManager? webSocketManager = coinbaseClient.WebSocket;

AppDomain.CurrentDomain.ProcessExit += async (s, e) => await CleanupAsync(webSocketManager);
Console.CancelKeyPress += async (s, e) =>
{
    e.Cancel = true;  // Prevent the process from terminating immediately
    await CleanupAsync(webSocketManager);
};

webSocketManager!.Level2MessageReceived += (sender, level2Data) =>
{
    Console.WriteLine($"Received Level 2 data at {DateTime.UtcNow}");
};

//webSocketManager.MessageReceived += (sender, e) =>
//{
//    Console.WriteLine($"Raw message received at {DateTime.UtcNow}: {e.StringData}");
//};

try
{
    Console.WriteLine("Connecting to the WebSocket...");
    await webSocketManager.ConnectAsync(CancellationToken.None);

    Console.WriteLine("Subscribing to level 2...");
    await webSocketManager.SubscribeAsync(["BTC-USDC"], ChannelType.Level2, CancellationToken.None);

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

    Console.WriteLine("Unsubscribing from level 2...");
    await webSocketManager!.UnsubscribeAsync(["BTC-USDC"], ChannelType.Level2, CancellationToken.None);

    Console.WriteLine("Disconnecting...");
    await webSocketManager.DisconnectAsync(CancellationToken.None);

    _isCleanupDone = true;  // Set the flag to indicate cleanup has been done
}
