using System;

namespace Simplify.DI
{
	/// <summary>
	/// Represents DI container registrator
	/// </summary>
	public interface IDIRegistrator
	{
		/// <summary>
		/// Registers the specified service type with corresponding implementation type.
		/// </summary>
		/// <param name="serviceType">Service type.</param>
		/// <param name="implementationType">Implementation type.</param>
		/// <param name="lifetimeType">Lifetime type of the registering service type.</param>
		void Register(Type serviceType, Type implementationType, LifetimeType lifetimeType = LifetimeType.PerLifetimeScope);

		/// <summary>
		/// Registers the specified service type for resolve with delegate for service implementation instance creation.
		/// </summary>
		/// <param name="serviceType">Type of the service.</param>
		/// <param name="instanceCreator">The instance creator.</param>
		/// <param name="lifetimeType">Lifetime type of the registering concrete type.</param>
		void Register(Type serviceType, Func<IDIResolver, object> instanceCreator,
			LifetimeType lifetimeType = LifetimeType.PerLifetimeScope);
	}
}