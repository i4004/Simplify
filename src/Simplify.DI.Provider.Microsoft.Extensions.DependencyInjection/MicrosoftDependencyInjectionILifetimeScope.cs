using Microsoft.Extensions.DependencyInjection;

namespace Simplify.DI.Provider.Microsoft.Extensions.DependencyInjection
{
	/// <summary>
	/// Microsoft.DependencyInjection DI provider lifetime scope implementation
	/// </summary>
	public class MicrosoftDependencyInjectionILifetimeScope : ILifetimeScope
	{
		private readonly IServiceScope _scope;

		/// <summary>
		/// Initializes a new instance of the <see cref="MicrosoftDependencyInjectionILifetimeScope"/> class.
		/// </summary>
		/// <param name="provider">The provider.</param>
		public MicrosoftDependencyInjectionILifetimeScope(MicrosoftDependencyInjectionDIProvider provider)
		{
			_scope = provider.ServiceProvider.CreateScope();
			Resolver = new MicrosoftDependencyInjectionDIResolver(_scope.ServiceProvider);
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