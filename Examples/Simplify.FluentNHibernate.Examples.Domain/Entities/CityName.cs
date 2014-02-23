using Simplify.FluentNHibernate.Examples.Domain.Entities.Base;

namespace Simplify.FluentNHibernate.Examples.Domain.Entities
{
	public class CityName : IdNameObject
	{
		public virtual City City { get; set; }
		public virtual string Language { get; set; }
	}
}