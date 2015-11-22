using FluentNHibernate.Mapping;
using Simplify.Repository.Model;

namespace Simplify.Repository.FluentNHibernate.Mappings
{
	/// <summary>
	/// Identity object mapping
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class IdentityObjectMap<T> : ClassMap<T>
		where T : IIdentityObject
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="IdentityObjectMap{T}"/> class.
		/// </summary>
		public IdentityObjectMap()
		{
			Id(x => x.ID);
		}
	}
}