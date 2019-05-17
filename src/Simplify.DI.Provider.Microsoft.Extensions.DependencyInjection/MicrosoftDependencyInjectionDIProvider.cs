using Microsoft.Extensions.DependencyInjection;
using System;

namespace Simplify.DI.Provider.Microsoft.Extensions.DependencyInjection
{
	/// <summary>
	///  Microsoft.DependencyInjection DI container provider implementation
	/// </summary>
	public class MicrosoftDependencyInjectionDIProvider : IDIContainerProvider
	{
		private IServiceCollection _services;
		private IServiceProvider _serviceProvider;

		/// <summary>
		/// <c>true</c> to perform check verifying that scoped services never gets resolved from root provider; otherwise <c>false</c>.
		/// </summary>
		public bool ValidateScopes { get; set; } = true;

		/// <summary>
		/// The IOC container registrations
		/// </summary>
		public IServiceCollection Services
		{
			get => _services ?? (_services = new ServiceCollection());
			set => _services = value ?? throw new ArgumentNullException(nameof(value));
		}

		/// <summary>
		/// Gets or sets the service provider.
		/// </summary>
		/// <value>
		/// The service provider.
		/// </value>
		/// <exception cref="ArgumentNullException">value</exception>
		public IServiceProvider ServiceProvider
		{
			get => _serviceProvider ?? (_serviceProvider = Services.BuildServiceProvider(ValidateScopes));
			set => _serviceProvider = value ?? throw new ArgumentNullException(nameof(value));
		}

		/// <summary>
		/// Resolves the specified service type.
		/// </summary>
		/// <param name="serviceType">Type of the service.</param>
		/// <returns></returns>
		public object Resolve(Type serviceType)
		{
			return ServiceProvider.GetRequiredService(serviceType);
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
				case LifetimeType.PerLifetimeScope:
					Services.AddScoped(serviceType);
					break;

				case LifetimeType.Singleton:
					Services.AddSingleton(serviceType);
					break;

				case LifetimeType.Transient:
					Services.AddTransient(serviceType);
					break;
			}
		}

		/// <summary>
		/// Registers the specified provider.
		/// </summary>
		/// <typeparam name="TService">Concrete type.</typeparam>
		/// <param name="instanceCreator">The instance creator.</param>
		/// <param name="lifetimeType">Lifetime type of the registering concrete type.</param>
		public void Register<TService>(Func<IDIResolver, TService> instanceCreator, LifetimeType lifetimeType = LifetimeType.Singleton)
			where TService : class
		{
			switch (lifetimeType)
			{
				case LifetimeType.PerLifetimeScope:
					Services.AddScoped(c => instanceCreator(new MicrosoftDependencyInjectionDIResolver(c)));
					break;

				case LifetimeType.Singleton:
					Services.AddSingleton(c => instanceCreator(new MicrosoftDependencyInjectionDIResolver(c)));
					break;

				case LifetimeType.Transient:
					Services.AddTransient(c => instanceCreator(new MicrosoftDependencyInjectionDIResolver(c)));
					break;
			}
		}

		/// <summary>
		/// Begins the lifetime scope.
		/// </summary>
		/// <returns></returns>
		public ILifetimeScope BeginLifetimeScope()
		{
			return new MicrosoftDependencyInjectionILifetimeScope(this);
		}

		/// <summary>
		/// Releases unmanaged and - optionally - managed resources.
		/// </summary>
		public void Dispose()
		{
		}
	}
}