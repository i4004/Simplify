using System;
using System.Data;
using NHibernate;

namespace Simplify.Repository.FluentNHibernate
{
	/// <summary>
	/// Provides unit of work with manual open transaction
	/// </summary>
	public class BeginTransactUnitOfWork : IBeginTransactUnitOfWork
	{
		private ITransaction _transaction;

		/// <summary>
		/// Gets the session.
		/// </summary>
		/// <value>
		/// The session.
		/// </value>
		public ISession Session { get; }

		/// <summary>
		/// Initializes a new instance of the <see cref="BeginTransactUnitOfWork"/> class.
		/// </summary>
		/// <param name="sessionFactory">The session factory.</param>
		public BeginTransactUnitOfWork(ISessionFactory sessionFactory)
		{
			Session = sessionFactory.OpenSession();
		}

		/// <summary>
		/// Begins the transaction.
		/// </summary>
		/// <param name="isolationLevel">The isolation level.</param>
		public void BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
		{
			_transaction = Session.BeginTransaction(isolationLevel);
		}

		/// <summary>
		/// Commits transaction.
		/// </summary>
		/// <exception cref="System.InvalidOperationException">Oops! We don't have an active transaction</exception>
		public void Commit()
		{
			if (!_transaction.IsActive)
				throw new InvalidOperationException("Oops! We don't have an active transaction");

			_transaction.Commit();
		}

		/// <summary>
		/// Rollbacks transaction.
		/// </summary>
		public void Rollback()
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