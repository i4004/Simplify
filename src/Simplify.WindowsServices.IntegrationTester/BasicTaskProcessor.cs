using System;
using System.Diagnostics;

namespace Simplify.WindowsServices.IntegrationTester
{
	public class BasicTaskProcessor : IDisposable
	{
		private static bool _isRunning;

		private readonly Dependency1 _dependency1;

		public BasicTaskProcessor(Dependency1 dependency1)
		{
			_dependency1 = dependency1;
		}

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