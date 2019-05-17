using Microsoft.Extensions.DependencyInjection;
using Simplify.DI.Provider.DryIoc;
using System;

namespace Simplify.DI.Provider.Microsoft.Extensions.DependencyInjection
{
	/// <summary>
	/// Providers Microsoft.DependencyInjection resolver
	/// </summary>
	/// <seealso cref="IDIResolver" />
	public class MicrosoftDependencyInjectionDIResolver : IDIResolver
	{
		private readonly IServiceProvider _serviceProvider;

		/// <summary>
		/// Initializes a new instance of the <see cref="DryIocDIResolver"/> class.
		/// </summary>
		/// <param name="serviceProvider">The resolver service provider.</param>
		public MicrosoftDependencyInjectionDIResolver(IServiceProvider serviceProvider)
		{
			_serviceProvider = serviceProvider;
		}

		/// <summary>
		/// Resolves the specified type.
		/// </summary>
		/// <param name="type">The type.</param>
		/// <returns></returns>
		public object Resolve(Type type)
		{
			return _serviceProvider.GetRequiredService(type);
		}
	}
}