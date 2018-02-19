using System;

namespace Simplify.FluentNHibernate
{
	/// <summary>
	/// Provides Simplify.FluentNHibernate related exception
	/// </summary>
	/// <seealso cref="Exception" />
	public class SimplifyFluentNHibernateException : Exception
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="SimplifyFluentNHibernateException"/> class.
		/// </summary>
		public SimplifyFluentNHibernateException()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="SimplifyFluentNHibernateException"/> class.
		/// </summary>
		/// <param name="message">The message that describes the error.</param>
		public SimplifyFluentNHibernateException(string message) : base(message)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="SimplifyFluentNHibernateException"/> class.
		/// </summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="innerException">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified.</param>
		public SimplifyFluentNHibernateException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}