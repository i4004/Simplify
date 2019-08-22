using Simplify.Scheduler.Jobs;
using System.Diagnostics;
using System.Threading;

namespace Simplify.Scheduler.IntegrationTester
{
	public class TaskProcessor2
	{
		private static bool _isRunning;

		public void Run(IJobArgs args)
		{
			if (_isRunning)
				throw new SimplifyWindowsServicesException("TaskProcessor2 is running a duplicate!");

			_isRunning = true;

			Trace.WriteLine("TaskProcessor2 launched");
			Trace.WriteLine($"TaskProcessor2 args startup args is: {args.StartupArgs}");
			Trace.WriteLine($"TaskProcessor2 args service name is: {args.ServiceName}");

			Thread.Sleep(3670);

			_isRunning = false;
		}
	}
}