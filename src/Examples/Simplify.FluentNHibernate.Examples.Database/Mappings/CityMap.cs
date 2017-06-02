using Simplify.FluentNHibernate.Examples.Database.Entities.Location;
using Simplify.Repository.FluentNHibernate.Mappings;

namespace Simplify.FluentNHibernate.Examples.Database.Mappings
{
	public class CityMap : IdentityObjectMap<City>
	{
		public CityMap()
		{
			Table("Cities");

			HasMany<CityName>(x => x.CityNames)
				.KeyColumn("CityID")
				.Inverse()
				.Cascade.All()
				.Cascade.AllDeleteOrphan();
		}
	}
}