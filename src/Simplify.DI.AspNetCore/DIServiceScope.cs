using Microsoft.Extensions.DependencyInjection;
using System;

namespace Simplify.DI.AspNetCore
{
	/// <summary>
	/// Simplify.DI based service scope for Microsoft.Extensions.DependencyInjection
	/// </summary>
	/// <seealso cref="IServiceScope" />
	public class DIServiceScope : IServiceScope
	{
		private readonly ILifetimeScope _scope;

		/// <summary>
		/// Initializes a new instance of the <see cref="DIServiceScope"/> class.
		/// </summary>
		/// <param name="contextHandler">The context handler.</param>
		public DIServiceScope(IDIContextHandler contextHandler)
		{
			_scope = contextHandler.BeginLifetimeScope();
			ServiceProvider = new DIServiceProvider(_scope.Resolver);
		}

		/// <summary>
		/// The <see cref="T:System.IServiceProvider" /> used to resolve dependencies from the scope.
		/// </summary>
		public IServiceProvider ServiceProvider { get; set; }

		/// <summary>
		/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
		/// </summary>
		public void Dispose()
		{
			_scope.Dispose();
		}
	}
}