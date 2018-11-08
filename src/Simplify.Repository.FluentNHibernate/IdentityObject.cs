namespace Simplify.Repository.FluentNHibernate
{
	/// <summary>
	/// Provides object with identifier
	/// </summary>
	public class IdentityObject : IIdentityObject
	{
		/// <summary>
		/// Gets or sets the identifier.
		/// </summary>
		/// <value>
		/// The identifier.
		/// </value>
		public virtual int ID { get; set; }
	}
}