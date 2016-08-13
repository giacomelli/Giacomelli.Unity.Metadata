using System.Diagnostics.CodeAnalysis;
using System.Globalization;

[SuppressMessage("Microsoft.Design", "CA1050:DeclareTypesInNamespaces")]
public static class StringExtensions
{
    /// <summary>
    /// A shortcut to string.Format(CultureInfo.InvariantCulture.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <param name="args">The arguments.</param>
    /// <returns>The formatted string</returns>
    public static string With(this string value, params object[] args)
    {
        return string.Format(CultureInfo.InvariantCulture, value, args);
    }
}
