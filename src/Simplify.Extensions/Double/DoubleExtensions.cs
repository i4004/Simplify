using System;

namespace Simplify.Extensions.Double
{
	/// <summary>
	/// Provides extensions for the double class
	/// </summary>
	public static class DoubleExtensions
	{
		/// <summary>
		/// The epsilon value for double variables comparison
		/// </summary>
		public const double Epsilon = 0.000000000000000000001;

		/// <summary>
		/// Checking what two double values most likely the same ( the difference between values is less than Epsilon)
		/// </summary>
		/// <param name="a">First value to compare</param>
		/// <param name="b">Second value to compare</param>
		/// <returns></returns>
		public static bool AreSameAs(this double a, double b)
		{
			return Math.Abs(a - b) < Epsilon;
		}
	}
}
