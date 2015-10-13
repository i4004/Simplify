using FluentNHibernate.Mapping;
using Simplify.Repository.Model;

namespace Simplify.Repository.FluentNHibernate.Mappings
{
	public class IdentityObjectMap<T> : ClassMap<T>
		where T : IIdentityObject
	{
		public IdentityObjectMap()
		{
			Id(x => x.ID);
		}
	}
}