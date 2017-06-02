using System;
using Simplify.FluentNHibernate.Examples.Domain.Model.Location;
using Simplify.Repository.Model;

namespace Simplify.FluentNHibernate.Examples.Domain.Model.Accounts
{
	public interface IUser : INamedObject
	{
		string Password { get; set; }

		string EMail { get; set; }

		ICity City { get; set; }

		DateTime LastActivityTime { get; set; }
	}
}