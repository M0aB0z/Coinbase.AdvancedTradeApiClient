using Coinbase.AdvancedTradeApiClient;
using Coinbase.AdvancedTradeApiClient.Enums;
using Coinbase.AdvancedTradeApiClient.ExchangeManagers;

class Program
{
    private static bool _isCleanupDone = false;
    private static CoinbaseClient? coinbaseClient = null;
    private static WebSocketManager? webSocketManager = null;
    private static string? apiKey;
    private static string? apiSecret;

    static async Task Main(string[] args)
    {
        // Coinbase Developer Platform Keys
        apiKey = Environment.GetEnvironmentVariable("COINBASE_CLOUD_TRADING_API_KEY", EnvironmentVariableTarget.User)
                 ?? throw new InvalidOperationException("API Key not found");
        apiSecret = Environment.GetEnvironmentVariable("COINBASE_CLOUD_TRADING_API_SECRET", EnvironmentVariableTarget.User)
                   ?? throw new InvalidOperationException("API Secret not found");

        // Initialize the Coinbase client and WebSocket manager
        coinbaseClient = new CoinbaseClient(apiKey: apiKey, apiSecret: apiSecret);

        webSocketManager = coinbaseClient.WebSocket;

        // Handle process exit and Ctrl+C events to ensure cleanup
        AppDomain.CurrentDomain.ProcessExit += async (s, e) => await CleanupAsync();
        Console.CancelKeyPress += async (s, e) =>
        {
            e.Cancel = true;  // Prevent the process from terminating immediately
            await CleanupAsync();
        };

        // Subscribe to WebSocket events
        SubscribeToWebSocketEvents(webSocketManager);

        try
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Connecting to the WebSocket...");
            await webSocketManager.ConnectAsync(CancellationToken.None);

            // Subscribe to necessary channels
            await SubscribeToChannelsAsync();

            var statusTimer = new System.Timers.Timer(2000);
            statusTimer.Elapsed += (e, a) =>
            {
                LogWebSocketStatus();
            };
            statusTimer.Enabled = true;

            while (true)
            {
                Console.WriteLine($"Press a key to disconnect");
                Console.ReadKey();
                await webSocketManager!.InternalDisconnect(CancellationToken.None);
            }
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"An error occurred: {ex.Message}");
            Console.ResetColor();
        }
        finally
        {
            if (!_isCleanupDone)
            {
                await CleanupAsync();
            }
        }
    }

    /// <summary>
    /// Performs cleanup actions like unsubscribing and disconnecting WebSocket.
    /// </summary>
    private static async Task CleanupAsync()
    {
        if (_isCleanupDone) return;

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("Unsubscribing from channels...");
        await webSocketManager!.UnsubscribeAsync(["BTC-USDC"], ChannelType.Heartbeats, CancellationToken.None);
        await webSocketManager.UnsubscribeAsync(["BTC-USDC"], ChannelType.Candles, CancellationToken.None);
        await webSocketManager.DisconnectAsync(CancellationToken.None);
        Console.ResetColor();

        webSocketManager.Dispose();
        _isCleanupDone = true;
    }

    /// <summary>
    /// Subscribes to WebSocket events like heartbeat and candle messages.
    /// </summary>
    private static void SubscribeToWebSocketEvents(WebSocketManager webSocketManager)
    {
        webSocketManager.HeartbeatMessageReceived += (sender, heartbeatData) =>
        {
            Console.WriteLine($"Received heartbeat at {DateTime.UtcNow}  {heartbeatData.Message.Events.Last().CurrentTime}");
        };
        webSocketManager.CandleMessageReceived += (sender, candleData) =>
        {
            Console.WriteLine($"Received candle data at {DateTime.UtcNow}  {candleData.Message.Events.Last().Candles.Last().StartDate.ToString("HH-mm-ss")}");
        };
    }

    /// <summary>
    /// Subscribes to required channels like Heartbeats and Candles.
    /// </summary>
    private static async Task SubscribeToChannelsAsync()
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("Subscribing to channels...");

        await webSocketManager!.SubscribeAsync(["BTC-USDC"], ChannelType.Heartbeats, CancellationToken.None);
        await webSocketManager!.SubscribeAsync(["BTC-USDC"], ChannelType.Candles, CancellationToken.None);

        LogWebSocketStatus();
        Console.ResetColor();
    }

    /// <summary>
    /// Logs the current WebSocket status and subscriptions.
    /// </summary>
    private static void LogWebSocketStatus()
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"WebSocket connection state: {webSocketManager!.WebSocketState}");
        Console.WriteLine("Current subscriptions: " + string.Join(", ", webSocketManager.Subscriptions));
        Console.ResetColor();
    }
}