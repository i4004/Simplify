using Simplify.DI;

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

			var handler = new MultitaskServiceHandler();

			handler.AddJob<TaskProcessor1>(true);
			handler.AddJob<TaskProcessor1>("TaskProcessor1SecondTaskSettings", "RunTask2");

			DIContainer.Current.Register<TaskProcessor2>();
			handler.AddJob<TaskProcessor2>();

			handler.Start(args);
		}
	}
}