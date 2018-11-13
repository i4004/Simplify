using System;

namespace Simplify.DI
{
	/// <summary>
	/// Represents DI container resolver
	/// </summary>
	public interface IDIResolver
	{
		/// <summary>
		/// Resolves the specified type.
		/// </summary>
		/// <param name="type">The type.</param>
		/// <returns></returns>
		object Resolve(Type type);
	}
}