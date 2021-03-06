﻿using Castle.MicroKernel.Registration;
using Castle.Windsor;
using System;

namespace Simplify.DI.Provider.CastleWindsor
{
	/// <summary>
	/// Castle Windsor container provider implementation
	/// </summary>
	public class CastleWindsorDIProvider : IDIContainerProvider
	{
		private IWindsorContainer _container;

		/// <summary>
		/// The IOC container
		/// </summary>
		public IWindsorContainer Container
		{
			get => _container ?? (_container = new WindsorContainer());
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
		public void Register(Type serviceType, Type implementationType, LifetimeType lifetimeType)
		{
			switch (lifetimeType)
			{
				case LifetimeType.PerLifetimeScope:
					Container.Register(Component.For(serviceType).ImplementedBy(implementationType).LifestyleScoped());
					break;

				case LifetimeType.Singleton:
					Container.Register(Component.For(serviceType).ImplementedBy(implementationType).LifestyleSingleton());
					break;

				case LifetimeType.Transient:
					Container.Register(Component.For(serviceType).ImplementedBy(implementationType).LifestyleTransient());
					break;
			}
		}

		/// <summary>
		/// Registers the specified service type.
		/// </summary>
		/// <param name="serviceType">Type of the service.</param>
		/// <param name="instanceCreator">The instance creator.</param>
		/// <param name="lifetimeType">Type of the lifetime.</param>
		public void Register(Type serviceType, Func<IDIResolver, object> instanceCreator, LifetimeType lifetimeType = LifetimeType.PerLifetimeScope)
		{
			switch (lifetimeType)
			{
				case LifetimeType.PerLifetimeScope:
					Container.Register(Component.For(serviceType).UsingFactoryMethod(c => instanceCreator(this)).LifestyleScoped());
					break;

				case LifetimeType.Singleton:
					Container.Register(Component.For(serviceType).UsingFactoryMethod(c => instanceCreator(this)).LifestyleSingleton());
					break;

				case LifetimeType.Transient:
					Container.Register(Component.For(serviceType).UsingFactoryMethod(c => instanceCreator(this)).LifestyleTransient());
					break;
			}
		}

		/// <summary>
		/// Begins the lifetime scope.
		/// </summary>
		/// <returns></returns>
		public ILifetimeScope BeginLifetimeScope()
		{
			return new CastleWindsorLifetimeScope(this);
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
			Container.CheckForPotentiallyMisconfiguredComponents();
		}
	}
}