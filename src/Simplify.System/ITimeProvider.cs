using System;

namespace Simplify.System
{
	/// <summary>
	/// Represent time provider
	/// </summary>
	public interface ITimeProvider : IHideObjectMembers
	{
		/// <summary>
		/// Gets the current UTC time.
		/// </summary>
		/// <value>
		/// The current UTC time.
		/// </value>
		DateTime UtcNow { get; }

		/// <summary>
		/// Gets the current time.
		/// </summary>
		/// <value>
		/// The current time.
		/// </value>
		DateTime Now { get; }

		/// <summary>
		/// Gets the today date without time.
		/// </summary>
		/// <value>
		/// The today date without time.
		/// </value>
		DateTime Today { get; }
	}
}