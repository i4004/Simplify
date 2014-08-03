using System;

using Simplify.FluentNHibernate.Examples.Domain.Entities;

namespace Simplify.FluentNHibernate.Examples.Domain
{
	public class UsersService : IUsersService
	{
		private readonly IUsersRepository _repository;

		public UsersService(IUsersRepository repository)
		{
			_repository = repository;
		}

		public User GetUser(string userName)
		{
			if (userName == null) throw new ArgumentNullException("userName");

			return _repository.GetUser(x => x.Name == userName);
		}

		public void UpdateUserCity(User user, City city)
		{
			if (user == null) throw new ArgumentNullException("user");
			if (city == null) throw new ArgumentNullException("city");

			user.City = city;

			_repository.Update(user);
		}
	}
}
