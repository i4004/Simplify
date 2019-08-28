using Microsoft.Extensions.DependencyInjection;

namespace Simplify.DI.AspNetCore
{
	/// <summary>
	/// Simplify.DI based service scope factory for Microsoft.Extensions.DependencyInjection
	/// </summary>
	/// <seealso cref="IServiceScopeFactory" />
	public class DIServiceScopeFactory : IServiceScopeFactory
	{
		private readonly IDIContextHandler _contextHandler;

		/// <summary>
		/// Initializes a new instance of the <see cref="DIServiceScopeFactory"/> class.
		/// </summary>
		/// <param name="contextHandler">The context handler.</param>
		public DIServiceScopeFactory(IDIContextHandler contextHandler)
		{
			_contextHandler = contextHandler;
		}

		/// <summary>
		/// Create an <see cref="T:Microsoft.Extensions.DependencyInjection.IServiceScope" /> which
		/// contains an <see cref="T:System.IServiceProvider" /> used to resolve dependencies from a
		/// newly created scope.
		/// </summary>
		/// <returns>
		/// An <see cref="T:Microsoft.Extensions.DependencyInjection.IServiceScope" /> controlling the
		/// lifetime of the scope. Once this is disposed, any scoped services that have been resolved
		/// from the <see cref="P:Microsoft.Extensions.DependencyInjection.IServiceScope.ServiceProvider" />
		/// will also be disposed.
		/// </returns>
		public IServiceScope CreateScope()
		{
			return new DIServiceScope(_contextHandler);
		}
	}
}