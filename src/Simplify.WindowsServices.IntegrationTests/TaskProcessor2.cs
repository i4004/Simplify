using System.Threading;

namespace Simplify.WindowsServices.IntegrationTests
{
	public class TaskProcessor2
	{
		private static bool _isRunning;

		public void Run()
		{
			if (_isRunning)
				throw new SimplifyWindowsServicesException("TaskProcessor2 is running a duplicate!");

			_isRunning = true;

			Thread.Sleep(3670);

			_isRunning = false;
		}
	}
}