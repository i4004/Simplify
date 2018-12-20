using Microsoft.Extensions.Configuration;
using Simplify.DI;

namespace Simplify.WindowsServices.IntegrationTester
{
	internal class Program
	{
		private static void Main(string[] args)
		{
#if DEBUG
			// Run debugger
			global::System.Diagnostics.Debugger.Launch();
#endif

			IocRegistrations.Register();

			var configuration = new ConfigurationBuilder()
				.AddJsonFile("appsettings.json", false)
				.Build();

			var handler = new MultitaskServiceHandler();

			handler.AddJob<TaskProcessor1>("TaskProcessor1Settings");
			handler.AddJob<TaskProcessor2>(configuration, "TaskProcessor2Settings", "Run", true);
			handler.AddJob<TaskProcessor3>(configuration, "TaskProcessor3Settings", "Run", true);
			handler.AddJob<TaskProcessor4>("TaskProcessor4Settings", "Run", true);
			handler.AddBasicJob<BasicTaskProcessor>();

			if (handler.Start(args))
				return;

			using (var scope = DIContainer.Current.BeginLifetimeScope())
				scope.Resolver.Resolve<BasicTaskProcessor>().Run();
		}
	}
}