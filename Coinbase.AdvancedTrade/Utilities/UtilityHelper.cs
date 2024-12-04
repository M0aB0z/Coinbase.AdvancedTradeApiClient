using Coinbase.AdvancedTrade.Utilities.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Coinbase.AdvancedTrade.Utilities;

/// <summary>
/// Provides various utility functions for data manipulation and conversion.
/// </summary>
internal static class UtilityHelper
{


    /// <summary>
    /// Converts an object's properties to a dictionary of string keys and values.
    /// </summary>
    /// <param name="obj">The object to convert.</param>
    /// <returns>A dictionary representation of the object's properties.</returns>
    private static Dictionary<string, string> ConvertToDictionary(object obj)
    {
        return obj.GetType().GetProperties()
            .Where(prop => prop.GetValue(obj) != null) // Simplified null check
            .ToDictionary(
                prop => prop.Name,
                prop =>
                {
                    var value = prop.GetValue(obj);
                    if (value is Array array) // Handle arrays
                        return string.Join(",", array.Cast<object>());
                    else if (value is IList && !(value is string)) // Handle non-generic lists
                        return string.Join(",", ((IList)value).Cast<object>());
                    else // Handle other types
                        return value?.ToString() ?? string.Empty;
                }
            );
    }

    /// <summary>
    /// Builds a URI with query parameters from an object's properties.
    /// </summary>
    /// <param name="uri"></param>
    /// <param name="paramsObj"></param>
    /// <returns></returns>
    public static string BuildParamUri(string uri, object paramsObj)
    {
        var parameters = ConvertToDictionary(paramsObj);
        var queryString = string.Join("&", parameters.Select(kvp => $"{kvp.Key}={kvp.Value}"));
        return $"{uri}?{queryString}";
    }

    /// <summary>
    /// Converts an array of enums to an array of strings.
    /// </summary>
    /// <param name="enums">The array of enums.</param>
    /// <typeparam name="TEnum">The type of enum.</typeparam>
    /// <returns>An array of strings.</returns>
    internal static string[] EnumToStringArray<TEnum>(TEnum[] enums) where TEnum : struct
    {
        if (enums == null)
            return null;

        return enums.Select(e => e.ToString()).ToArray();
    }

    /// <summary>
    /// Formats a DateTime instance to the ISO 8601 format.
    /// </summary>
    /// <param name="dateTime">The DateTime instance to format.</param>
    /// <returns>The ISO 8601 formatted string.</returns>
    internal static string FormatDateToISO8601(DateTime? dateTime)
    {
        return dateTime?.ToUniversalTime().ToString("o");
    }

    internal static double? ToNullableDouble(string value)
    {
        if (string.IsNullOrEmpty(value?.Trim()))
            return null;
        return value.ToDouble();
    }
}
