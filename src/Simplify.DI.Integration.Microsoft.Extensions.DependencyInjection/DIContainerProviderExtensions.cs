using Microsoft.Extensions.DependencyInjection;
using System;

namespace Simplify.DI.Integration.Microsoft.Extensions.DependencyInjection
{
	/// <summary>
	/// Provides IDIContainerProvider extensions for Microsoft.Extensions.DependencyInjection integration
	/// </summary>
	public static class DIContainerProviderExtensions
	{
		/// <summary>
		/// Creates  Simplify.DI wrapper service provider for Microsoft.Extensions.DependencyInjection
		/// </summary>
		/// <param name="provider">The resolver.</param>
		/// <returns></returns>
		/// <exception cref="ArgumentNullException">collection</exception>
		public static IServiceProvider CreateServiceProvider(this IDIContainerProvider provider)
		{
			if (provider == null) throw new ArgumentNullException(nameof(provider));

			var serviceProvider = new DIServiceProvider(provider);

			// Registering required IOC container types wrappers in itself

			provider.Register<IServiceProvider>(x => serviceProvider, LifetimeType.Singleton);
			provider.Register<IServiceScopeFactory>(x => new DIServiceScopeFactory(provider), LifetimeType.Singleton);

			return serviceProvider;
		}

		/// <summary>
		/// Integrates Simplify.DI with Microsoft Dependency Injection system.
		/// </summary>
		/// <param name="provider">The provider.</param>
		/// <param name="services">The services.</param>
		/// <returns></returns>
		public static IServiceProvider IntegrateWithMicrosoftDependencyInjection(this IDIContainerProvider provider, IServiceCollection services)
		{
			provider.RegisterFromServiceCollection(services);

			return provider.CreateServiceProvider();
		}

		/// <summary>
		/// Integrates Simplify.DI with Microsoft Dependency Injection system and verifies Simplify.DI provider.
		/// </summary>
		/// <param name="provider">The provider.</param>
		/// <param name="services">The services.</param>
		/// <returns></returns>
		public static IServiceProvider IntegrateWithMicrosoftDependencyInjectionAndVerify(this IDIContainerProvider provider, IServiceCollection services)
		{
			var serviceProvider = provider.IntegrateWithMicrosoftDependencyInjection(services);

			provider.Verify();

			return serviceProvider;
		}
	}
}