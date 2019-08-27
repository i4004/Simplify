using DryIoc;
using System;

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
		/// <exception cref="ArgumentOutOfRangeException">lifetimeType - null</exception>
		public void Register(Type serviceType, Type implementationType, LifetimeType lifetimeType)
		{
			switch (lifetimeType)
			{
				case LifetimeType.PerLifetimeScope:
					Container.Register(serviceType, implementationType, Reuse.InCurrentScope);
					break;

				case LifetimeType.Singleton:
					Container.Register(serviceType, implementationType, Reuse.Singleton);
					break;

				case LifetimeType.Transient:
					Container.Register(serviceType, implementationType, Reuse.Transient);
					break;

				default:
					throw new ArgumentOutOfRangeException(nameof(lifetimeType), lifetimeType, null);
			}
		}

		/// <summary>
		/// Registers the specified service type.
		/// </summary>
		/// <param name="serviceType">Type of the service.</param>
		/// <param name="instanceCreator">The instance creator.</param>
		/// <param name="lifetimeType">Type of the lifetime.</param>
		/// <exception cref="ArgumentOutOfRangeException">lifetimeType - null</exception>
		public void Register(Type serviceType, Func<IDIResolver, object> instanceCreator, LifetimeType lifetimeType = LifetimeType.PerLifetimeScope)
		{
			switch (lifetimeType)
			{
				case LifetimeType.PerLifetimeScope:
					Container.RegisterDelegate(serviceType, c => instanceCreator(new DryIocDIResolver(c)), Reuse.InCurrentScope);
					break;

				case LifetimeType.Singleton:
					Container.RegisterDelegate(serviceType, c => instanceCreator(new DryIocDIResolver(c)), Reuse.Singleton);
					break;

				case LifetimeType.Transient:
					Container.RegisterDelegate(serviceType, c => instanceCreator(new DryIocDIResolver(c)), Reuse.Transient);
					break;

				default:
					throw new ArgumentOutOfRangeException(nameof(lifetimeType), lifetimeType, null);
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

		/// <summary>
		/// Performs container objects graph verification
		/// </summary>
		public void Verify()
		{
			var result = Container.Validate();

			if (result.Length > 0)
				throw result[0].Value;
		}
	}
}