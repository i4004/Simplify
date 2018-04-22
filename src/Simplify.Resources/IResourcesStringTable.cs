namespace Simplify.Resources
{
	/// <summary>
	/// Interface for getting assembly resource file string
	/// </summary>
	public interface IResourcesStringTable
	{
		/// <summary>
		/// Get string table record by name
		/// </summary>
		string this[string name] { get; }

		/// <summary>
		/// Get string table record by name
		/// </summary>
		string GetString(string name);
	}
}