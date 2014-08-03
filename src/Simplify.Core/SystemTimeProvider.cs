using System;

namespace Simplify.Core
{
	/// <summary>
	/// System time provider, returns the actual DateTime.Now, DateTime.UtcNow, DateTime.Today data
	/// </summary>
	public sealed class SystemTimeProvider : TimeProvider
	{
		/// <summary>
		/// Gets the currrent UTC time.
		/// </summary>
		/// <value>
		/// The currrent UTC time.
		/// </value>
		public override DateTime UtcNow
		{
			get { return DateTime.UtcNow; }
		}

		/// <summary>
		/// Gets the current time.
		/// </summary>
		/// <value>
		/// The current time.
		/// </value>
		public override DateTime Now
		{
			get { return DateTime.Now; }
		}

		/// <summary>
		/// Gets the today date without time.
		/// </summary>
		/// <value>
		/// The today date without time.
		/// </value>
		public override DateTime Today
		{
			get { return DateTime.Today; }
		}
	}
}
