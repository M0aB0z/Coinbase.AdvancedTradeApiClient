// Coinbase Cloud Trading Keys
using Coinbase.AdvancedTradeApiClient;
using Coinbase.AdvancedTradeApiClient.Enums;

var apiKey = Environment.GetEnvironmentVariable("COINBASE_CLOUD_TRADING_API_KEY", EnvironmentVariableTarget.User)
             ?? throw new InvalidOperationException("API Key not found");
var apiSecret = Environment.GetEnvironmentVariable("COINBASE_CLOUD_TRADING_API_SECRET", EnvironmentVariableTarget.User)
               ?? throw new InvalidOperationException("API Secret not found");
var coinbaseClient = new CoinbaseClient(apiKey, apiSecret);

var start = new DateTime(2023, 07, 01, 00, 00, 00);
var end = new DateTime(2023, 07, 01, 02, 00, 00);

Console.WriteLine("Retrieving candles...");

var candles = await coinbaseClient.Products.GetProductCandlesAsync("BTC-USDC", start, end, Granularity.ONE_MINUTE, CancellationToken.None);

Console.WriteLine(candles.Count + " candles retrieved");
Console.ReadLine();