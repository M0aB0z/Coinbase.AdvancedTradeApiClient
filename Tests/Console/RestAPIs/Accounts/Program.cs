// Coinbase Cloud Trading Keys
using Coinbase.AdvancedTradeApiClient;

var apiKey = Environment.GetEnvironmentVariable("COINBASE_CLOUD_TRADING_API_KEY", EnvironmentVariableTarget.User)
             ?? throw new InvalidOperationException("API Key not found");
var apiSecret = Environment.GetEnvironmentVariable("COINBASE_CLOUD_TRADING_API_SECRET", EnvironmentVariableTarget.User)
               ?? throw new InvalidOperationException("API Secret not found");
var coinbaseClient = new CoinbaseClient(apiKey, apiSecret);

Console.WriteLine("Retrieving paginated accounts (first 5)...");

var accountsPage = await coinbaseClient.Accounts.ListAccountsAsync(5);

Console.WriteLine(accountsPage.Accounts.Count + " accounts retrieved");

Console.WriteLine("Retrieving all accounts...");

var allAccounts = await coinbaseClient.Accounts.ListAllAccountsAsync();
Console.WriteLine(allAccounts.Count + " accounts retrieved");

var usdcAccount = allAccounts.First(x => x.Currency == "USDC");


Console.ReadLine();