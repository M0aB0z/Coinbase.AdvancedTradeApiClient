using System;
using System.Text.Json;

namespace Coinbase.AdvancedTradeApiClient.Utilities.Extensions;

internal static class JsonExtensions
{
    public static T As<T>(this JsonDocument elt) => elt.RootElement.Deserialize<T>();
    public static T As<T>(this JsonElement elt) => elt.Deserialize<T>();
    public static T As<T>(this JsonDocument elt, string propertyName) => elt.RootElement.As<T>(propertyName);
    public static T As<T>(this JsonElement elt, string propertyName)
        => elt.TryGetProperty(propertyName, out JsonElement prop) ? prop.Deserialize<T>() : default;

    /// <summary>
    /// Extracts a double value from a JSON Elt specific property - handles string representation.
    /// </summary>
    /// <returns>The extracted double value, or null if extraction fails.</returns>
    /// <exception cref="InvalidOperationException">Thrown if extraction fails.</exception>
    public static double? ExtractDoubleValue(this JsonElement elt, string propertyName)
    {
        try
        {
            if (elt.TryGetProperty(propertyName, out JsonElement valueObj))
            {
                if (valueObj.ValueKind == JsonValueKind.Number)
                    return valueObj.GetDouble();
                else if (valueObj.ValueKind == JsonValueKind.String && double.TryParse(valueObj.GetString(), out var doubleValue))
                    return doubleValue;
            }
            return null;
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Failed to extract double value", ex);
        }
    }
}
