using System;
using DryIoc;

namespace Simplify.DI.Provider.DryIoc
{
	/// <summary>
	/// Providers DryIoc resolver
	/// </summary>
	/// <seealso cref="IDIResolver" />
	public class DryIocDIResolver : IDIResolver
	{
		private readonly IResolverContext _resolverContext;

		/// <summary>
		/// Initializes a new instance of the <see cref="DryIocDIResolver"/> class.
		/// </summary>
		/// <param name="resolverContext">The resolver context.</param>
		public DryIocDIResolver(IResolverContext resolverContext)
		{
			_resolverContext = resolverContext;
		}

		/// <summary>
		/// Resolves the specified type.
		/// </summary>
		/// <param name="type">The type.</param>
		/// <returns></returns>
		public object Resolve(Type type)
		{
			return _resolverContext.Resolve(type);
		}
	}
}