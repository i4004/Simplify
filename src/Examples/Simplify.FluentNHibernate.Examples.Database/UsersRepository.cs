using System;
using System.Linq.Expressions;

using NHibernate;

using Simplify.FluentNHibernate.Examples.Domain;
using Simplify.FluentNHibernate.Examples.Domain.Entities;

namespace Simplify.FluentNHibernate.Examples.Database
{
	public class UsersRepository : IUsersRepository
	{
		private readonly ISession _session;

		public UsersRepository(ISession session)
		{
			_session = session;
		}

		public User GetUser(Expression<Func<User, bool>> filterFunc)
		{
			return _session.GetObject(filterFunc);
		}

		public void Update(User user)
		{
			_session.Update(user);
		}
	}
}
