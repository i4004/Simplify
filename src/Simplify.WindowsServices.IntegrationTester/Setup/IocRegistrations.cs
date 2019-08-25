using Microsoft.Extensions.Configuration;
using Simplify.DI;

namespace Simplify.WindowsServices.IntegrationTester.Setup
{
	public static class IocRegistrations
	{
		public static IConfiguration Configuration { get; private set; }

		public static void Register()
		{
			RegisterConfiguration();

			DIContainer.Current.Register<DisposableDependency>();

			DIContainer.Current.Register<OneSecondStepProcessor>();
			DIContainer.Current.Register<TwoSecondStepProcessor>();
			DIContainer.Current.Register<TwoParallelTasksProcessor>();
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