using System;

namespace Simplify.Core
{
	/// <summary>
	/// Default denependency resolver
	/// </summary>
	public class DefaultDependencyResolver : IDependecyResolver
	{
		/// <summary>
		/// Resolves the specified type.
		/// </summary>
		/// <param name="type">The type.</param>
		/// <returns></returns>
		public object Resolve(Type type)
		{
			return Activator.CreateInstance(type);
		}

		/// <summary>
		/// Begins the lifetime scope for the given container.
		/// </summary>
		/// <returns></returns>
		public IDisposable BeginLifetimeScope()
		{
			return null;
		}
	}
}