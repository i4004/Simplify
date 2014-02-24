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

		///// <summary>
		///// Gets the enum associated value from the resource file.
		///// </summary>
		///// <typeparam name="T"></typeparam>
		///// <param name="enumValue">The enum value.</param>
		///// <returns></returns>
		//public static string GetAssociatedValue<T>(T enumValue) where T : struct
		//{
		//	return EnumStringTableBinder.GetAssociatedValue(enumValue, Entry);
		//}
	}
}
