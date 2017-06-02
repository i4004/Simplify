using System;
using Simplify.FluentNHibernate.Examples.Domain.Model.Accounts;
using Simplify.FluentNHibernate.Examples.Domain.Model.Location;
using Simplify.Repository.FluentNHibernate.Entities;

namespace Simplify.FluentNHibernate.Examples.Database.Entities.Accounts
{
	public class User : NamedObject, IUser
	{
		public virtual string Password { get; set; }

		public virtual string EMail { get; set; }

		public virtual ICity City { get; set; }

		public virtual DateTime LastActivityTime { get; set; }
	}
}