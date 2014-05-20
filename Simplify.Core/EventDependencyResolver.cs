using System;

namespace Simplify.Core
{
	/// <summary>
	/// Dependency resolver by event class
	/// </summary>
	public class EventDependencyResolver : IDependecyResolver
	{
		/// <summary>
		/// Delegate for resolving dependencies
		/// </summary>
		/// <param name="type">The type.</param>
		/// <returns></returns>
		public delegate object Resolver(Type type);

		private event Resolver ResolveMethod;

		/// <summary>
		/// Initializes a new instance of the <see cref="EventDependencyResolver"/> class.
		/// </summary>
		/// <param name="resolveMethod">The resolve method.</param>
		public EventDependencyResolver(Resolver resolveMethod)
		{
			ResolveMethod = resolveMethod;
		}

		/// <summary>
		/// Resolves the specified type.
		/// </summary>
		/// <param name="type">The type.</param>
		/// <returns></returns>
		public object Resolve(Type type)
		{
			return ResolveMethod(type);
		}
	}
}