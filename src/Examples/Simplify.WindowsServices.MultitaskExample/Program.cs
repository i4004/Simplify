using System.ServiceProcess;
using Simplify.DI;

namespace Simplify.WindowsServices.MultitaskExample
{
	class Program
	{
		static void Main()
		{
#if DEBUG
			// Run debugger
			System.Diagnostics.Debugger.Launch();
#endif

			var handler = new MultitaskServiceHandler();

			handler.AddJob<TaskProcessor1>(true);
			handler.AddJob<TaskProcessor1>("TaskProcessor1SecondTaskSettings", "RunTask2");

			DIContainer.Current.Register<TaskProcessor2>();
			handler.AddJob<TaskProcessor2>();

			ServiceBase.Run(handler);
		}
	}
}
