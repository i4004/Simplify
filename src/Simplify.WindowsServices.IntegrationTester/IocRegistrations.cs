using Microsoft.Extensions.Configuration;
using Simplify.DI;

namespace Simplify.WindowsServices.IntegrationTester
{
	public static class IocRegistrations
	{
		public static IConfiguration Configuration { get; private set; }

		public static void Register()
		{
			DIContainer.Current.Register<Dependency1>();

			DIContainer.Current.Register<TaskProcessor1>();
			DIContainer.Current.Register<BasicTaskProcessor>();
		}

		private static void RegisterConfiguration()
		{
			Configuration = new ConfigurationBuilder()
				.AddJsonFile("appsettings.json", false)
				.Build();

			DIContainer.Current.Register(p => Configuration, LifetimeType.Singleton);
		}
	}
}