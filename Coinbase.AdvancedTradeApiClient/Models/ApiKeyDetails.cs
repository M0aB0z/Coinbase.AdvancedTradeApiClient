namespace Coinbase.AdvancedTradeApiClient.Models;

/// <summary>
/// Represents the details of an API key.
/// </summary>
public class ApiKeyDetails
{
    /// <summary>
    /// Is your key allowed to read data?
    /// </summary>
    public bool CanView { get; internal set; }

    /// <summary>
    /// Is your key allowed to trade?
    /// </summary>
    public bool CanTrade { get; internal set; }

    /// <summary>
    /// Is your key allowed to transfer?
    /// </summary>
    public bool CanTransfer { get; internal set; }

    /// <summary>
    /// Your key's portfolio UUID.
    /// </summary>
    public string PortfolioUuid { get; internal set; }

    /// <summary>
    /// Your key's portfolio type.
    /// </summary>
    public string PortfolioType { get; internal set; }
}