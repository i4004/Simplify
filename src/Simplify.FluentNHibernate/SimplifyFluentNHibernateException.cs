using System;

namespace Simplify.FluentNHibernate
{
	/// <summary>
	/// Provides Simplify.FluentNHibernate related exception
	/// </summary>
	/// <seealso cref="Exception" />
	public class SimplifyFluentNHibernateException : Exception
	{
		public SimplifyFluentNHibernateException()
		{
		}

		public SimplifyFluentNHibernateException(string message) : base(message)
		{
		}

		public SimplifyFluentNHibernateException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}