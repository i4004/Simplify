using System;
using NHibernate;

namespace Simplify.Repository.FluentNHibernate
{
	/// <summary>
	/// Base class for session factory managers
	/// </summary>
	public abstract class FactoryManagerBase : IDisposable
	{
		/// <summary>
		/// Gets or sets the instance.
		/// </summary>
		/// <value>
		/// The instance.
		/// </value>
		public ISessionFactory Instance { get; protected set; }

		/// <summary>
		/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
		/// </summary>
		public void Dispose()
		{
			Instance?.Dispose();
		}
	}
}