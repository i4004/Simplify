using System;

namespace Simplify.DI
{
	/// <summary>
	/// Provides DI registrator extensions
	/// </summary>
	public static class DIRegistratorExtensions
	{
		/// <summary>
		/// Registers the specified concrete type for resolve.
		/// </summary>
		/// <param name="registrator">The DI registrator.</param>
		/// <param name="concreteType">Concrete type.</param>
		/// <param name="lifetimeType">Lifetime type of the registering concrete type.</param>
		public static void Register(this IDIRegistrator registrator, Type concreteType, LifetimeType lifetimeType = LifetimeType.PerLifetimeScope)
		{
			registrator.Register(concreteType, concreteType, lifetimeType);
		}

		/// <summary>
		/// Registers the specified service type with corresponding implementation type.
		/// </summary>
		/// <typeparam name="TService">Service type.</typeparam>
		/// <param name="registrator">The DI registrator.</param>
		/// <param name="implementationType">Implementation type.</param>
		/// <param name="lifetimeType">Lifetime type of the registering service type.</param>
		public static void Register<TService>(this IDIRegistrator registrator, Type implementationType, LifetimeType lifetimeType = LifetimeType.PerLifetimeScope)
		{
			registrator.Register(typeof(TService), implementationType, lifetimeType);
		}

		/// <summary>
		/// Registers the specified service type with corresponding implementation type.
		/// </summary>
		/// <typeparam name="TService">Service type.</typeparam>
		/// <typeparam name="TImplementation">Implementation type.</typeparam>
		/// <param name="registrator">The DI registrator.</param>
		/// <param name="lifetimeType">Lifetime type of the registering service type.</param>
		public static void Register<TService, TImplementation>(this IDIRegistrator registrator, LifetimeType lifetimeType = LifetimeType.PerLifetimeScope)
		{
			registrator.Register(typeof(TService), typeof(TImplementation), lifetimeType);
		}

		/// <summary>
		/// Registers the specified concrete type for resolve.
		/// </summary>
		/// <typeparam name="TConcrete">Concrete type.</typeparam>
		/// <param name="registrator">The DI registrator.</param>
		/// <param name="lifetimeType">Lifetime type of the registering concrete type.</param>
		public static void Register<TConcrete>(this IDIRegistrator registrator, LifetimeType lifetimeType = LifetimeType.PerLifetimeScope)
			where TConcrete : class
		{
			registrator.Register(typeof(TConcrete), typeof(TConcrete), lifetimeType);
		}
	}
}