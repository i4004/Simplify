using System.ServiceProcess;
using Simplify.DI;
using Simplify.DI.Provider.SimpleInjector;

namespace Simplify.WindowsServices.IntegrationTests
{
	internal class Program
	{
		private static void Main()
		{
#if DEBUG
			// Run debugger
			System.Diagnostics.Debugger.Launch();
#endif

			DIContainer.Current = new SimpleInjectorDIProvider();

			var handler = new MultitaskServiceHandler();

			handler.AddJob<TaskProcessor1>("TaskProcessor1Settings");
			handler.AddJob<TaskProcessor2>("TaskProcessor2Settings", "Run", true);
			handler.AddJob<TaskProcessor3>("TaskProcessor3Settings", "Run", true);
			handler.AddBasicJob<BasicTaskProcessor>(true);

			DIContainer.Current.Register<TaskProcessor1>();

			ServiceBase.Run(handler);
		}
	}
}