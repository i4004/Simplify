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

			handler.AddJob<OneSecondStepProcessor>("OneSecondStepProcessor");
			handler.AddJob<TwoSecondStepProcessor>(IocRegistrations.Configuration, startupArgs: "Hello world!!!");
			handler.AddJob<OneMinuteStepCrontabProcessor>(IocRegistrations.Configuration, "OneMinuteStepCrontabProcessor", automaticallyRegisterUserType: true);
			handler.AddJob<TwoParallelTasksProcessor>(invokeMethodName: "Execute");
			handler.AddBasicJob<BasicTaskProcessor>();

			if (handler.Start(args))
				return;

			using (var scope = DIContainer.Current.BeginLifetimeScope())
				scope.Resolver.Resolve<BasicTaskProcessor>().Run();
		}
	}
}