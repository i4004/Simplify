using System;
using System.Diagnostics;

namespace Simplify.WindowsServices.IntegrationTests
{
	public class BasicTaskProcessor : IDisposable
	{
		private static bool _isRunning;

		public void Run()
		{
			if (_isRunning)
				throw new SimplifyWindowsServicesException("BasicTaskProcessor is running a duplicate!");

			_isRunning = true;

			Trace.WriteLine("BasicTaskProcessor launched");
		}

		public void Dispose()
		{
			Trace.WriteLine("BasicTaskProcessor disposed");
		}
	}
}