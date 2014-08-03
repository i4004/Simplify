using Simplify.FluentNHibernate.Examples.Database.Mappings.Base;
using Simplify.FluentNHibernate.Examples.Domain.Entities;

namespace Simplify.FluentNHibernate.Examples.Database.Mappings
{
	public class CityNameMap : IdNameObjectMap<CityName>
	{
		public CityNameMap()
		{
			Table("CitiesNames");

			References(x => x.City, "CityID");
			Map(x => x.Language);
		}
	}
}
