using Coinbase.AdvancedTradeApiClient.Utilities;
using System.Text.Json.Serialization;

namespace Coinbase.AdvancedTradeApiClient.Models;

internal class InternalApiKeyDetails : IModelMapper<ApiKeyDetails>
{
    [JsonPropertyName("can_view")]
    public bool CanView { get; set; }

    [JsonPropertyName("can_trade")]
    public bool CanTrade { get; set; }

    [JsonPropertyName("can_transfer")]
    public bool CanTransfer { get; set; }

    [JsonPropertyName("portfolio_uuid")]
    public string PortfolioUuid { get; set; }

    [JsonPropertyName("portfolio_type")]
    public string PortfolioType { get; set; }

    public ApiKeyDetails ToModel()
    {
        return new ApiKeyDetails
        {
            CanView = CanView,
            CanTrade = CanTrade,
            CanTransfer = CanTransfer,
            PortfolioUuid = PortfolioUuid,
            PortfolioType = PortfolioType
        };
    }
}