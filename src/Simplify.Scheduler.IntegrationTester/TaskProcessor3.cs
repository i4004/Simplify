using System.Diagnostics;
using System.Threading;

namespace Simplify.Scheduler.IntegrationTester
{
	public class TaskProcessor3
	{
		private static bool _isRunning;

		public void Run()
		{
			if (_isRunning)
				throw new SimplifyWindowsServicesException("TaskProcessor3 is running a duplicate!");

			_isRunning = true;

			Trace.WriteLine("--- TaskProcessor3 launched");

			Thread.Sleep(3218);

			_isRunning = false;
		}
	}
}