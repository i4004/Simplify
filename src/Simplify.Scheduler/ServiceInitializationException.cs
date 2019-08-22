using System;

namespace Simplify.Scheduler
{
	/// <summary>
	/// The exception class using for ApplicationHelper.Service services exceptions
	/// </summary>
	[Serializable]
	public sealed class ServiceInitializationException : Exception
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ServiceInitializationException"/> class.
		/// </summary>
		/// <param name="message">The message that describes the error.</param>
		public ServiceInitializationException(string message) : base(message) { }
	}
}