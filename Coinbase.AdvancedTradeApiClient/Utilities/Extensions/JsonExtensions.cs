using System.Text.Json;

namespace Coinbase.AdvancedTradeApiClient.Utilities.Extensions;

internal static class JsonExtensions
{
    public static T As<T>(this JsonElement elt) => elt.Deserialize<T>();
    public static T As<T>(this JsonDocument elt, string propertyName) => elt.RootElement.As<T>(propertyName);
    public static T As<T>(this JsonElement elt, string propertyName)
        => elt.TryGetProperty(propertyName, out JsonElement prop) ? prop.Deserialize<T>() : default;

}
