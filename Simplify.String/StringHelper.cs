using System.Text.RegularExpressions;

namespace Simplify.String
{
	/// <summary>
	/// Strings operations helper class
	/// </summary>
	public static class StringHelper
	{
		/// <summary>
		/// Parse and convert mobile phone to format like +77015543456
		/// </summary>
		/// <param name="phone">Mobile phone source string</param>
		/// <returns></returns>
		public static string ParseMobilePhone(string phone)
		{
			return Regex.Replace(phone, @"[^+0-9]", "");
		}

		/// <summary>
		/// Checking if e-mail address is correct
		/// </summary>
		/// <param name="eMail">E-mail address</param>
		/// <returns></returns>
		public static bool ValidateEMail(string eMail)
		{
			if(string.IsNullOrEmpty(eMail))
				return false;

			const string regularExpression = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
								 @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
								 @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";

			return Regex.IsMatch(eMail, regularExpression);
		}

		/// <summary>
		/// Checking if mobile phone is correct
		/// </summary>
		/// <param name="phone">Mobile phone number</param>
		/// <returns></returns>
		public static bool ValidateMobilePhone(string phone)
		{
			if(string.IsNullOrEmpty(phone))
				return false;

			return Regex.IsMatch(phone, @"^(8{1}\d{10})$") ||
				   Regex.IsMatch(phone, @"^(\+7{1}\d{10})$");
		}

		/// <summary>
		/// Indistinct matching of the two strings.
		/// </summary>
		/// <param name="stringA">The string a.</param>
		/// <param name="stringB">The string b.</param>
		/// <param name="comparingBlockLength">Length of the comparing block.</param>
		/// <returns></returns>
		public static float IndistinctMatching(string stringA, string stringB, int comparingBlockLength = 3)
		{
			RetCount gRet;
			int currentLength;

			if(string.IsNullOrEmpty(stringA) || string.IsNullOrEmpty(stringB))
				return 0;

			gRet.CountLike = 0;
			gRet.SubRows = 0;

			for(currentLength = 1; currentLength <= comparingBlockLength; currentLength++)
			{
				var tRet = Matching(stringA, stringB, currentLength);
				gRet.CountLike = gRet.CountLike + tRet.CountLike;
				gRet.SubRows = gRet.SubRows + tRet.SubRows;

				tRet = Matching(stringB, stringA, currentLength);
				gRet.CountLike = gRet.CountLike + tRet.CountLike;
				gRet.SubRows = gRet.SubRows + tRet.SubRows;
			}

			if(gRet.SubRows == 0)
				return 0;

			return (float)(gRet.CountLike * 100.0 / gRet.SubRows);
		}

		private static RetCount Matching(string stringA, string stringB, int length)
		{
			RetCount tempRet;
			int posStrA;

			tempRet.CountLike = 0;
			tempRet.SubRows = 0;

			for(posStrA = 0; posStrA <= stringA.Length - length; posStrA++)
			{
				var tempStringA = stringA.Substring(posStrA, length);
				int posStrB;

				for(posStrB = 0; posStrB <= stringB.Length - length; posStrB++)
				{
					var tempStringB = stringB.Substring(posStrB, length);

					if((System.String.CompareOrdinal(tempStringA, tempStringB) != 0))
						continue;

					tempRet.CountLike = (tempRet.CountLike + 1);
					break;
				}

				tempRet.SubRows = (tempRet.SubRows + 1);
			}
			return tempRet;
		}

		private struct RetCount
		{
			public long SubRows;
			public long CountLike;
		}

		/// <summary>
		/// Strips the HTML tags of the strings.
		/// </summary>
		/// <param name="source">The source string.</param>
		/// <returns></returns>
		public static string StripHtmlTags(string source)
		{
			return Regex.Replace(source, "<.*?>", string.Empty);
		}
	}
}
