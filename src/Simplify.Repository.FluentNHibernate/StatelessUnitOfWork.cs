using NHibernate;

namespace Simplify.Repository.FluentNHibernate
{
	/// <summary>
	/// Provides unit of work with stateless session
	/// </summary>
	/// <seealso cref="IUnitOfWork" />
	public class StatelessUnitOfWork : IUnitOfWork
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="StatelessUnitOfWork"/> class.
		/// </summary>
		/// <param name="sessionFactory">The session factory.</param>
		public StatelessUnitOfWork(ISessionFactory sessionFactory)
		{
			Session = sessionFactory.OpenStatelessSession();
		}

		/// <summary>
		/// Gets the session.
		/// </summary>
		/// <value>
		/// The session.
		/// </value>
		public IStatelessSession Session { get; }

		/// <summary>
		/// Releases unmanaged and - optionally - managed resources.
		/// </summary>
		public void Dispose()
		{
			if (Session.IsOpen)
				Session.Close();
		}
	}
}