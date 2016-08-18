using System;
using SimpleInjector;
using SimpleInjector.Extensions.ExecutionContextScoping;

namespace Simplify.DI.Provider.SimpleInjector
{
	/// <summary>
	/// Simple Injector DI container provider implementation
	/// </summary>
	public class SimpleInjectorDIProvider : IDIContainerProvider
	{
		private Container _container;

		/// <summary>
		/// The IOC container
		/// </summary>
		public Container Container
		{
			get
			{
				return _container ??
					   (_container = new Container { Options = { DefaultScopedLifestyle = new ExecutionContextScopeLifestyle() } });
			}
			set
			{
				if (value == null)
					throw new ArgumentNullException(nameof(value));

				_container = value;
			}
		}

		/// <summary>
		/// Resolves the specified service type.
		/// </summary>
		/// <param name="serviceType">Type of the service.</param>
		/// <returns></returns>
		public object Resolve(Type serviceType)
		{
			return Container.GetInstance(serviceType);
		}

		/// <summary>
		/// Registers the specified service type with corresponding implementation type.
		/// </summary>
		/// <param name="serviceType">Service type.</param>
		/// <param name="implementationType">Implementation type.</param>
		/// <param name="lifetimeType">Lifetime type of the registering services type.</param>
		public void Register(Type serviceType, Type implementationType, LifetimeType lifetimeType = LifetimeType.Singleton)
		{
			switch (lifetimeType)
			{
				case LifetimeType.Transient:
					Container.Register(serviceType, implementationType, Lifestyle.Transient);
					break;

				case LifetimeType.Singleton:
					Container.Register(serviceType, implementationType, Lifestyle.Singleton);
					break;

				case LifetimeType.PerLifetimeScope:
					Container.Register(serviceType, implementationType, Lifestyle.Scoped);
					break;
			}
		}

		/// <summary>
		/// Registers the specified provider.
		/// </summary>
		/// <typeparam name="TService">Concrete type.</typeparam>
		/// <param name="instanceCreator">The instance creator.</param>
		/// <param name="lifetimeType">Lifetime type of the registering concrete type.</param>
		public void Register<TService>(Func<IDIContainerProvider, TService> instanceCreator, LifetimeType lifetimeType = LifetimeType.Singleton)
			where TService : class
		{
			switch (lifetimeType)
			{
				case LifetimeType.Transient:
					Container.Register(() => instanceCreator(this), Lifestyle.Transient);
					break;

				case LifetimeType.Singleton:
					Container.Register(() => instanceCreator(this), Lifestyle.Singleton);
					break;

				case LifetimeType.PerLifetimeScope:
					Container.Register(() => instanceCreator(this), Lifestyle.Scoped);
					break;
			}
		}

		/// <summary>
		/// Begins the lifetime scope.
		/// </summary>
		/// <returns></returns>
		public ILifetimeScope BeginLifetimeScope()
		{
			return new SimpleInjectorLifetimeScope(this);
		}
	}
}