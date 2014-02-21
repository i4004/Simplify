using System.ServiceProcess;

namespace Simplify.WindowsServices
{
	/// <summary>
	/// Non-timer windows service (for constant async operations, like TCP/IP server) base class
	/// </summary>
	public abstract class BasicServiceBase : ServiceBase
	{
		/// <summary>
		/// Subscribe your async operations startup method here
		/// </summary>
		protected new event RunEventHandler Run;

		/// <summary>
		/// Non-timer windows service working method delegate
		/// </summary>
		/// <returns></returns>
		protected delegate bool RunEventHandler();

		/// <summary>
		/// Subscribe your service initialization method here
		/// </summary>
		protected event InitializeEventHandler Initialize;

		/// <summary>
		/// Non-timer windows service initialization method delegate
		/// </summary>
		/// <returns></returns>
		protected delegate bool InitializeEventHandler();

		/// <summary>
		/// When implemented in a derived class, executes when a Start command is sent to the service by the Service Control Manager (SCM) or when the operating system starts (for a service that starts automatically). Specifies actions to take when the service starts.
		/// </summary>
		/// <param name="args">Data passed by the start command.</param>
		/// <exception cref="ServiceInitializationException">
		/// Initialize event not set
		/// or
		/// OnRun event not set
		/// </exception>
		protected override void OnStart(string[] args)
		{
			if (Initialize == null)
				throw new ServiceInitializationException("Initialize event not set");

			if (Run == null)
				throw new ServiceInitializationException("OnRun event not set");

			if (Initialize())
		    {
		        base.OnStart(args);

				if (!Run())
					Stop();
		    }
			else
			{
			    ExitCode = 14001;	// Configuration is incorrect
			    Stop();
			}
		}
	}
}
