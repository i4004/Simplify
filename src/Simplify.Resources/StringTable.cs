using System;

namespace Simplify.Resources
{
	/// <summary>
	/// Provides application string tables access
	/// </summary>
	public static class StringTable
	{
		private static IResourcesStringTable _entryStringTableInstance;

		/// <summary>
		/// Entry assembly string table (ProgramResources.resx)
		/// </summary>
		public static IResourcesStringTable Entry
		{
			get { return _entryStringTableInstance ?? (_entryStringTableInstance = new ResourcesStringTable(false, "ProgramResources")); }
			set
			{
				if(value == null)
					throw new ArgumentNullException();

				_entryStringTableInstance = value;
			}
		}
	}
}
