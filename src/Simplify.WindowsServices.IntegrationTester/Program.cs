using Simplify.DI;
using Simplify.WindowsServices.IntegrationTester.Setup;

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
			DIContainer.Current.Verify();

			var handler = new MultitaskServiceHandler();

			handler.AddJob<TaskProcessor1>("TaskProcessor1Settings");
			handler.AddJob<TaskProcessor2>(IocRegistrations.Configuration, "TaskProcessor2Settings", startupArgs: "Hello world!!!");
			handler.AddJob<TaskProcessor3>(IocRegistrations.Configuration, "TaskProcessor3Settings", automaticallyRegisterUserType: true);
			handler.AddJob<TaskProcessor4>("TaskProcessor4Settings", "Execute");
			handler.AddBasicJob<BasicTaskProcessor>();

			if (handler.Start(args))
				return;

			using (var scope = DIContainer.Current.BeginLifetimeScope())
				scope.Resolver.Resolve<BasicTaskProcessor>().Run();
		}
	}
}