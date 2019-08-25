using System.Diagnostics;
using System.Threading;

namespace Simplify.WindowsServices.IntegrationTester
{
	public class OneMinuteStepCrontabProcessor
	{
		private static bool _isRunning;

		public void Run()
		{
			if (_isRunning)
				throw new SimplifyWindowsServicesException("OneMinuteStepCrontabProcessor is running a duplicate!");

			_isRunning = true;

			Trace.WriteLine("--- OneMinuteStepCrontabProcessor launched");

			Thread.Sleep(3218);

			_isRunning = false;
		}
	}
}