using FluentNHibernate.Mapping;
using Simplify.FluentNHibernate.Tests.Entities.Accounts;

namespace Simplify.FluentNHibernate.Tests.Mappings.Accounts
{
	public class UserMap : ClassMap<User>
	{
		public UserMap()
		{
			Table("Users");

			Id(x => x.ID);

			Map(x => x.Name);

			Map(x => x.Password);
			Map(x => x.EMail);
			Map(x => x.LastActivityTime);
		}
	}
}