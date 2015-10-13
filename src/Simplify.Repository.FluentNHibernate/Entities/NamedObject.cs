using Simplify.Repository.Model;

namespace Simplify.Repository.FluentNHibernate.Entities
{
	/// <summary>
	/// Provides object with name
	/// </summary>
	public class NamedObject : IdentityObject, INamedObject
	{
		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		/// <value>
		/// The name.
		/// </value>
		public virtual string Name { get; set; }
	}
}