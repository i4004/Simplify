using System;
using Simplify.FluentNHibernate.Examples.Domain.Location;
using Simplify.Repository;

namespace Simplify.FluentNHibernate.Examples.Domain.Accounts
{
	public class UsersService : IUsersService
	{
		private readonly IGenericRepository<IUser> _repository;
		private readonly IGenericRepository<ICity> _citiesRepository;

		public UsersService(IGenericRepository<IUser> repository, IGenericRepository<ICity> citiesRepository)
		{
			_repository = repository;
			_citiesRepository = citiesRepository;
		}

		public IUser GetUser(string userName)
		{
			if (userName == null) throw new ArgumentNullException(nameof(userName));

			return _repository.GetSingleByQuery(x => x.Name == userName);
		}

		public void SetUserCity(int userID, int cityID)
		{
			var user = _repository.GetSingleByID(userID);
			var city = _citiesRepository.GetSingleByID(cityID);

			if (user == null)
				throw new Exception($"User with ID: {userID} is not found");

			if (city == null)
				throw new Exception($"City with ID: {userID} is not found");

			user.City = city;

			_repository.Update(user);
		}
	}
}