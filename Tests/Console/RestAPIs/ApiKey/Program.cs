// Coinbase Cloud Trading Keys
using Coinbase.AdvancedTradeApiClient;

var apiKey = Environment.GetEnvironmentVariable("COINBASE_CLOUD_TRADING_API_KEY", EnvironmentVariableTarget.User)
             ?? throw new InvalidOperationException("API Key not found");
var apiSecret = Environment.GetEnvironmentVariable("COINBASE_CLOUD_TRADING_API_SECRET", EnvironmentVariableTarget.User)
               ?? throw new InvalidOperationException("API Secret not found");
var coinbaseClient = new CoinbaseClient(apiKey, apiSecret);

Console.WriteLine("Retrieving api key details...");

var apiKeyDetails = await coinbaseClient.ApiKey.GetApiKeyDetailsAsync(CancellationToken.None);

var details = $"""
    Can view: {apiKeyDetails.CanView}
    Can trade: {apiKeyDetails.CanTrade}
    Can transfer: {apiKeyDetails.CanTransfer}
    Portfolio UUID: {apiKeyDetails.PortfolioUuid}
    Portfolio Type: {apiKeyDetails.PortfolioType}
    """;

Console.WriteLine(details);

Console.ReadLine();