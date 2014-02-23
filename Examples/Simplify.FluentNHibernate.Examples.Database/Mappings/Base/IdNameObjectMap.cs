using FluentNHibernate.Mapping;

using Simplify.FluentNHibernate.Examples.Domain.Entities.Base;

namespace Simplify.FluentNHibernate.Examples.Database.Mappings.Base
{
	public class IdNameObjectMap<T> : ClassMap<T>
		where T : IdNameObject
	{
		public IdNameObjectMap()
		{
			Id(x => x.ID);
			Map(x => x.Name);
		}
	}
}
