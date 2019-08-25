using System.Diagnostics;
using System.Threading;

namespace Simplify.WindowsServices.IntegrationTester
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