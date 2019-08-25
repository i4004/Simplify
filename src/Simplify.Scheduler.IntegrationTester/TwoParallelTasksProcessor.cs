using System.Diagnostics;
using System.Threading;

namespace Simplify.Scheduler.IntegrationTester
{
	public class TwoParallelTasksProcessor
	{
		public void Execute()
		{
			Trace.WriteLine("--- TwoParallelTasksProcessor launched");

			Thread.Sleep(5000);
		}
	}
}