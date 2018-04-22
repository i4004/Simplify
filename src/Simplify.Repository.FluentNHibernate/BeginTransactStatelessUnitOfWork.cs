using System;
using NHibernate;

namespace Simplify.Repository.FluentNHibernate
{
	/// <summary>
	///  Provides unit of work with manual statless session open transaction
	/// </summary>
	/// <seealso cref="IUnitOfWork" />
	public class BeginTransactStatelessUnitOfWork : StatelessUnitOfWork, ITransactUnitOfWork
	{
		private ITransaction _transaction;

		/// <summary>
		/// Initializes a new instance of the <see cref="StatelessUnitOfWork"/> class.
		/// </summary>
		/// <param name="sessionFactory">The session factory.</param>
		public BeginTransactStatelessUnitOfWork(ISessionFactory sessionFactory) : base(sessionFactory)
		{
		}

		/// <summary>
		/// Gets a value indicating whether this instance is transaction active.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is transaction active; otherwise, <c>false</c>.
		/// </value>
		public bool IsTransactionActive { get; private set; }

		/// <summary>
		/// Begins the transaction.
		/// </summary>
		public void BeginTransaction()
		{
			IsTransactionActive = true;
			_transaction = Session.BeginTransaction();
		}

		/// <summary>
		/// Commits transaction.
		/// </summary>
		/// <exception cref="InvalidOperationException">Oops! We don't have an active transaction</exception>
		public void Commit()
		{
			if (!_transaction.IsActive)
				throw new InvalidOperationException("Oops! We don't have an active transaction");

			_transaction.Commit();
			IsTransactionActive = false;
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
		/// Releases unmanaged and - optionally - managed resources.
		/// </summary>
		public void Dispose()
		{
			if (Session.IsOpen)
				Session.Close();
		}
	}
}