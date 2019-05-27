using NHibernate;
using Simplify.FluentNHibernate.Examples.Domain;
using Simplify.Repository.FluentNHibernate;

namespace Simplify.FluentNHibernate.Examples.Database
{
	public class ExampleUnitOfWork : TransactUnitOfWork, IExampleUnitOfWork
	{
		public ExampleUnitOfWork(ISessionFactory sessionFactory) : base(sessionFactory)
		{
		}
	}
}