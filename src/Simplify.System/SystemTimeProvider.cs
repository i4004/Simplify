using System;

namespace Simplify.System
{
	/// <summary>
	/// System time provider, returns the actual DateTime.Now, DateTime.UtcNow, DateTime.Today data
	/// </summary>
	public sealed class SystemTimeProvider : ITimeProvider
	{
		/// <summary>
		/// Gets the current UTC time.
		/// </summary>
		/// <value>
		/// The current UTC time.
		/// </value>
		public DateTime UtcNow => DateTime.UtcNow;

		/// <summary>
		/// Gets the current time.
		/// </summary>
		/// <value>
		/// The current time.
		/// </value>
		public DateTime Now => DateTime.Now;

		/// <summary>
		/// Gets the today date without time.
		/// </summary>
		/// <value>
		/// The today date without time.
		/// </value>
		public DateTime Today => DateTime.Today;
	}
}