namespace Simplify.Repository.Model
{
	/// <summary>
	/// Represent object with name
	/// </summary>
	public interface INamedObject : IIdentityObject
	{
		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		/// <value>
		/// The name.
		/// </value>
		string Name { get; set; }
	}
}