using Simplify.FluentNHibernate.Examples.Domain.Location;
using Simplify.Repository.FluentNHibernate;

namespace Simplify.FluentNHibernate.Examples.Database.Location
{
	public class CityName : NamedObject, ICityName
	{
		public virtual ICity City { get; set; }

		public virtual string Language { get; set; }
	}
}