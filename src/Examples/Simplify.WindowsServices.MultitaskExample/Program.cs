using Microsoft.Extensions.Configuration;
using Simplify.DI;
using Simplify.WindowsServices.MultitaskExample.Setup;

namespace Simplify.WindowsServices.MultitaskExample
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

			var handler = new MultitaskServiceHandler();

			using (var scope = DIContainer.Current.BeginLifetimeScope())
			{
				var configuration = scope.Resolver.Resolve<IConfiguration>();

				handler.AddJob<TaskProcessor1>(true);
				handler.AddJob<TaskProcessor1>("TaskProcessor1SecondTaskSettings", "RunTask2");

				DIContainer.Current.Register<TaskProcessor2>();
				handler.AddJob<TaskProcessor2>(configuration);
			}

			handler.Start(args);
		}
	}
}