using Simplify.Repository.Model;

namespace Simplify.Repository.FluentNHibernate.Mappings
{
	public class NamedObjectMap<T> : IdentityObjectMap<T>
		where T : INamedObject
	{
		public NamedObjectMap()
		{
			Map(x => x.Name).Not.Nullable();
		}
	}
}