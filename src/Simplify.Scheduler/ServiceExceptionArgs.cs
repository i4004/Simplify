using System;

namespace Simplify.Scheduler
{
	/// <summary>
	/// Service exception delegate
	/// </summary>
	/// <param name="args">The arguments.</param>
	public delegate void ServiceExceptionEventHandler(ServiceExceptionArgs args);

	/// <summary>
	/// Provides service exception event args
	/// </summary>
	public class ServiceExceptionArgs
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ServiceExceptionArgs"/> class.
		/// </summary>
		/// <param name="serviceName">Name of the service.</param>
		/// <param name="exception">The exception.</param>
		public ServiceExceptionArgs(string serviceName, Exception exception)
		{
			ServiceName = serviceName;
			Exception = exception;
		}

		/// <summary>
		/// Gets the name of the service.
		/// </summary>
		/// <value>
		/// The name of the service.
		/// </value>
		public string ServiceName { get; }

		/// <summary>
		/// Gets the exception.
		/// </summary>
		/// <value>
		/// The exception.
		/// </value>
		public Exception Exception { get; }
	}
}