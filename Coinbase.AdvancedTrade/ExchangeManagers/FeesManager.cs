using Coinbase.AdvancedTrade.Interfaces;
using Coinbase.AdvancedTrade.Models;
using Coinbase.AdvancedTrade.Utilities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Coinbase.AdvancedTrade.ExchangeManagers
{
    /// <summary>
    /// Manages fee-related activities for the Coinbase Advanced Trade API.
    /// </summary>
    public class FeesManager : BaseManager, IFeesManager
    {
        /// <summary>
        /// Initializes a new instance of the FeesManager class.
        /// </summary>
        /// <param name="authenticator">The Coinbase authenticator.</param>
        public FeesManager(CoinbaseAuthenticator authenticator) : base(authenticator) { }

        /// <inheritdoc/>
        public async Task<TransactionsSummary> GetTransactionsSummaryAsync(
            DateTime startDate,
            DateTime endDate,
            string userNativeCurrency = "USD",
            string productType = "SPOT",
            CancellationToken cancellationToken = default)
        {
            // Create request parameters
            var paramsDict = new Dictionary<string, string>
            {
                {"start_date", startDate.ToString("yyyy-MM-ddTHH:mm:ssZ")},
                {"end_date", endDate.ToString("yyyy-MM-ddTHH:mm:ssZ")},
                {"user_native_currency", userNativeCurrency},
                {"product_type", productType}
            };

            try
            {
                // Assuming SendAuthenticatedRequest becomes asynchronous
                if (_authenticator == null)
                    throw new InvalidOperationException("Authenticator is not initialized.");

                var response = await _authenticator.GetAsync(UtilityHelper.BuildParamUri("/api/v3/brokerage/transaction_summary", paramsDict), cancellationToken);

                return new TransactionsSummary
                {
                    TotalVolume = response.ExtractDoubleValue("total_volume") ?? 0.0,
                    TotalFees = response.ExtractDoubleValue("total_fees") ?? 0.0,
                    AdvancedTradeOnlyVolume = response.ExtractDoubleValue("advanced_trade_only_volume") ?? 0.0,
                    AdvancedTradeOnlyFees = response.ExtractDoubleValue("advanced_trade_only_fees") ?? 0.0,
                    CoinbaseProVolume = response.ExtractDoubleValue("coinbase_pro_volume") ?? 0.0,
                    CoinbaseProFees = response.ExtractDoubleValue("coinbase_pro_fees") ?? 0.0,
                    Low = response.ExtractDoubleValue("low") ?? 0.0,
                    FeeTier = response.As<FeeTier>("fee_tier"),
                    MarginRate = response.As<MarginRate>("margin_rate"),
                    GoodsAndServicesTax = response.As<GoodsAndServicesTax>("goods_and_services_tax")
                };
                
            }
            catch (Exception ex)
            {
                // Wrap and rethrow exceptions to provide context.
                throw new InvalidOperationException("Failed to get transactions summary", ex);
            }
        }
    }
}
