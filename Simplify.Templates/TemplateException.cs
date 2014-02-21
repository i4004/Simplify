using System;

namespace Simplify.Templates
{
	/// <summary>
	/// The exception class using for Template exceptions
	/// </summary>
	[Serializable]
	public sealed class TemplateException : Exception
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="TemplateException"/> class.
		/// </summary>
		/// <param name="message">The message that describes the error.</param>
		public TemplateException(string message) : base(message) {}
	}
}
