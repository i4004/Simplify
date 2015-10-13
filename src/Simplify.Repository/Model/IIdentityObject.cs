namespace Simplify.Repository.Model
{
	/// <summary>
	/// Represent object with identifier
	/// </summary>
	public interface IIdentityObject : IHideObjectMembers
	{
		/// <summary>
		/// Gets the identifier.
		/// </summary>
		/// <value>
		/// The identifier.
		/// </value>
		int ID { get; }
	}
}