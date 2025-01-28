// Coinbase Cloud Trading Keys
using Coinbase.AdvancedTradeApiClient;
using Coinbase.AdvancedTradeApiClient.Enums;

var apiKey = Environment.GetEnvironmentVariable("COINBASE_CLOUD_TRADING_API_KEY", EnvironmentVariableTarget.User)
             ?? throw new InvalidOperationException("API Key not found");
var apiSecret = Environment.GetEnvironmentVariable("COINBASE_CLOUD_TRADING_API_SECRET", EnvironmentVariableTarget.User)
               ?? throw new InvalidOperationException("API Secret not found");
var coinbaseClient = new CoinbaseClient(apiKey, apiSecret);

Console.WriteLine("Retrieving transactions summary...");

Coinbase.AdvancedTradeApiClient.Models.TransactionsSummary transactionsSummary
    = await coinbaseClient.Fees.GetTransactionsSummaryAsync(ProductType.Spot, ProductVenue.Unknown, ContractExpiryType.Unknown, CancellationToken.None);


Console.ReadLine();
