using System.Data;

namespace Simplify.FluentNHibernate.Examples.Domain.Accounts
{
	public class TransactUsersService : IUsersService
	{
		private readonly IUsersService _baseService;
		private readonly IExampleUnitOfWork _unitOfWork;

		public TransactUsersService(IUsersService baseService, IExampleUnitOfWork unitOfWork)
		{
			_baseService = baseService;
			_unitOfWork = unitOfWork;
		}

		public IUser GetUser(string userName)
		{
			_unitOfWork.BeginTransaction(IsolationLevel.ReadUncommitted);

			var item = _baseService.GetUser(userName);

			_unitOfWork.Commit();

			return item;
		}

		public void SetUserCity(int userID, int cityID)
		{
			_unitOfWork.BeginTransaction();

			_baseService.SetUserCity(userID, cityID);

			_unitOfWork.Commit();
		}
	}
}