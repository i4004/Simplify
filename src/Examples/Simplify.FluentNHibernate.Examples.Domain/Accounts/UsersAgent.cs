using System.Data;

namespace Simplify.FluentNHibernate.Examples.Domain.Accounts
{
	public class UsersAgent : IUsersAgent
	{
		private readonly IUsersService _service;
		private readonly IExampleUnitOfWork _unitOfWork;

		public UsersAgent(IUsersService service, IExampleUnitOfWork unitOfWork)
		{
			_service = service;
			_unitOfWork = unitOfWork;
		}

		public IUser GetUser(string userName)
		{
			_unitOfWork.BeginTransaction(IsolationLevel.ReadUncommitted);

			var item = _service.GetUser(userName);

			_unitOfWork.Commit();

			return item;
		}

		public void SetUserCity(int userID, int cityID)
		{
			_unitOfWork.BeginTransaction();

			_service.SetUserCity(userID, cityID);

			_unitOfWork.Commit();
		}
	}
}