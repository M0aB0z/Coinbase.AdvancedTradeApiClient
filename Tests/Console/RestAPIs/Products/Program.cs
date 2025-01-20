// Coinbase Cloud Trading Keys
using Coinbase.AdvancedTradeApiClient;
using System.Threading;

var apiKey = Environment.GetEnvironmentVariable("COINBASE_CLOUD_TRADING_API_KEY", EnvironmentVariableTarget.User)
             ?? throw new InvalidOperationException("API Key not found");
var apiSecret = Environment.GetEnvironmentVariable("COINBASE_CLOUD_TRADING_API_SECRET", EnvironmentVariableTarget.User)
               ?? throw new InvalidOperationException("API Secret not found");
var coinbaseClient = new CoinbaseClient(apiKey, apiSecret);

var productsSpot = await coinbaseClient.Products.ListProductsAsync("SPOT", CancellationToken.None);

Console.WriteLine("Retrieving product BTC-USDC...");
var product = await coinbaseClient.Products.GetProductAsync("BTC-USDC", CancellationToken.None);
Console.WriteLine("Product " + product.Alias + " retrieved");

Console.WriteLine("Retrieving all products...");
var products = await coinbaseClient.Products.ListProductsAsync();
Console.WriteLine(products.Count + " products retrieved");

Console.ReadLine();
