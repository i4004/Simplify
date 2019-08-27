using System;

namespace Simplify.DI.AspNetCore
{
	/// <summary>
	/// Provides IDIResolver extensions for Microsoft.Extensions.DependencyInjection integration
	/// </summary>
	public static class DIResolverExtensions
	{
		/// <summary>
		/// Creates  Simplify.DI wrapper service provider for Microsoft.Extensions.DependencyInjection
		/// </summary>
		/// <param name="resolver">The resolver.</param>
		/// <returns></returns>
		/// <exception cref="ArgumentNullException">collection</exception>
		public static IServiceProvider CreateServiceProvider(this IDIResolver resolver)
		{
			if (resolver == null) throw new ArgumentNullException(nameof(resolver));

			return new DIServiceProvider(resolver);
		}
	}
}