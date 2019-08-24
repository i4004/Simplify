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
				scheduler.AddJob<TaskProcessor1>(IocRegistrations.Configuration, "TaskProcessor1Settings");
				scheduler.AddJob<TaskProcessor2>(IocRegistrations.Configuration, "TaskProcessor2Settings", startupArgs: "Hello world!!!");
				scheduler.AddJob<TaskProcessor3>(IocRegistrations.Configuration, "TaskProcessor3Settings");
				scheduler.AddJob<TaskProcessor4>(IocRegistrations.Configuration, "TaskProcessor4Settings", "Execute");
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