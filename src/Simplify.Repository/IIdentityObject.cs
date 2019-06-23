namespace Simplify.Repository
{
	/// <summary>
	/// Represent object with identifier
	/// </summary>
	public interface IIdentityObject
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