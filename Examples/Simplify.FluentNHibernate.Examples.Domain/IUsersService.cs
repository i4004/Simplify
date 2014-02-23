using Simplify.FluentNHibernate.Examples.Domain.Entities;

namespace Simplify.FluentNHibernate.Examples.Domain
{
	public interface IUsersService
	{
		User GetUser(string userName);

		void UpdateUserCity(User user, City city);
	}
}