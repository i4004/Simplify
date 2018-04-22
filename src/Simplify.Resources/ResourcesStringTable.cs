using System.Reflection;
using System.Resources;

namespace Simplify.Resources
{
	/// <summary>
	/// Class for getting assembly resource file string
	/// </summary>
	public class ResourcesStringTable : IResourcesStringTable
	{
		private readonly Assembly _workingAssembly;
		private ResourceManager _resourceManager;

		/// <summary>
		/// Initializes ResourcesStringTable with calling assembly string table
		/// </summary>
		/// <param name="callingAssembly">If true then initializes ResourcesStringTable with calling assembly string table otherwise with entry assembly string table</param>
		/// <param name="resourcesFileName">Resources file name</param>
		/// <param name="baseName">The root name of the resources (Assembly name will be used by default).</param>
		public ResourcesStringTable(bool callingAssembly, string resourcesFileName = "Resources", string baseName = null)
		{
			_workingAssembly = callingAssembly ? Assembly.GetCallingAssembly() : Assembly.GetEntryAssembly();

			InitializeResourceManager(resourcesFileName, baseName);
		}

		/// <summary>
		/// Initializes ResourcesStringTable with specified assembly string table
		/// </summary>
		/// <param name="assembly">Assembly to get string table from</param>
		/// <param name="resourcesFileName">Resources file name</param>
		/// <param name="baseName">The root name of the resources (Assembly name will be used by default).</param>
		public ResourcesStringTable(Assembly assembly, string resourcesFileName = "Resources", string baseName = null)
		{
			_workingAssembly = assembly;
			InitializeResourceManager(resourcesFileName, baseName);
		}

		/// <summary>
		/// Get string table record by name
		/// </summary>
		public string this[string name] => GetString(name);

		/// <summary>
		/// Get string table record by name
		/// </summary>
		public string GetString(string name)
		{
			return _resourceManager.GetString(name);
		}

		private void InitializeResourceManager(string resourcesFileName = "Resources", string baseName = null)
		{
			if (baseName == null)
				baseName = _workingAssembly.GetName().Name;

			_resourceManager = new ResourceManager(baseName + "." + resourcesFileName, _workingAssembly);
		}
	}
}