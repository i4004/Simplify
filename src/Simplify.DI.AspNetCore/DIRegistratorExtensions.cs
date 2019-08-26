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
		/// <param name="collection">The collection.</param>
		/// <exception cref="ArgumentNullException">collection</exception>
		public static void RegisterFromServiceCollection(this IDIRegistrator registrator, IServiceCollection collection)
		{
			if (registrator == null) throw new ArgumentNullException(nameof(registrator));
			if (collection == null) throw new ArgumentNullException(nameof(collection));

			foreach (var item in collection)
			{
				// TODO
				//registrator.Register(item.ServiceType);
			}
		}
	}
}