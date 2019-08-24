using System;

namespace Simplify.Scheduler
{
	/// <summary>
	/// The exception class using for scheduler initialization exceptions
	/// </summary>
	[Serializable]
	public sealed class SchedulerInitializationException : Exception
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="SchedulerInitializationException"/> class.
		/// </summary>
		/// <param name="message">The message that describes the error.</param>
		public SchedulerInitializationException(string message) : base(message) { }
	}
}