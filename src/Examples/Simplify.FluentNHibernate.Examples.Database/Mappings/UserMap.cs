using Simplify.FluentNHibernate.Examples.Database.Accounts;
using Simplify.FluentNHibernate.Examples.Database.Location;
using Simplify.Repository.FluentNHibernate.Mappings;

namespace Simplify.FluentNHibernate.Examples.Database.Mappings
{
	public class UserMap : NamedObjectMap<User>
	{
		public UserMap()
		{
			Table("Users");

			Map(x => x.Password);

			Map(x => x.EMail);

			References<City>(x => x.City);

			Map(x => x.LastActivityTime);
		}
	}
}