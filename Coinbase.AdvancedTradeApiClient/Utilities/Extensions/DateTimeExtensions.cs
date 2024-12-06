using System;

namespace Coinbase.AdvancedTradeApiClient.Utilities.Extensions;

internal static class DateTimeExtensions
{

    /// <summary>
    /// Formats a DateTime instance to the ISO 8601 format.
    /// </summary>
    /// <param name="dateTime">The DateTime instance to format.</param>
    /// <returns>The ISO 8601 formatted string.</returns>
    internal static string FormatDateToISO8601(this DateTime dateTime) => dateTime.ToUniversalTime().ToString("o");
}
