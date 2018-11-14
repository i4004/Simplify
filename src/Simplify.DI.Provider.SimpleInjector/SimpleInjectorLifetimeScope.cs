using SimpleInjector;
using SimpleInjector.Lifestyles;

namespace Simplify.DI.Provider.SimpleInjector
{
	/// <summary>
	/// Simple Injector DI provider lifetime scope implementation
	/// </summary>
	public class SimpleInjectorLifetimeScope : ILifetimeScope
	{
		private readonly Scope _scope;

		/// <summary>
		/// Initializes a new instance of the <see cref="SimpleInjectorLifetimeScope"/> class.
		/// </summary>
		/// <param name="provider">The provider.</param>
		public SimpleInjectorLifetimeScope(SimpleInjectorDIProvider provider)
		{
			Resolver = provider;

			_scope = AsyncScopedLifestyle.BeginScope(provider.Container);
		}

		/// <summary>
		/// Gets the DI container resolver (should be used to resolve types when using scoping).
		/// </summary>
		/// <value>
		/// The DI container resolver (should be used to resolve types when using scoping).
		/// </value>
		public IDIResolver Resolver { get; }

		/// <summary>
		/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
		/// </summary>
		public void Dispose()
		{
			_scope.Dispose();
		}
	}
}