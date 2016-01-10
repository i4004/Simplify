using System.ServiceProcess;
using Simplify.DI;
using Simplify.DI.Provider.SimpleInjector;

namespace Simplify.WindowsServices.IntegrationTests
{
	class Program
	{
		static void Main()
		{
			DIContainer.Current = new SimpleInjectorDIProvider();

			var handler = new MultitaskServiceHandler();

			handler.AddJob<TaskProcessor1>("TaskProcessor1Settings", "Run", true);
			handler.AddJob<TaskProcessor2>("TaskProcessor2Settings", "Run", true);
			
			ServiceBase.Run(handler);
		}
	}
}
