using System.ServiceProcess;

namespace Simplify.AutomatedWindowsServices.Example
{
	class Program
	{
		static void Main()
		{
#if DEBUG
			// Run debugger
			System.Diagnostics.Debugger.Launch();
#endif

			ServiceBase.Run(new SingleTaskServiceHandler<ServiceProcess>(true));
		}
	}
}
