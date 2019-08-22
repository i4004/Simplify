using System;
using System.Diagnostics;

namespace Simplify.Scheduler.IntegrationTester
{
	public class Dependency1 : IDisposable
	{
		public void Dispose()
		{
			Trace.WriteLine("Dependency1 disposed");
		}
	}
}