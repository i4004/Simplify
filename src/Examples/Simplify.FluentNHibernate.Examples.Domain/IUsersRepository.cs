using System;
using System.Linq.Expressions;

using Simplify.FluentNHibernate.Examples.Domain.Entities;

namespace Simplify.FluentNHibernate.Examples.Domain
{
	public interface IUsersRepository
	{
		User GetUser(Expression<Func<User, bool>> filterFunc);
		void Update(User user);
	}
}
