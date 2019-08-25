using Simplify.WindowsServices.Jobs;
using System.Diagnostics;
using System.Threading;

namespace Simplify.WindowsServices.IntegrationTester
{
	public class TwoSecondStepProcessor
	{
		private static bool _isRunning;

		public void Run(IJobArgs args)
		{
			if (_isRunning)
				throw new SimplifyWindowsServicesException("TwoSecondStepProcessor is running a duplicate!");

			_isRunning = true;

			Trace.WriteLine("TwoSecondStepProcessor launched");
			Trace.WriteLine($"TwoSecondStepProcessor args startup args is: {args.StartupArgs}");
			Trace.WriteLine($"TwoSecondStepProcessor args service name is: {args.ServiceName}");

			Thread.Sleep(3670);

			_isRunning = false;
		}
	}
}