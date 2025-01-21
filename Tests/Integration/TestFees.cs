using Coinbase.AdvancedTradeApiClient.Enums;

namespace Coinbase.AdvancedTrade.IntegrationTests;

[TestClass]
public class TestFees : TestBase
{
    [TestMethod]
    [Description("Test to verify that GetTransactionsSummary returns a valid summary of transactions.")]
    public async Task Test_Fees_GetTransactionsSummaryAsync()
    {
        await ExecuteRateLimitedTest(async () =>
        {

            // Directly await the method call, instead of using null-coalescing operator
            var result = await _coinbaseClient!.Fees.GetTransactionsSummaryAsync(ProductType.Spot, ProductVenue.Unknown, ContractExpiryType.Unknown, CancellationToken.None);

            Assert.IsNotNull(result, "Result should not be null.");
            Assert.IsNotNull(result?.FeeTier?.PricingTier, "FeeTier should not be null.");
        });
    }
}
