using Microsoft.Extensions.DependencyInjection;
using System;

namespace Simplify.DI.AspNetCore
{
	/// <summary>
	/// Provides IDIRegistrator extensions for Microsoft.Extensions.DependencyInjection integration
	/// </summary>
	public static class DIRegistratorExtensions
	{
		/// <summary>
		/// Registers the types from IServiceCollection inside IDIRegistrator (IDIContainerProvider).
		/// </summary>
		/// <param name="registrator">The registrator.</param>
		/// <param name="services">The services collection.</param>
		/// <exception cref="ArgumentNullException">collection</exception>
		public static void RegisterFromServiceCollection(this IDIRegistrator registrator, IServiceCollection services)
		{
			if (registrator == null) throw new ArgumentNullException(nameof(registrator));
			if (services == null) throw new ArgumentNullException(nameof(services));

			foreach (var item in services)
				RegisterServiceDescriptor(registrator, item, GetLifetime(item.Lifetime));
		}

		private static LifetimeType GetLifetime(ServiceLifetime lifetime)
		{
			switch (lifetime)
			{
				case ServiceLifetime.Scoped:
					return LifetimeType.PerLifetimeScope;

				case ServiceLifetime.Singleton:
					return LifetimeType.Singleton;

				case ServiceLifetime.Transient:
					return LifetimeType.Transient;

				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		private static void RegisterServiceDescriptor(IDIRegistrator registrator, ServiceDescriptor item, LifetimeType lifetime)
		{
			if (item.ImplementationInstance != null)
			{
				RegisterInstanceDescriptors(registrator, item, lifetime);
				return;
			}

			if (item.ImplementationFactory != null)
			{
				RegisterDelegateDescriptors(registrator, item, lifetime);
				return;
			}

			RegisterStandardDescriptors(registrator, item, lifetime);
		}

		private static void RegisterStandardDescriptors(IDIRegistrator registrator, ServiceDescriptor item, LifetimeType lifetime)
		{
			if (item.ImplementationType == null)
			{
				registrator.Register(item.ServiceType, lifetime);
				return;
			}

			registrator.Register(item.ServiceType, item.ImplementationType, lifetime);
		}

		private static void RegisterDelegateDescriptors(IDIRegistrator registrator, ServiceDescriptor item, LifetimeType lifetime)
		{
			if (item.ImplementationType == null)
			{
				registrator.Register(x => item.ImplementationFactory(new DIServiceProvider(x)), lifetime);
				return;
			}

			registrator.Register(item.ServiceType, x => item.ImplementationFactory(new DIServiceProvider(x)), lifetime);
		}

		private static void RegisterInstanceDescriptors(IDIRegistrator registrator, ServiceDescriptor item, LifetimeType lifetime)
		{
			if (item.ImplementationType == null)
			{
				registrator.Register(item.ImplementationInstance.GetType(), x => item.ImplementationInstance, lifetime);
				return;
			}

			registrator.Register(item.ServiceType, x => item.ImplementationInstance, lifetime);
		}
	}
}