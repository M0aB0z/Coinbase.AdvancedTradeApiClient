using System;

namespace Coinbase.AdvancedTradeApiClient.Models;

/// <summary>
/// Represents a candlestick data point for a specific time frame in a trading chart.
/// </summary>
public class Candle

{
    /// <summary>
    /// Gets the start time of the candlestick in UNIX timestamp format.
    /// </summary>
    public string StartUnix { get; internal set; }

    /// <summary>
    /// Gets the start date and time of the candlestick.
    /// </summary>
    public DateTime StartDate => !string.IsNullOrEmpty(StartUnix) ? UnixTimeStampToDateTime(StartUnix) : DateTime.MinValue;

    /// <summary>
    /// Gets the lowest traded price of the asset during the time interval represented by the candlestick.
    /// </summary>
    public double Low { get; internal set; }

    /// <summary>
    /// Gets the highest traded price of the asset during the time interval represented by the candlestick.
    /// </summary>
    public double High { get; internal set; }

    /// <summary>
    /// Gets the opening price of the asset at the beginning of the time interval represented by the candlestick.
    /// </summary>
    public double Open { get; internal set; }

    /// <summary>
    /// Gets the closing price of the asset at the end of the time interval represented by the candlestick.
    /// </summary>
    public double Close { get; internal set; }

    /// <summary>
    /// Gets the trading volume of the asset during the time interval represented by the candlestick.
    /// </summary>
    public double Volume { get; internal set; }

    /// <summary>
    /// Converts a UNIX timestamp string to its corresponding DateTime value.
    /// </summary>
    /// <param name="unixTimeStamp">The UNIX timestamp string.</param>
    /// <returns>The converted DateTime value, or DateTime.MinValue if the conversion fails.</returns>
    private static DateTime UnixTimeStampToDateTime(string unixTimeStamp)
    {
        if (long.TryParse(unixTimeStamp, out long parsedUnixTime))
        {
            return DateTimeOffset.FromUnixTimeSeconds(parsedUnixTime).UtcDateTime;
        }
        return DateTime.MinValue;
    }

    /// <inheritDoc/>
    public override string ToString() => $"[{StartDate:dd/MM/yyyy HH:mm}][Close={Math.Round(Close, 4)}] [Open={Math.Round(Open, 2)}][Low={Math.Round(Low, 2)}] [High={Math.Round(High, 2)}]";
}
