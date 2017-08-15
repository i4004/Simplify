using Simplify.DI;

namespace Simplify.WindowsServices.IntegrationTests
{
	internal class Program
	{
		private static void Main(string[] args)
		{
#if DEBUG
			// Run debugger
			System.Diagnostics.Debugger.Launch();
#endif

			IocRegistrations.Register();

			var handler = new MultitaskServiceHandler();

			handler.AddJob<TaskProcessor1>("TaskProcessor1Settings");
			handler.AddJob<TaskProcessor2>("TaskProcessor2Settings", "Run", true);
			handler.AddJob<TaskProcessor3>("TaskProcessor3Settings", "Run", true);
			handler.AddBasicJob<BasicTaskProcessor>();

			if (handler.Start(args))
				return;

			using (var scope = DIContainer.Current.BeginLifetimeScope())
				scope.Container.Resolve<BasicTaskProcessor>().Run();
		}
	}
}