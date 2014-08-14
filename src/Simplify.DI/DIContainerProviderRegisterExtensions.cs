using System;

namespace Simplify.DI
{
	/// <summary>
	/// Provides DI container provider register extensions
	/// </summary>
	public static class DIContainerProviderRegisterExtensions
	{
		/// <summary>
		/// Registers the specified concrete type for resolve.
		/// </summary>
		/// <param name="provider">The DI provider.</param>
		/// <param name="concreteType">Concrete type.</param>
		/// <param name="lifetimeType">Lifetime type of the registering concrete type.</param>
		public static void Register(this IDIContainerProvider provider, Type concreteType, LifetimeType lifetimeType = LifetimeType.Singleton)
		{
			provider.Register(concreteType, concreteType, lifetimeType);
		}

		/// <summary>
		/// Registers the specified service type with corresponding implemetation type.
		/// </summary>
		/// <typeparam name="TService">Service type.</typeparam>
		/// <param name="provider">The DI provider.</param>
		/// <param name="implementationType">Implementation type.</param>
		/// <param name="lifetimeType">Lifetime type of the registering service type.</param>
		public static void Register<TService>(this IDIContainerProvider provider, Type implementationType, LifetimeType lifetimeType = LifetimeType.Singleton)
		{
			provider.Register(typeof(TService), implementationType, lifetimeType);
		}

		/// <summary>
		/// Registers the specified service type with corresponding implemetation type.
		/// </summary>
		/// <typeparam name="TService">Service type.</typeparam>
		/// <typeparam name="TImplementation">Implementation type.</typeparam>
		/// <param name="provider">The DI provider.</param>
		/// <param name="lifetimeType">Lifetime type of the registering service type.</param>
		public static void Register<TService, TImplementation>(this IDIContainerProvider provider, LifetimeType lifetimeType = LifetimeType.Singleton)
		{
			provider.Register(typeof(TService), typeof(TImplementation), lifetimeType);
		}

		/// <summary>
		/// Registers the specified concrete type for resolve.
		/// </summary>
		/// <typeparam name="TConcrete">Concrete type.</typeparam>
		/// <param name="provider">The DI provider.</param>
		/// <param name="lifetimeType">Lifetime type of the registering concrete type.</param>
		public static void Register<TConcrete>(this IDIContainerProvider provider, LifetimeType lifetimeType = LifetimeType.Singleton)
			where TConcrete : class
		{
			provider.Register(typeof(TConcrete), typeof(TConcrete), lifetimeType);
		}
	}
}