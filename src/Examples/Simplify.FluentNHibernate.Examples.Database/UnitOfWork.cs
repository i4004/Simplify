using System;

using NHibernate;

using Simplify.FluentNHibernate.Examples.Domain;

namespace Simplify.FluentNHibernate.Examples.Database
{
	public class UnitOfWork : IUnitOfWork
	{
		public ISession Session { get; private set; }
		private readonly ITransaction _transaction;

		public UnitOfWork(ISessionFactory sessionFactory)
		{
			Session = sessionFactory.OpenSession();
			_transaction = Session.BeginTransaction();
		}

		public void Commit()
		{
			if (!_transaction.IsActive)
				throw new InvalidOperationException("Oops! We don't have an active transaction");

			_transaction.Commit();
		}

		public void Rollback()
		{
			if (_transaction.IsActive)
				_transaction.Rollback();
		}

		public void Dispose()
		{
			if (Session.IsOpen)
				Session.Close();
		}
	}
}
