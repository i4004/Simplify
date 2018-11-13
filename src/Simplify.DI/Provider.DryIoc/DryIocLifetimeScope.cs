using DryIoc;

namespace Simplify.DI.Provider.DryIoc
{
	/// <summary>
	/// DryIoc DI provider lifetime scope implementation
	/// </summary>
	public class DryIocLifetimeScope : ILifetimeScope
	{
		private readonly IResolverContext _context;

		/// <summary>
		/// Initializes a new instance of the <see cref="DryIocLifetimeScope"/> class.
		/// </summary>
		/// <param name="provider">The provider.</param>
		public DryIocLifetimeScope(DryIocDIProvider provider)
		{
			_context = provider.Container.OpenScope();
			Resolver = new DryIocDIResolver(_context);
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
			_context.Dispose();
		}
	}
}