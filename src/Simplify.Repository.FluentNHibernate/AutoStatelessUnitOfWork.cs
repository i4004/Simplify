using System;
using NHibernate;

namespace Simplify.Repository.FluentNHibernate
{
	/// <summary>
	/// Provides unit of work with auto-open stateless session transaction
	/// </summary>
	/// <seealso cref="IUnitOfWork" />
	public class AutoStatelessUnitOfWork : StatelessUnitOfWork, IAutoUnitOfWork
	{
		private readonly ITransaction _transaction;

		/// <summary>
		/// Initializes a new instance of the <see cref="StatelessUnitOfWork"/> class.
		/// </summary>
		/// <param name="sessionFactory">The session factory.</param>
		public AutoStatelessUnitOfWork(ISessionFactory sessionFactory) : base(sessionFactory)
		{
			_transaction = Session.BeginTransaction();
			IsTransactionActive = true;
		}

		/// <summary>
		/// Gets a value indicating whether this instance is transaction active.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is transaction active; otherwise, <c>false</c>.
		/// </value>
		public bool IsTransactionActive { get; private set; }

		/// <summary>
		/// Commits transaction.
		/// </summary>
		/// <exception cref="InvalidOperationException">Oops! We don't have an active transaction</exception>
		public void Commit()
		{
			if (!IsTransactionActive)
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
	}
}