using System.Diagnostics;
using System.Threading;

namespace Simplify.WindowsServices.IntegrationTests
{
	public class BasicTaskProcessor
	{
		private static bool _isRunning;

		public void Run()
		{
			if (_isRunning)
				throw new SimplifyWindowsServicesException("BasicTaskProcessor is running a duplicate!");

			_isRunning = true;

			Trace.WriteLine("TaskProcessor1 launched");

			Thread.Sleep(5120);
		}
	}
}