using Simplify.Repository.Model;

namespace Simplify.Repository.FluentNHibernate.Mappings
{
	/// <summary>
	/// Named object mapping
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class NamedObjectMap<T> : IdentityObjectMap<T>
		where T : INamedObject
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="NamedObjectMap{T}"/> class.
		/// </summary>
		public NamedObjectMap()
		{
			Map(x => x.Name).Not.Nullable();
		}
	}
}