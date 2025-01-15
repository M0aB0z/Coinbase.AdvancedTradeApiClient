// Coinbase Cloud Trading Keys
using Coinbase.AdvancedTradeApiClient;

var apiKey = Environment.GetEnvironmentVariable("COINBASE_CLOUD_TRADING_API_KEY", EnvironmentVariableTarget.User)
             ?? throw new InvalidOperationException("API Key not found");
var apiSecret = Environment.GetEnvironmentVariable("COINBASE_CLOUD_TRADING_API_SECRET", EnvironmentVariableTarget.User)
               ?? throw new InvalidOperationException("API Secret not found");
var coinbaseClient = new CoinbaseClient(apiKey, apiSecret);

Console.WriteLine("Retrieving 50 BTC-USDC trades...");
var trades = await coinbaseClient.Products.GetMarketTradesAsync("BTC-USDC", 50, CancellationToken.None);
Console.WriteLine(trades.Trades.Count + " trades retrieved with best bid " + trades.BestBid + " and best ask " + trades.BestAsk);

Console.ReadLine();
