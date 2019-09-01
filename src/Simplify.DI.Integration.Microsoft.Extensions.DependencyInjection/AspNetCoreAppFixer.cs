using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using Microsoft.Extensions.Options;

namespace Simplify.DI.Integration.Microsoft.Extensions.DependencyInjection
{
	internal static class AspNetCoreAppFixer
	{
		public static bool CompatibilityResolver(IDIRegistrator registrator, ServiceDescriptor item, LifetimeType lifetime)
		{
			if (FixLogger(registrator, item, lifetime))
				return true;

			return false;
		}

		private static bool FixLogger(IDIRegistrator registrator, ServiceDescriptor item, LifetimeType lifetime)
		{
			// Fixing logger

			var interfaceType = typeof(ILoggerProvider);
			var classType = typeof(ConsoleLoggerProvider);

			if (item.ServiceType == interfaceType || item.ServiceType == classType)
			{
				DIRegistratorExtensions.RegisterServiceDescriptor(registrator, new ServiceDescriptor(interfaceType, r => new ConsoleLoggerProvider(r
					.GetService<
						IOptionsMonitor<ConsoleLoggerOptions>>()), ServiceLifetime.Singleton), lifetime);

				return true;
			}

			return false;
		}
	}
}