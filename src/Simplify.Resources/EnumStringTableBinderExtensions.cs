using System;
using System.Collections.Generic;

namespace Simplify.Resources
{
	/// <summary>
	/// String table binder, converts enum to <c>KeyValuePair</c> with associated string table values
	/// </summary>
	public static class EnumStringTableBinderExtensions
	{
		/// <summary>
		/// Get enum associated value from string table by enum type + enum element name
		/// </summary>
		/// <typeparam name="T">Enum</typeparam>
		/// <param name="stringTable">The string table.</param>
		/// <param name="enumValue">Enum value</param>
		/// <returns>
		/// associated value
		/// </returns>
		public static string GetAssociatedValue<T>(this IResourcesStringTable stringTable, T enumValue) where T : struct
		{
			return stringTable[enumValue.GetType().Name + Enum.GetName(typeof(T), enumValue)] ?? "";
		}

		/// <summary>
		/// Generates <c>KeyValuePair</c> list with ID from enum and associated name from <c>StringTable</c>
		/// </summary>
		/// <returns>List of <c>KeyValuePair</c> pairs</returns>
		public static IList<KeyValuePair<T, string>> GetKeyValuePairList<T>(this IResourcesStringTable stringTable) where T : struct
		{
			var list = new List<KeyValuePair<T, string>>();

			foreach (T item in Enum.GetValues(typeof(T)))
				list.Add(new KeyValuePair<T, string>(item, stringTable.GetAssociatedValue(item)));

			return list;
		}
	}
}
