using FluentNHibernate.Mapping;

using Simplify.FluentNHibernate.Examples.Domain.Entities;

namespace Simplify.FluentNHibernate.Examples.Database.Mappings
{
	public class CityMap : ClassMap<City>
	{
		public CityMap()
		{
			Table("Cities");

			Id(x => x.ID);
			
			HasMany(x => x.CityNames)
				.KeyColumn("CityID")
				.Inverse()
				.Cascade.All()
				.Cascade.AllDeleteOrphan();
		}
	}
}
