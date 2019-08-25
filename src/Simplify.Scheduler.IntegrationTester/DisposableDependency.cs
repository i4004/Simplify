using System;
using System.Diagnostics;

namespace Simplify.Scheduler.IntegrationTester
{
	public class DisposableDependency : IDisposable
	{
		public void Dispose()
		{
			Trace.WriteLine("Disposable dependency disposed");
		}
	}
}