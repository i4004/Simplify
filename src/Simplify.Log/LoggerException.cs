using System;

namespace Simplify.Log
{
	/// <summary>
	/// The exception class using for Logger exceptions
	/// </summary>
	[Serializable]
	public sealed class LoggerException : Exception
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="LoggerException"/> class.
		/// </summary>
		/// <param name="message">The message that describes the error.</param>
		public LoggerException(string message) : base(message) {}
	}
}
