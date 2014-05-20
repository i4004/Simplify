using System;

namespace Simplify.Core
{
	/// <summary>
	/// Represents dependenct resolver
	/// </summary>
	public interface IDependecyResolver : IHideObjectMembers
	{
		/// <summary>
		/// Resolves the specified type.
		/// </summary>
		/// <param name="type">The type.</param>
		/// <returns></returns>
		object Resolve(Type type);
	}
}