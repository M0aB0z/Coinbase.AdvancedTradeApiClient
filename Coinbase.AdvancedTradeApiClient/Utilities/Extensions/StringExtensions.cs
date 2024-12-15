namespace Coinbase.AdvancedTradeApiClient.Utilities.Extensions;

internal static class StringExtensions
{
    public static decimal ToDecimal(this string value) => decimal.Parse(value.Replace(".", ","));
    public static decimal? ToNullableDecimal(this string value)
    {
        if (string.IsNullOrEmpty(value?.Trim()))
            return null;

        return value.ToDecimal();
    }

    public static double ToDouble(this string value) => double.Parse(value.Replace(".", ","));
    public static double? ToNullableDouble(this string value)
    {
        if (string.IsNullOrEmpty(value?.Trim()))
            return null;

        return value.ToDouble();
    }
}
