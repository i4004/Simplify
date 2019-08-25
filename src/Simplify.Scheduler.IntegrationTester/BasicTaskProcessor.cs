using System;
using System.Diagnostics;

namespace Simplify.Scheduler.IntegrationTester
{
	public class BasicTaskProcessor : IDisposable
	{
		private static bool _isRunning;

		private readonly DisposableDependency _dependency;

		public BasicTaskProcessor(DisposableDependency dependency)
		{
			_dependency = dependency;
		}

		public void Run()
		{
			if (_isRunning)
				throw new SimplifySchedulerException("BasicTaskProcessor is running a duplicate!");

			_isRunning = true;

			Trace.WriteLine("BasicTaskProcessor launched");
		}

		public void Dispose()
		{
			Trace.WriteLine("BasicTaskProcessor disposed");
		}
	}
}