using System;
using System.ComponentModel;
using System.Linq;

namespace Coinbase.AdvancedTrade.Utilities.Extensions;

internal static class EnumExtensions
{
    /// <summary>
    /// Gets the description attribute value of an enum value.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="value"></param>
    /// <returns></returns>
    public static string GetDescription<T>(this T value) where T : IConvertible
    {
        var fi = value.GetType().GetField(value.ToString());

        var attributes = fi.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];

        return attributes != null && attributes.Any()
            ? attributes.First().Description
            : value.ToString();
    }

    public static T FromDescriptionToEnum<T>(this string description) where T : IConvertible
    {
        var enumValues = Enum.GetValues(typeof(T)).Cast<T>();
        return enumValues.First(x => x.GetDescription() == description);
    }
}
