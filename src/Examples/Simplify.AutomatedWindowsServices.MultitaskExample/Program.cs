using System.ServiceProcess;

namespace Simplify.AutomatedWindowsServices.MultitaskExample
{
	class Program
	{
		static void Main()
		{
#if DEBUG
			// Run debugger
			System.Diagnostics.Debugger.Launch();
#endif

			var handler = new MultitaskTaskServiceHandler();

			handler.AddJob<TaskProcessor1>(true);
			handler.AddJob<TaskProcessor1>("TaskProcessor1SecondTaskSettings", "RunTask2");
			handler.AddJob<TaskProcessor2>(true);

			ServiceBase.Run(handler);
		}
	}
}
