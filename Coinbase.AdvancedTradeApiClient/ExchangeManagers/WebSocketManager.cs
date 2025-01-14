using Coinbase.AdvancedTradeApiClient.Enums;
using Coinbase.AdvancedTradeApiClient.Models.WebSocket;
using Coinbase.AdvancedTradeApiClient.Models.WebSocket.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Coinbase.AdvancedTradeApiClient.ExchangeManagers;

/// <summary>
/// Manages WebSocket communication for the Coinbase Advanced Trade API.
/// </summary>
public sealed class WebSocketManager : IDisposable
{
    // WebSocket instance for managing the WebSocket connection.
    private readonly ClientWebSocket _webSocket = new ClientWebSocket();

    // The URI of the WebSocket server.
    private readonly Uri _webSocketUri;

    // The API key used for authentication.
    private readonly string _apiKey;

    // The API secret used for authentication.
    private readonly string _apiSecret;

    // Dictionary to map channel names to message processors.
    private readonly Dictionary<string, Action<string>> _messageMap = [];

    // Semaphore for controlling access to critical sections of the code.
    private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);

    // Flag to track whether the object has been disposed.
    private bool _disposed;

    // Property to check if the WebSocket connection is open.
    private bool IsWebSocketOpen => _webSocket.State == System.Net.WebSockets.WebSocketState.Open;

    // JSON serialization options for WebSocket messages.
    private static readonly JsonSerializerOptions JsonOptions = new JsonSerializerOptions
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    // Tracks the subscriptions
    private readonly HashSet<string> _subscriptions = [];

    /// <summary>
    /// Gets the current status of the WebSocket connection.
    /// </summary>
    public Enums.WebSocketState WebSocketState
    {
        get
        {
            switch (_webSocket.State)
            {
                case System.Net.WebSockets.WebSocketState.None:
                    return Enums.WebSocketState.None;
                case System.Net.WebSockets.WebSocketState.Connecting:
                    return Enums.WebSocketState.Connecting;
                case System.Net.WebSockets.WebSocketState.Open:
                    return Enums.WebSocketState.Open;
                case System.Net.WebSockets.WebSocketState.CloseSent:
                    return Enums.WebSocketState.CloseSent;
                case System.Net.WebSockets.WebSocketState.CloseReceived:
                    return Enums.WebSocketState.CloseReceived;
                case System.Net.WebSockets.WebSocketState.Closed:
                    return Enums.WebSocketState.Closed;
                case System.Net.WebSockets.WebSocketState.Aborted:
                    return Enums.WebSocketState.Aborted;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }

    /// <summary>
    /// Gets the current active subscriptions.
    /// </summary>
    public IEnumerable<string> Subscriptions => _subscriptions;

    // Buffer size for receiving messages.
    private readonly int _bufferSize;

    /// <summary>
    /// Initializes a new instance of the WebSocketManager class.
    /// </summary>
    /// <param name="webSocketUri">The URI of the WebSocket server.</param>
    /// <param name="apiKey">The API key used for authentication.</param>
    /// <param name="apiSecret">The API secret used for authentication.</param>
    /// <param name="bufferSize">The buffer size for receiving messages, in bytes (default is 5,242,880 bytes or 5MB).</param>
    public WebSocketManager(string webSocketUri, string apiKey, string apiSecret, int bufferSize = 5 * 1024 * 1024)
    {
        // Check for null or empty values and throw exceptions if necessary.
        if (string.IsNullOrWhiteSpace(webSocketUri)) throw new ArgumentNullException(nameof(webSocketUri));
        if (string.IsNullOrWhiteSpace(apiKey)) throw new ArgumentNullException(nameof(apiKey));
        if (string.IsNullOrWhiteSpace(apiSecret)) throw new ArgumentNullException(nameof(apiSecret));

        // Initialize fields with provided values.
        _webSocketUri = new Uri(webSocketUri);
        _apiKey = apiKey;
        _apiSecret = apiSecret;
        _bufferSize = bufferSize;

        // Initialize the message map, mapping channel names to message processors.
        _messageMap = new Dictionary<string, Action<string>>
        {
            ["candles"] = msg => ProcessInternalMessage<InternalCandleMessage, CandleMessage>(msg, CandleMessageReceived, (item) => item.ToModel()),
            ["heartbeats"] = msg => ProcessMessage(msg, HeartbeatMessageReceived),
            ["market_trades"] = msg => ProcessMessage(msg, MarketTradeMessageReceived),
            ["status"] = msg => ProcessMessage(msg, StatusMessageReceived),
            //["ticker"] = msg => ProcessMessage(msg, TickerMessageReceived),
            ["ticker"] = msg => ProcessInternalMessage<InternalTickerMessage, TickerMessage>(msg, TickerBatchMessageReceived, (item) => item.ToModel()),
            ["ticker_batch"] = msg => ProcessMessage(msg, TickerBatchMessageReceived),
            ["level2"] = msg => ProcessMessage(msg, Level2MessageReceived),
            ["user"] = msg => ProcessMessage(msg, UserMessageReceived)
        };
    }

    /// <summary>
    /// Gets the string representation of a channel type.
    /// </summary>
    /// <param name="channelType">The channel type to convert to a string.</param>
    /// <returns>The string representation of the channel type.</returns>
    public static string GetChannelString(ChannelType channelType)
    {
        // Use a switch statement to map the channel type to its string representation.
        return channelType switch
        {
            ChannelType.Candles => "candles",
            ChannelType.Matches => "match",
            ChannelType.Heartbeats => "heartbeats",
            ChannelType.MarketTrades => "market_trades",
            ChannelType.Status => "status",
            ChannelType.Ticker => "ticker",
            ChannelType.TickerBatch => "ticker_batch",
            ChannelType.Level2 => "level2",
            ChannelType.User => "user",
            _ => throw new ArgumentOutOfRangeException(nameof(channelType), channelType, "Invalid channel type provided."),
        };
    }

    /// <summary>
    /// Establishes a WebSocket connection asynchronously.
    /// </summary>
    public async ValueTask ConnectAsync()
    {
        // Acquire the semaphore to ensure exclusive access to this method.
        await _semaphore.WaitAsync().ConfigureAwait(false);

        try
        {
            // If the WebSocket is already open, return without doing anything.
            if (IsWebSocketOpen)
            {
                return;
            }

            // Attempt to establish the WebSocket connection to the specified URI.
            await _webSocket.ConnectAsync(_webSocketUri, CancellationToken.None).ConfigureAwait(false);

            // Start the background task to receive WebSocket messages (fire and forget).
            _ = ReceiveMessagesAsync().AsTask();
        }
        finally
        {
            // Always release the semaphore to allow other threads to access this method.
            _semaphore.Release();
        }
    }

    /// <summary>
    /// Closes the WebSocket connection asynchronously.
    /// </summary>
    public async ValueTask DisconnectAsync()
    {
        // Acquire the semaphore to ensure exclusive access to this method.
        await _semaphore.WaitAsync().ConfigureAwait(false);

        try
        {
            // Check if the WebSocket is open or in the process of closing.
            if (IsWebSocketOpen || _webSocket.State == System.Net.WebSockets.WebSocketState.CloseReceived)
            {
                // Close the WebSocket gracefully with a normal closure status.
                await _webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closing", CancellationToken.None).ConfigureAwait(false);
            }
        }
        finally
        {
            // Always release the semaphore to allow other threads to access this method.
            _semaphore.Release();
        }
    }

    /// <summary>
    /// Subscribes to a WebSocket channel asynchronously.
    /// </summary>
    /// <param name="products">An optional array of product IDs to subscribe to.</param>
    /// <param name="channelType">The type of channel to subscribe to.</param>
    public async ValueTask SubscribeAsync(string[] products, ChannelType channelType)
    {
        // Check if the provided channel type is valid.
        if (!Enum.IsDefined(typeof(ChannelType), channelType))
            throw new ArgumentException("Invalid channel type provided.", nameof(channelType));

        // Acquire the semaphore to ensure exclusive access to this method.
        await _semaphore.WaitAsync().ConfigureAwait(false);

        try
        {
            // Get the string representation of the channel type.
            string channelString = GetChannelString(channelType);

            // Subscribe to the specified channel asynchronously.
            await SubscribeToChannelAsync(products, channelString).ConfigureAwait(false);
        }
        finally
        {
            // Always release the semaphore to allow other threads to access this method.
            _semaphore.Release();
        }
    }

    /// <summary>
    /// Unsubscribes from a WebSocket channel asynchronously.
    /// </summary>
    /// <param name="products">An optional array of product IDs to unsubscribe from.</param>
    /// <param name="channelType">The type of channel to unsubscribe from.</param>
    public async ValueTask UnsubscribeAsync(string[] products, ChannelType channelType)
    {
        // Check if the provided channel type is valid.
        if (!Enum.IsDefined(typeof(ChannelType), channelType))
            throw new ArgumentException("Invalid channel type provided.", nameof(channelType));

        // Acquire the semaphore to ensure exclusive access to this method.
        await _semaphore.WaitAsync().ConfigureAwait(false);

        try
        {
            // Get the string representation of the channel type.
            string channelString = GetChannelString(channelType);

            // Unsubscribe from the specified channel asynchronously.
            await UnsubscribeFromChannelAsync(products, channelString).ConfigureAwait(false);

            // Remove from the subscription set
            _subscriptions.Remove(channelString);
        }
        finally
        {
            // Always release the semaphore to allow other threads to access this method.
            _semaphore.Release();
        }
    }

    /// <summary>
    /// Subscribes to a WebSocket channel with the specified products and channel name asynchronously.
    /// </summary>
    /// <param name="products">An optional array of product IDs to subscribe to.</param>
    /// <param name="channelName">The name of the channel to subscribe to.</param>
    private async ValueTask SubscribeToChannelAsync(string[] products, string channelName)
    {
        // Check if the channel name is null and throw an exception if it is.
        if (channelName == null) throw new ArgumentNullException(nameof(channelName));

        // If the WebSocket is not open, return without sending the subscribe message.
        if (!IsWebSocketOpen)
        {
            return;
        }

        // Create a subscription message based on the provided products and channel name.
        var message = CreateSubscriptionMessage(products, channelName, "subscribe");

        // Serialize the message to JSON format.
        var jsonString = JsonSerializer.Serialize(message, JsonOptions);

        // Convert the JSON message to a byte array for sending over the WebSocket.
        var byteData = Encoding.UTF8.GetBytes(jsonString);

        // Send the subscription message as a text message over the WebSocket.
        await _webSocket.SendAsync(new ArraySegment<byte>(byteData), WebSocketMessageType.Text, true, CancellationToken.None).ConfigureAwait(false);

        // Add to the subscription set
        _subscriptions.Add(channelName);
    }

    /// <summary>
    /// Unsubscribes from a WebSocket channel with the specified products and channel name asynchronously.
    /// </summary>
    /// <param name="products">An optional array of product IDs to unsubscribe from.</param>
    /// <param name="channelName">The name of the channel to unsubscribe from.</param>
    private async ValueTask UnsubscribeFromChannelAsync(string[] products, string channelName)
    {
        // Check if the channel name is null and throw an exception if it is.
        if (channelName == null) throw new ArgumentNullException(nameof(channelName));

        // If the WebSocket is not open, return without sending the unsubscribe message.
        if (!IsWebSocketOpen)
        {
            return;
        }

        // Create an unsubscribe message based on the provided products and channel name.
        var message = CreateSubscriptionMessage(products, channelName, "unsubscribe");

        // Serialize the message to JSON format.
        var jsonString = JsonSerializer.Serialize(message, JsonOptions);

        // Convert the JSON message to a byte array for sending over the WebSocket.
        var byteData = Encoding.UTF8.GetBytes(jsonString);

        // Send the unsubscribe message as a text message over the WebSocket.
        await _webSocket.SendAsync(new ArraySegment<byte>(byteData), WebSocketMessageType.Text, true, CancellationToken.None).ConfigureAwait(false);

        // Remove from the subscription set
        _subscriptions.Remove(channelName);
    }

    /// <summary>
    /// Creates a subscription message object for WebSocket communication.
    /// The message object is structured differently based on the type of API key used.
    /// </summary>
    /// <param name="products">An optional array of product IDs to include in the subscription.</param>
    /// <param name="channelName">The name of the channel to subscribe to or unsubscribe from.</param>
    /// <param name="type">The type of the subscription message (e.g., 'subscribe' or 'unsubscribe').</param>
    /// <returns>A subscription message object in JSON format.</returns>
    /// <exception cref="ArgumentNullException">Thrown if channelName or type is null or empty.</exception>
    private object CreateSubscriptionMessage(string[] products, string channelName, string type)
    {
        // Validate parameters to ensure they are not null or empty
        if (string.IsNullOrWhiteSpace(channelName))
            throw new ArgumentNullException(nameof(channelName));
        if (string.IsNullOrWhiteSpace(type))
            throw new ArgumentNullException(nameof(type));

        // Get the current timestamp in Unix time seconds format
        var timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString();

        // Initialize the base message structure
        var message = new Dictionary<string, object>
        {
            {"type", type},
            {"product_ids", products},
            {"channel", channelName},
            {"api_key", _apiKey},
            {"timestamp", timestamp}
        };

        var jwt = JwtTokenGenerator.GenerateToken(_apiKey, _apiSecret, "public_websocket_api");
        message.Add("jwt", jwt);

        // Return the constructed message
        return message;
    }

    /// <summary>
    /// Processes a WebSocket message by deserializing it into the specified type and invoking the associated event handler.
    /// </summary>
    /// <typeparam name="T">The type to deserialize the message into.</typeparam>
    /// <param name="message">The WebSocket message to process.</param>
    /// <param name="eventInvoker">The event handler to invoke after deserialization.</param>
    private void ProcessMessage<T>(string message, EventHandler<WebSocketMessageEventArgs<T>> eventInvoker)
    {
        // Check if the message is null or empty and throw an exception if it is.
        if (string.IsNullOrWhiteSpace(message)) throw new ArgumentNullException(nameof(message));

        // Deserialize the WebSocket message into the specified type.
        var deserializedMessage = JsonSerializer.Deserialize<T>(message);

        // Check if the deserialized message is not null and if an event handler is provided.
        if (deserializedMessage != null && eventInvoker != null)
        {
            // Invoke the event handler with the deserialized message as an argument.
            eventInvoker(this, new WebSocketMessageEventArgs<T>(deserializedMessage));
        }
    }

    /// <summary>
    /// Processes a WebSocket message by deserializing it into the specified type and invoking the associated event handler.
    /// </summary>
    /// <typeparam name="TInternal"></typeparam>
    /// <typeparam name="TPublic"></typeparam>
    /// <param name="message"></param>
    /// <param name="eventInvoker"></param>
    /// <param name="converter"></param>
    /// <exception cref="ArgumentNullException"></exception>
    private void ProcessInternalMessage<TInternal, TPublic>(string message, EventHandler<WebSocketMessageEventArgs<TPublic>> eventInvoker, Func<TInternal, TPublic> converter)
    {
        // Check if the message is null or empty and throw an exception if it is.
        if (string.IsNullOrWhiteSpace(message)) throw new ArgumentNullException(nameof(message));

        // Deserialize the WebSocket message into the specified type.
        var internalMessage = JsonSerializer.Deserialize<TInternal>(message);

        // Check if the deserialized message is not null and if an event handler is provided.
        if (internalMessage != null && eventInvoker != null)
        {
            // Invoke the event handler with the deserialized message as an argument.
            eventInvoker(this, new WebSocketMessageEventArgs<TPublic>(converter(internalMessage)));
        }
    }

    /// <summary>
    /// Processes a WebSocket message by deserializing it into a JSON element and routing it to the appropriate message processor.
    /// </summary>
    /// <param name="message">The WebSocket message to process.</param>
    private void ProcessMessage(string message)
    {
        // Check if the message is null or empty and throw an exception if it is.
        if (string.IsNullOrWhiteSpace(message)) throw new ArgumentNullException(nameof(message));

        var jsonObject = JsonSerializer.Deserialize<JsonElement>(message);

        if (jsonObject.TryGetProperty("channel", out var channelToken)
            && channelToken.ValueKind == JsonValueKind.String
            && _messageMap.TryGetValue(channelToken.GetString(), out var processor))
        {
            // If a message processor is found for the channel, invoke it with the original message.
            processor(message);
        }
    }

    /// <summary>
    /// Asynchronously receives WebSocket messages and processes them.
    /// </summary>
    private async ValueTask ReceiveMessagesAsync()
    {
        // Check if the WebSocket is not open, and if not, return immediately.
        if (!IsWebSocketOpen)
        {
            return;
        }

        var buffer = new byte[_bufferSize];
        var messageSegments = new List<ArraySegment<byte>>();

        while (IsWebSocketOpen)
        {
            // Receive a WebSocket message into the buffer.
            var result = await _webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None).ConfigureAwait(false);

            // Add the received message segment to the list.
            messageSegments.Add(new ArraySegment<byte>(buffer, 0, result.Count));

            // Check if the received message is of type WebSocketMessageType.Close, indicating the WebSocket is closing.
            if (result.MessageType == WebSocketMessageType.Close)
            {
                break;
            }

            // Check if this is the end of a complete message.
            if (result.EndOfMessage)
            {
                // Calculate the total bytes received by summing up the lengths of all segments.
                var totalBytes = messageSegments.Sum(segment => segment.Count);
                var messageBuffer = new byte[totalBytes];
                var offset = 0;

                foreach (var segment in messageSegments)
                {
                    if (segment.Array is null)
                    {
                        throw new InvalidOperationException("ArraySegment does not have an underlying array.");
                    }

                    // Copy the data from each segment into the complete message buffer.
                    Buffer.BlockCopy(segment.Array, segment.Offset, messageBuffer, offset, segment.Count);
                    offset += segment.Count;
                }

                // Invoke the MessageReceived event with the complete message data.
                MessageReceived?.Invoke(this, new MessageEventArgs(new ArraySegment<byte>(messageBuffer, 0, totalBytes)));

                // Convert the message data to a string.
                var stringData = Encoding.UTF8.GetString(messageBuffer);

                // Check if the string data is not empty or null, and then process it.
                if (!string.IsNullOrWhiteSpace(stringData))
                {
                    ProcessMessage(stringData);
                }

                // Clear the message segments list to prepare for the next message.
                messageSegments.Clear();
            }
        }
    }



    /// <summary>
    /// Event raised when a WebSocket message is received.
    /// </summary>
    public event EventHandler<MessageEventArgs> MessageReceived;

    /// <summary>
    /// Event raised when a WebSocket message of type <see cref="CandleMessage"/> is received.
    /// </summary>
    public event EventHandler<WebSocketMessageEventArgs<CandleMessage>> CandleMessageReceived;


    /// <summary>
    /// Event raised when a WebSocket message of type <see cref="HeartbeatMessage"/> is received.
    /// </summary>
    public event EventHandler<WebSocketMessageEventArgs<HeartbeatMessage>> HeartbeatMessageReceived;

    /// <summary>
    /// Event raised when a WebSocket message of type <see cref="MarketTradesMessage"/> is received.
    /// </summary>
    public event EventHandler<WebSocketMessageEventArgs<MarketTradesMessage>> MarketTradeMessageReceived;

    /// <summary>
    /// Event raised when a WebSocket message of type <see cref="StatusMessage"/> is received.
    /// </summary>
    public event EventHandler<WebSocketMessageEventArgs<StatusMessage>> StatusMessageReceived;

    /// <summary>
    /// Event raised when a WebSocket message of type <see cref="TickerMessage"/> is received.
    /// </summary>
    public event EventHandler<WebSocketMessageEventArgs<TickerMessage>> TickerMessageReceived;

    /// <summary>
    /// Event raised when a WebSocket message of type <see cref="TickerMessage"/> is received in batch.
    /// </summary>
    public event EventHandler<WebSocketMessageEventArgs<TickerMessage>> TickerBatchMessageReceived;

    /// <summary>
    /// Event raised when a WebSocket message of type <see cref="Level2Message"/> is received.
    /// </summary>
    public event EventHandler<WebSocketMessageEventArgs<Level2Message>> Level2MessageReceived;

    /// <summary>
    /// Event raised when a WebSocket message of type <see cref="UserMessage"/> is received.
    /// </summary>
    public event EventHandler<WebSocketMessageEventArgs<UserMessage>> UserMessageReceived;

    /// <summary>
    /// Disposes of the WebSocketManager instance.
    /// </summary>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Disposes of the WebSocketManager instance.
    /// </summary>
    /// <param name="disposing">True if disposing of managed resources, false if disposing of unmanaged resources only.</param>
    private void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _webSocket.Dispose();
            }

            _disposed = true;
        }
    }

    /// <summary>
    /// Finalizer for the WebSocketManager class.
    /// </summary>
    ~WebSocketManager()
    {
        Dispose(false); // Invokes the Dispose method to release unmanaged resources.
    }
}

/// <summary>
/// Represents an event argument for WebSocket messages with a strongly-typed message property.
/// </summary>
/// <typeparam name="T">The type of the WebSocket message.</typeparam>
public class WebSocketMessageEventArgs<T> : EventArgs
{
    /// <summary>
    /// Gets the WebSocket message.
    /// </summary>
    public T Message { get; }

    /// <summary>
    /// Initializes a new instance of the WebSocketMessageEventArgs class with a WebSocket message.
    /// </summary>
    /// <param name="message">The WebSocket message.</param>
    public WebSocketMessageEventArgs(T message)
    {
        if (message == null)
        {
            throw new ArgumentNullException(nameof(message));
        }

        Message = message;
    }
}

/// <summary>
/// Represents an event argument for WebSocket raw message data.
/// </summary>
public class MessageEventArgs : EventArgs
{
    /// <summary>
    /// Gets the raw data of the WebSocket message.
    /// </summary>
    public ArraySegment<byte> RawData { get; }

    /// <summary>
    /// Gets a string representation of the WebSocket message data.
    /// </summary>
    public string StringData
        => Encoding.UTF8.GetString(RawData.Array ?? Array.Empty<byte>(), RawData.Offset, RawData.Count);

    /// <summary>
    /// Initializes a new instance of the MessageEventArgs class with raw message data.
    /// </summary>
    /// <param name="data">The raw data of the WebSocket message.</param>
    public MessageEventArgs(ArraySegment<byte> data)
    {
        RawData = data;
    }
}
