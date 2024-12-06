namespace Coinbase.AdvancedTrade.Utilities.Extensions;

internal static class StringExtensions
{
    public static double ToDouble(this string value)
    {
        if (string.IsNullOrEmpty(value?.Trim()))
            return default;

        return double.Parse(value.Replace(".", ","));
    }
    public static double? ToNullableDouble(this string value)
    {
        if (string.IsNullOrEmpty(value?.Trim()))
            return null;

        return value.ToDouble();
    }
}
