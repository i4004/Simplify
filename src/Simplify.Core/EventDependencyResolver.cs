using System;

namespace Simplify.Core
{
	/// <summary>
	/// Dependency resolver by event to specify custom resolve method and begin lifetime scope method
	/// </summary>
	public class EventDependencyResolver : IDependecyResolver
	{
		/// <summary>
		/// Delegate for resolving dependencies
		/// </summary>
		/// <param name="type">The type.</param>
		/// <returns></returns>
		public delegate object ResolveHandler(Type type);

		/// <summary>
		/// Delegate for begin lifetime scoping
		/// </summary>
		/// <returns></returns>
		public delegate IDisposable BeginLifetimeScopeHandler();

		private event ResolveHandler ResolveMethod;
		private event BeginLifetimeScopeHandler BeginLifetimeScopeMethod;

		/// <summary>
		/// Initializes a new instance of the <see cref="EventDependencyResolver" /> class.
		/// </summary>
		/// <param name="resolveMethod">The dependency resolve method.</param>
		/// <param name="beginLifetimeScopeMethod">The begin lifetime scope method.</param>
		public EventDependencyResolver(ResolveHandler resolveMethod, BeginLifetimeScopeHandler beginLifetimeScopeMethod = null)
		{
			ResolveMethod = resolveMethod;
			BeginLifetimeScopeMethod = beginLifetimeScopeMethod;
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

		/// <summary>
		/// Begins the lifetime scope.
		/// </summary>
		/// <returns></returns>
		public IDisposable BeginLifetimeScope()
		{
			return BeginLifetimeScopeMethod != null ? BeginLifetimeScopeMethod() : null;
		}
	}
}