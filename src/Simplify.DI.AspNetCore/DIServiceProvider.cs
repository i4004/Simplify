using System;

namespace Simplify.DI.AspNetCore
{
	/// <summary>
	/// Simplify.DI based service provider for Microsoft.Extensions.DependencyInjection
	/// </summary>
	/// <seealso cref="IServiceProvider" />
	public class DIServiceProvider : IServiceProvider
	{
		private readonly IDIResolver _resolver;

		/// <summary>
		/// Initializes a new instance of the <see cref="DIServiceProvider"/> class.
		/// </summary>
		/// <param name="resolver">The registrator.</param>
		public DIServiceProvider(IDIResolver resolver)
		{
			_resolver = resolver;
		}

		/// <summary>
		/// Gets the service object of the specified type.
		/// </summary>
		/// <param name="serviceType">An object that specifies the type of service object to get.</param>
		/// <returns>
		/// A service object of type <paramref name="serviceType">serviceType</paramref>.   -or-  null if there is no service object of type <paramref name="serviceType">serviceType</paramref>.
		/// </returns>
		public object GetService(Type serviceType)
		{
			return _resolver.Resolve(serviceType);
		}
	}
}