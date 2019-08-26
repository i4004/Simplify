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
			{
				// TODO
				//registrator.Register(item.ServiceType);
			}
		}
	}
}