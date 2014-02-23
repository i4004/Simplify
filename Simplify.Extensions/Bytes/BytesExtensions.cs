using System;

namespace Simplify.Extensions.Bytes
{
	/// <summary>
	/// Provides extensions fo the bytes[] arrays
	/// </summary>
	public static class BytesExtensions
	{
		/// <summary>
		/// Converts bytes array to the string.
		/// </summary>
		/// <param name="bytes">The bytes array.</param>
		/// <returns></returns>
		public static string GetString(this byte[] bytes)
		{
			var chars = new char[bytes.Length / sizeof(char)];
			Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
			return new string(chars);
		}
	}
}
