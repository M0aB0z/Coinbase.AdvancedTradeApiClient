// Coinbase Cloud Trading Keys
using Coinbase.AdvancedTradeApiClient;
using System.Threading;

var apiKey = Environment.GetEnvironmentVariable("COINBASE_CLOUD_TRADING_API_KEY", EnvironmentVariableTarget.User)
             ?? throw new InvalidOperationException("API Key not found");
var apiSecret = Environment.GetEnvironmentVariable("COINBASE_CLOUD_TRADING_API_SECRET", EnvironmentVariableTarget.User)
               ?? throw new InvalidOperationException("API Secret not found");
var coinbaseClient = new CoinbaseClient(apiKey, apiSecret);

Console.WriteLine("Retrieving transactions summary...");

var transactionsSummary = await coinbaseClient.Fees.GetTransactionsSummaryAsync(DateTime.UtcNow.AddDays(-30), DateTime.UtcNow, "USD", "SPOT", CancellationToken.None);


Console.ReadLine();
