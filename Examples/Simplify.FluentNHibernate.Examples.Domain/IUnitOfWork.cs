namespace Simplify.FluentNHibernate.Examples.Domain
{
	public interface IUnitOfWork
	{
		void Commit();
		void Rollback();
	}
}