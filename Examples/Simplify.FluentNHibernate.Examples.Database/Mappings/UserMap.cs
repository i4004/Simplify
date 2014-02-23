using FluentNHibernate.Mapping;

using Simplify.FluentNHibernate.Examples.Domain.Entities;

namespace Simplify.FluentNHibernate.Examples.Database.Mappings
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
			References(x => x.City, "CityID");
			Map(x => x.LastActivityTime);
		}
	}
}
