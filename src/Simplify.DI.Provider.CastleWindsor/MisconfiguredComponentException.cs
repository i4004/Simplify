using System;

namespace Simplify.DI.Provider.CastleWindsor
{
	/// <summary>
	/// Provides Castle.Windsor container verification exception
	/// </summary>
	/// <seealso cref="Exception" />
	public class MisconfiguredComponentException : Exception
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="MisconfiguredComponentException" /> class.
		/// </summary>
		/// <param name="message">The message.</param>
		public MisconfiguredComponentException(string message) : base(message)
		{
		}
	}
}