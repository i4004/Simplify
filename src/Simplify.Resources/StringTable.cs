using System;

namespace Simplify.Resources
{
	/// <summary>
	/// Provides application string tables acccess
	/// </summary>
	public static class StringTable
	{
		private static IResourcesStringTable EntryStringTableInstance;

		/// <summary>
		/// Entry assembly string table (ProgramResources.resx)
		/// </summary>
		public static IResourcesStringTable Entry
		{
			get { return EntryStringTableInstance ?? (EntryStringTableInstance = new ResourcesStringTable(false, "ProgramResources")); }
			set
			{
				if(value == null)
					throw new ArgumentNullException();

				EntryStringTableInstance = value;
			}
		}
	}
}
