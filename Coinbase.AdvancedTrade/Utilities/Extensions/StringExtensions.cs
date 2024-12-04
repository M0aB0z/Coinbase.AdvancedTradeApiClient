namespace Coinbase.AdvancedTrade.Utilities.Extensions;

internal static class StringExtensions
{
    public static double ToDouble(this string value) => double.Parse(value.Replace(".", ","));
}
