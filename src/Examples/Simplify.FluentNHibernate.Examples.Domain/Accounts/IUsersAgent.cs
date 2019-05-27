namespace Simplify.FluentNHibernate.Examples.Domain.Accounts
{
	public interface IUsersAgent
	{
		IUser GetUser(string userName);

		void SetUserCity(int userID, int cityID);
	}
}