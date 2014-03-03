using System;
using System.Text;

namespace Simplify.Cryptography
{
	/// <summary>
	/// MD5 hash algorithm implementaion class
	/// </summary>
	public static class MD5
	{
		private static System.Security.Cryptography.MD5 MD5HashInstance;
		private static System.Security.Cryptography.MD5 MD5Hash
		{
			get
			{
				return MD5HashInstance ?? (MD5HashInstance = System.Security.Cryptography.MD5.Create());
			}
		}

		/// <summary>
		/// Get MD5 hash code of an input string
		/// </summary>
		/// <param name="input">Input string</param>
		/// <returns>MD5 hash code of input string</returns>
		public static string GetHash(string input)
		{
			// Convert the input string to a byte array and compute the hash.
			var data = MD5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

			var sBuilder = new StringBuilder();

			// Loop through each byte of the hashed data 
			// and format each one as a hexadecimal string.
			foreach (var t in data)
				sBuilder.Append(t.ToString("x2"));

			return sBuilder.ToString();
		}

		/// <summary>
		/// Verify a hash against a string.
		/// </summary>
		/// <param name="input">Input string</param>
		/// <param name="hash">hash for checking</param>
		/// <returns><see langword="true"/> if input string hash equals hash otherwise <see langword="false"/></returns>
		public static bool VerifyHash(string input, string hash)
		{
			var hashOfInput = GetHash(input);

			var comparer = StringComparer.OrdinalIgnoreCase;

			return 0 == comparer.Compare(hashOfInput, hash);
		}
	}
}
