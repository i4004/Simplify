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
				RegisterServiceDescriptor(registrator, item);
		}

		private static void RegisterServiceDescriptor(IDIRegistrator registrator, ServiceDescriptor item)
		{
			if (item.ImplementationInstance != null)
			{
				RegisterInstanceDescriptors(registrator, item);
				return;
			}

			if (item.ImplementationFactory != null)
			{
				RegisterDelegateDescriptors(registrator, item);
				return;
			}

			RegisterStandardDescriptors(registrator, item);
		}

		private static void RegisterStandardDescriptors(IDIRegistrator registrator, ServiceDescriptor item)
		{
			if (item.ImplementationType == null)
			{
				RegisterOnlyService(registrator, item);
				return;
			}

			RegisterServiceWithImplementation(registrator, item);
		}

		private static void RegisterDelegateDescriptors(IDIRegistrator registrator, ServiceDescriptor item)
		{
			if (item.ImplementationType == null)
			{
				RegisterDelegate(registrator, item);
				return;
			}

			RegisterServiceWithDelegate(registrator, item);
		}

		private static void RegisterInstanceDescriptors(IDIRegistrator registrator, ServiceDescriptor item)
		{
			if (item.ImplementationType == null)
			{
				RegisterInstance(registrator, item);
				return;
			}

			RegisterServiceWithInstance(registrator, item);
		}

		private static void RegisterOnlyService(IDIRegistrator registrator, ServiceDescriptor item)
		{
			switch (item.Lifetime)
			{
				case ServiceLifetime.Scoped:
					registrator.Register(item.ServiceType);
					break;

				case ServiceLifetime.Singleton:
					registrator.Register(item.ServiceType, LifetimeType.Singleton);
					break;

				case ServiceLifetime.Transient:
					registrator.Register(item.ServiceType, LifetimeType.Transient);
					break;

				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		private static void RegisterServiceWithImplementation(IDIRegistrator registrator, ServiceDescriptor item)
		{
			switch (item.Lifetime)
			{
				case ServiceLifetime.Scoped:
					registrator.Register(item.ServiceType, item.ImplementationType);
					break;

				case ServiceLifetime.Singleton:
					registrator.Register(item.ServiceType, item.ImplementationType, LifetimeType.Singleton);
					break;

				case ServiceLifetime.Transient:
					registrator.Register(item.ServiceType, item.ImplementationType, LifetimeType.Transient);
					break;

				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		private static void RegisterDelegate(IDIRegistrator registrator, ServiceDescriptor item)
		{
			switch (item.Lifetime)
			{
				case ServiceLifetime.Scoped:
					registrator.Register(x => item.ImplementationFactory(new DIServiceProvider(x)));
					break;

				case ServiceLifetime.Singleton:
					registrator.Register(x => item.ImplementationFactory(new DIServiceProvider(x)), LifetimeType.Singleton);
					break;

				case ServiceLifetime.Transient:
					registrator.Register(x => item.ImplementationFactory(new DIServiceProvider(x)), LifetimeType.Transient);
					break;

				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		private static void RegisterServiceWithDelegate(IDIRegistrator registrator, ServiceDescriptor item)
		{
			// TODO not available
			//switch (item.Lifetime)
			//{
			//	case ServiceLifetime.Scoped:
			//		registrator.Register(item.ServiceType, x => item.ImplementationFactory(new DIServiceProvider(x)));
			//		break;

			//	case ServiceLifetime.Singleton:
			//		registrator.Register(item.ServiceType, x => item.ImplementationFactory(new DIServiceProvider(x)), LifetimeType.Singleton);
			//		break;

			//	case ServiceLifetime.Transient:
			//		registrator.Register(item.ServiceType, x => item.ImplementationFactory(new DIServiceProvider(x)), LifetimeType.Transient);
			//		break;

			//	default:
			//		throw new ArgumentOutOfRangeException();
			//}

			throw new NotImplementedException();
		}

		private static void RegisterInstance(IDIRegistrator registrator, ServiceDescriptor item)
		{
			switch (item.Lifetime)
			{
				case ServiceLifetime.Scoped:
					registrator.Register(x => item.ImplementationInstance);
					break;

				case ServiceLifetime.Singleton:
					registrator.Register(x => item.ImplementationInstance, LifetimeType.Singleton);
					break;

				case ServiceLifetime.Transient:
					registrator.Register(x => item.ImplementationInstance, LifetimeType.Transient);
					break;

				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		private static void RegisterServiceWithInstance(IDIRegistrator registrator, ServiceDescriptor item)
		{
			// TODO not available
			//switch (item.Lifetime)
			//{
			//	case ServiceLifetime.Scoped:
			//		registrator.Register(item.ServiceType, x => item.ImplementationInstance);
			//		break;

			//	case ServiceLifetime.Singleton:
			//		registrator.Register(item.ServiceType, x => item.ImplementationInstance, LifetimeType.Singleton);
			//		break;

			//	case ServiceLifetime.Transient:
			//		registrator.Register(item.ServiceType, x => item.ImplementationInstance, LifetimeType.Transient);
			//		break;

			//	default:
			//		throw new ArgumentOutOfRangeException();
			//}

			throw new NotImplementedException();
		}
	}
}