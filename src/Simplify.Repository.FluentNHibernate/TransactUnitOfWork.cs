using System;
using NHibernate;

namespace Simplify.Repository.FluentNHibernate
{
	/// <summary>
	/// Provides unit of work with auto-open transaction
	/// </summary>
	public class TransactUnitOfWork : ITransactUnitOfWork
	{
		private readonly ITransaction _transaction;

		/// <summary>
		/// Gets the session.
		/// </summary>
		/// <value>
		/// The session.
		/// </value>
		public ISession Session { get; }

		/// <summary>
		/// Initializes a new instance of the <see cref="TransactUnitOfWork"/> class.
		/// </summary>
		/// <param name="sessionFactory">The session factory.</param>
		public TransactUnitOfWork(ISessionFactory sessionFactory)
		{
			Session = sessionFactory.OpenSession();
			_transaction = Session.BeginTransaction();
		}

		/// <summary>
		/// Commits transaction.
		/// </summary>
		/// <exception cref="InvalidOperationException">Oops! We don't have an active transaction</exception>
		public virtual void Commit()
		{
			if (!_transaction.IsActive)
				throw new InvalidOperationException("Oops! We don't have an active transaction");

			_transaction.Commit();
		}

		/// <summary>
		/// Rollbacks transaction.
		/// </summary>
		public virtual void Rollback()
		{
			if (_transaction.IsActive)
				_transaction.Rollback();
		}

		/// <summary>
		/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
		/// </summary>
		public void Dispose()
		{
			Session?.Dispose();
		}
	}
}