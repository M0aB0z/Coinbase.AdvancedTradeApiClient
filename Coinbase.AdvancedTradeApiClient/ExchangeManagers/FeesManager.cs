﻿using Coinbase.AdvancedTradeApiClient.Enums;
using Coinbase.AdvancedTradeApiClient.Interfaces;
using Coinbase.AdvancedTradeApiClient.Models;
using Coinbase.AdvancedTradeApiClient.Models.Internal;
using Coinbase.AdvancedTradeApiClient.Utilities;
using Coinbase.AdvancedTradeApiClient.Utilities.Extensions;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Coinbase.AdvancedTradeApiClient.ExchangeManagers;

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
        ProductType productType,
        ProductVenue productVenue,
        ContractExpiryType contractExpiryType,
        CancellationToken cancellationToken)
    {
        // Create request parameters
        var paramsDict = new
        {
            product_type = productType.GetDescription(),
            product_venue = productVenue.GetDescription(),
            contract_expiry_type = "UNKNOWN_CONTRACT_EXPIRY_TYPE"
        };

        try
        {
            // Assuming SendAuthenticatedRequest becomes asynchronous
            if (_authenticator == null)
                throw new InvalidOperationException("Authenticator is not initialized.");

            var response = await _authenticator.GetAsync(UtilityHelper.BuildParamUri("/api/v3/brokerage/transaction_summary", paramsDict), cancellationToken);

            return new InternalTransactionsSummary
            {
                TotalVolume = response.As<decimal>("total_volume"),
                TotalFees = response.As<decimal>("total_fees"),
                AdvancedTradeOnlyVolume = response.As<decimal>("advanced_trade_only_volume"),
                AdvancedTradeOnlyFees = response.As<decimal>("advanced_trade_only_fees"),
                CoinbaseProVolume = response.As<decimal>("coinbase_pro_volume"),
                CoinbaseProFees = response.As<decimal>("coinbase_pro_fees"),
                Low = response.As<decimal>("low"),
                FeeTier = response.As<InternalFeeTier>("fee_tier"),
                MarginRate = response.As<InternalMarginRate>("margin_rate"),
                GoodsAndServicesTax = response.As<InternalGoodsAndServicesTax>("goods_and_services_tax")
            }.ToModel();

        }
        catch (Exception ex)
        {
            // Wrap and rethrow exceptions to provide context.
            throw new InvalidOperationException("Failed to get transactions summary", ex);
        }
    }
}
