using System;
using DryIoc;

namespace Simplify.DI.Provider.DryIoc
{
	/// <summary>
	/// DryIoc DI container provider implementation
	/// </summary>
	public class DryIocDIProvider : IDIContainerProvider
	{
		private IContainer _container;

		/// <summary>
		/// The IOC container
		/// </summary>
		public IContainer Container
		{
			get => _container ?? (_container = new Container());
			set => _container = value ?? throw new ArgumentNullException(nameof(value));
		}

		/// <summary>
		/// Resolves the specified service type.
		/// </summary>
		/// <param name="serviceType">Type of the service.</param>
		/// <returns></returns>
		public object Resolve(Type serviceType)
		{
			return Container.Resolve(serviceType);
		}

		/// <summary>
		/// Registers the specified service type with corresponding implementation type.
		/// </summary>
		/// <param name="serviceType">Service type.</param>
		/// <param name="implementationType">Implementation type.</param>
		/// <param name="lifetimeType">Lifetime type of the registering services type.</param>
		public void Register(Type serviceType, Type implementationType, LifetimeType lifetimeType = LifetimeType.PerLifetimeScope)
		{
			switch (lifetimeType)
			{
				case LifetimeType.Transient:
					Container.Register(serviceType, implementationType, Reuse.Transient);
					break;

				case LifetimeType.Singleton:
					Container.Register(serviceType, implementationType, Reuse.Singleton);
					break;

				case LifetimeType.PerLifetimeScope:
					Container.Register(serviceType, implementationType, Reuse.InCurrentScope);
					break;
			}
		}

		/// <summary>
		/// Registers the specified concrete type for resolve with delegate for concrete implementation instance creation.
		/// </summary>
		/// <typeparam name="TService">Service type.</typeparam>
		/// <param name="instanceCreator">The instance creator.</param>
		/// <param name="lifetimeType">Lifetime type of the registering concrete type.</param>
		public void Register<TService>(Func<IDIRegistrator, TService> instanceCreator,
			LifetimeType lifetimeType = LifetimeType.PerLifetimeScope)
			where TService : class
		{
			switch (lifetimeType)
			{
				case LifetimeType.Transient:
					Container.RegisterDelegate(c => instanceCreator(this), Reuse.Transient);
					break;

				case LifetimeType.Singleton:
					Container.RegisterDelegate(c => instanceCreator(this), Reuse.Singleton);
					break;

				case LifetimeType.PerLifetimeScope:
					Container.RegisterDelegate(c => instanceCreator(this), Reuse.InCurrentScope);
					break;
			}
		}

		/// <summary>
		/// Begins the lifetime scope.
		/// </summary>
		/// <returns></returns>
		public ILifetimeScope BeginLifetimeScope()
		{
			return new DryIocLifetimeScope(this);
		}

		/// <summary>
		/// Releases unmanaged and - optionally - managed resources.
		/// </summary>
		public void Dispose()
		{
			_container?.Dispose();
		}
	}
}