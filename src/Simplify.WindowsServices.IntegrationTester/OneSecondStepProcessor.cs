using System;
using System.Diagnostics;
using System.Threading;

namespace Simplify.WindowsServices.IntegrationTester
{
	public class OneSecondStepProcessor : IDisposable
	{
		private static bool _isRunning;

		public OneSecondStepProcessor(DisposableDependency dependency)
		{
		}

		public void Run()
		{
			if (_isRunning)
				throw new SimplifyWindowsServicesException("EverySecondProcessor is running a duplicate!");

			_isRunning = true;

			Trace.WriteLine("EverySecondProcessor launched");

			Thread.Sleep(5120);

			_isRunning = false;
		}

		public void Dispose()
		{
			Trace.WriteLine("EverySecondProcessor disposed");
		}
	}
}