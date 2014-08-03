using System.ComponentModel;

namespace Simplify.Resources
{
	/// <summary>
	/// Class for for setting custom resource file name
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class CustomComponentResourceManager<T> : ComponentResourceManager
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="CustomComponentResourceManager{T}"/> class.
		/// </summary>
		/// <param name="resourceName">Name of the resource file.</param>
		public CustomComponentResourceManager(string resourceName)
			: base(typeof(T))
		{
			BaseNameField = resourceName;
		}
	}
}
