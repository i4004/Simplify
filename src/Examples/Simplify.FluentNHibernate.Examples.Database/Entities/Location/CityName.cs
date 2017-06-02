using Simplify.FluentNHibernate.Examples.Domain.Model.Location;
using Simplify.Repository.FluentNHibernate.Entities;

namespace Simplify.FluentNHibernate.Examples.Database.Entities.Location
{
	public class CityName : NamedObject, ICityName
	{
		public virtual ICity City { get; set; }

		public virtual string Language { get; set; }
	}
}