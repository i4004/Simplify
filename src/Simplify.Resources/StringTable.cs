using System;

namespace Simplify.Resources
{
	/// <summary>
	/// Provides application string tables access
	/// </summary>
	public static class StringTable
	{
		private static IResourcesStringTable _entryStringTable;

		/// <summary>
		/// Entry assembly string table (ProgramResources.resx)
		/// </summary>
		public static IResourcesStringTable Entry
		{
			get => _entryStringTable ?? (_entryStringTable = new ResourcesStringTable(false, "ProgramResources"));
			set => _entryStringTable = value ?? throw new ArgumentNullException();
		}
	}
}