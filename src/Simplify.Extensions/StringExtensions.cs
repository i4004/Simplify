using System;
using System.Globalization;

namespace Simplify.Extensions
{
	/// <summary>
	/// Provides extensions for the string class
	/// </summary>
	public static class StringExtensions
	{
		/// <summary>
		/// Convert string to the bytes array.
		/// </summary>
		/// <param name="str">The string.</param>
		/// <returns></returns>
		public static byte[] ToBytesArray(this string str)
		{
			var bytes = new byte[str.Length * sizeof(char)];
			Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
			return bytes;
		}

		/// <summary>
		/// Converts the specified string representation of a date and time to its DateTime? equivalent using the specified format, invariant culture format information. The format of the string representation must match at least one of the specified formats exactly. The method returns a value if the conversion succeeded otherwise null
		/// </summary>
		/// <param name="s">A string containing one or more dates and times to convert.</param>
		/// <param name="format">The required format of <paramref name="s"/></param>
		/// <returns></returns>
		public static DateTime? TryToDateTimeExact(this string s, string format)
		{
			DateTime date;

			return DateTime.TryParseExact(s, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out date) ? date : (DateTime?)null;
		}
	}
}