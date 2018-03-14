namespace Simplify.WindowsServices.SingleTaskExample
{
	internal class Program
	{
		private static void Main(string[] args)
		{
#if DEBUG
			// Run debugger
			global::System.Diagnostics.Debugger.Launch();
#endif

			new SingleTaskServiceHandler<ServiceProcess>(true).Start(args);
		}
	}
}