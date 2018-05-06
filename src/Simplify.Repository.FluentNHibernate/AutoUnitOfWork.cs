using System;
using NHibernate;

namespace Simplify.Repository.FluentNHibernate
{
	/// <summary>
	/// Provides unit of work with auto-open transaction
	/// </summary>
	public class AutoUnitOfWork : UnitOfWork, IAutoUnitOfWork
	{
		private readonly ITransaction _transaction;

		/// <summary>
		/// Initializes a new instance of the <see cref="AutoUnitOfWork"/> class.
		/// </summary>
		/// <param name="sessionFactory">The session factory.</param>
		public AutoUnitOfWork(ISessionFactory sessionFactory) : base(sessionFactory)
		{
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
	}
}