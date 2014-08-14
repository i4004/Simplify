using System;

namespace Simplify.System
{
	/// <summary>
	/// Provides Time abstraction
	/// </summary>
	public abstract class TimeProvider
	{
		private static TimeProvider _currentInstance;

		/// <summary>
		/// Gets or sets the current time provider.
		/// </summary>
		/// <value>
		/// The current time provider.
		/// </value>
		/// <exception cref="ArgumentNullException">value</exception>
		public static TimeProvider Current
		{
			get
			{
				return _currentInstance ?? (_currentInstance = new SystemTimeProvider());
			}
			set
			{
				if (value == null)
					throw new ArgumentNullException("value");

				_currentInstance = value;
			}
		}

		/// <summary>
		/// Gets the current UTC time.
		/// </summary>
		/// <value>
		/// The current UTC time.
		/// </value>
		public abstract DateTime UtcNow { get; }

		/// <summary>
		/// Gets the current time.
		/// </summary>
		/// <value>
		/// The current time.
		/// </value>
		public abstract DateTime Now { get; }

		/// <summary>
		/// Gets the today date without time.
		/// </summary>
		/// <value>
		/// The today date without time.
		/// </value>
		public abstract DateTime Today { get; }
	}
}
