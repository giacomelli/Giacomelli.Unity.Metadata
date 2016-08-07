using System;
using System.Globalization;

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

