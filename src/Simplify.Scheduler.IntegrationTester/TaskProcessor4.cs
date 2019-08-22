using System.Diagnostics;
using System.Threading;

namespace Simplify.Scheduler.IntegrationTester
{
	public class TaskProcessor4
	{
		public void Execute()
		{
			Trace.WriteLine("--- TaskProcessor4 launched");

			Thread.Sleep(5000);
		}
	}
}