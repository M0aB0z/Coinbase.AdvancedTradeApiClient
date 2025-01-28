using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Coinbase.AdvancedTradeApiClient.Utilities;

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
    private static List<(string, string)> ConvertToPropertiesDictionary(this object obj)
    {
        var res = new List<(string, string)>();

        var props = obj.GetType().GetProperties()
            .Where(prop => prop.GetValue(obj) != null); // Simplified null check

        foreach (var prop in props)
        {
            var propName = prop.Name;
            var propValue = prop.GetValue(obj);
            if (propValue is Array array) // Handle arrays
            {
                foreach (var item in array)
                    res.Add((propName, item.ToString()));
            }
            else if (propValue is IList propValueList && propValue is not string) // Handle non-generic lists
            {
                foreach (var item in propValueList)
                    res.Add((propName, item.ToString()));
            }
            else
                res.Add((propName, propValue?.ToString() ?? string.Empty));
        }

        return res;
    }

    /// <summary>
    /// Builds a URI with query parameters from an object's properties.
    /// </summary>
    /// <param name="uri"></param>
    /// <param name="paramsObj"></param>
    /// <returns></returns>
    public static string BuildParamUri(string uri, object paramsObj)
    {
        var parameters = paramsObj.ConvertToPropertiesDictionary();
        var queryString = string.Join("&", parameters.Select(kvp => $"{kvp.Item1}={kvp.Item2}"));
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
}
