using Simplify.FluentNHibernate.Examples.Domain.Model.Accounts;

namespace Simplify.FluentNHibernate.Examples.Domain.Accounts
{
	public interface IUsersService
	{
		IUser GetUser(string userName);

		void SetUserCity(int userID, int cityID);
	}
}