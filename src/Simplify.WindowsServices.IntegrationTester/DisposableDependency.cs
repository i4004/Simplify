using System;
using System.Diagnostics;

namespace Simplify.WindowsServices.IntegrationTester
{
	public class DisposableDependency : IDisposable
	{
		public void Dispose()
		{
			Trace.WriteLine("Disposable dependency disposed");
		}
	}
}