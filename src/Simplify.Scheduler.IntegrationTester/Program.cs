using Simplify.DI;
using Simplify.Scheduler.IntegrationTester.Setup;

namespace Simplify.Scheduler.IntegrationTester
{
	internal class Program
	{
		private static void Main(string[] args)
		{
			// IOC container setup

			IocRegistrations.Register();
			DIContainer.Current.Verify();

			// Using scheduler

			using (var scheduler = new MultitaskScheduler())
			{
				scheduler.AddJob<OneSecondStepProcessor>(IocRegistrations.Configuration);
				scheduler.AddJob<TwoSecondStepProcessor>(IocRegistrations.Configuration, startupArgs: "Hello world!!!");
				scheduler.AddJob<OneMinuteStepCrontabProcessor>(IocRegistrations.Configuration);
				scheduler.AddJob<TwoParallelTasksProcessor>(IocRegistrations.Configuration, invokeMethodName: "Execute");
				scheduler.AddBasicJob<BasicTaskProcessor>();

				if (scheduler.Start(args))
					return;
			}

			// Testing some processors without scheduler
			using (var scope = DIContainer.Current.BeginLifetimeScope())
				scope.Resolver.Resolve<BasicTaskProcessor>().Run();
		}
	}
}