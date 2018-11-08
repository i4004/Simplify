using System;
using Simplify.FluentNHibernate.Examples.Domain.Accounts;
using Simplify.FluentNHibernate.Examples.Domain.Location;
using Simplify.Repository.FluentNHibernate;

namespace Simplify.FluentNHibernate.Examples.Database.Accounts
{
	public class User : NamedObject, IUser
	{
		public virtual string Password { get; set; }

		public virtual string EMail { get; set; }

		public virtual ICity City { get; set; }

		public virtual DateTime LastActivityTime { get; set; }
	}
}