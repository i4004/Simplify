using Simplify.FluentNHibernate.Examples.Database.Location;
using Simplify.Repository.FluentNHibernate.Mappings;

namespace Simplify.FluentNHibernate.Examples.Database.Mappings
{
	public class CityNameMap : NamedObjectMap<CityName>
	{
		public CityNameMap()
		{
			Table("CitiesNames");

			References<City>(x => x.City, "CityID");
			Map(x => x.Language);
		}
	}
}