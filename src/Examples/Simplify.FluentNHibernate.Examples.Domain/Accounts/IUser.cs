using System;
using Simplify.FluentNHibernate.Examples.Domain.Location;
using Simplify.Repository;

namespace Simplify.FluentNHibernate.Examples.Domain.Accounts
{
	public interface IUser : INamedObject
	{
		string Password { get; set; }

		string EMail { get; set; }

		ICity City { get; set; }

		DateTime LastActivityTime { get; set; }
	}
}