namespace Coinbase.AdvancedTradeApiClient.Utilities.Extensions;

internal static class DecimalExtensions
{
    public static string ToApiString(this decimal value)
        => value.ToString().Replace(",", ".");
}
