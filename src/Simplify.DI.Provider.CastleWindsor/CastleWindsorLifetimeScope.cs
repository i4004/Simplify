using System;
using Castle.MicroKernel.Lifestyle;

namespace Simplify.DI.Provider.CastleWindsor
{
	/// <summary>
	/// Castle Windsor DI provider lifetime scope implementation
	/// </summary>
	public class CastleWindsorLifetimeScope : ILifetimeScope
	{
		private readonly IDisposable _scope;

		/// <summary>
		/// Initializes a new instance of the <see cref="CastleWindsorLifetimeScope"/> class.
		/// </summary>
		/// <param name="provider">The provider.</param>
		public CastleWindsorLifetimeScope(CastleWindsorDIProvider provider)
		{
			Resolver = provider;

			_scope = provider.Container.BeginScope();
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