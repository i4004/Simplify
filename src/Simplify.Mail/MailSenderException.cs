using System;

namespace Simplify.Mail
{
	/// <summary>
	/// The exception class using for MailSender exceptions
	/// </summary>
	[Serializable]
	public sealed class MailSenderException : Exception
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="MailSenderException"/> class.
		/// </summary>
		/// <param name="message">The message that describes the error.</param>
		public MailSenderException(string message) : base(message) { }
	}
}
