using System;

namespace Simplify.Scheduler
{
	/// <summary>
	/// Scheduler exception delegate
	/// </summary>
	/// <param name="args">The arguments.</param>
	public delegate void SchedulerExceptionEventHandler(SchedulerExceptionArgs args);

	/// <summary>
	/// Provides scheduler exception event args
	/// </summary>
	public class SchedulerExceptionArgs
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="SchedulerExceptionArgs"/> class.
		/// </summary>
		/// <param name="appName">Name of the scheduling application.</param>
		/// <param name="exception">The exception.</param>
		public SchedulerExceptionArgs(string appName, Exception exception)
		{
			AppName = appName;
			Exception = exception;
		}

		/// <summary>
		/// Gets the name of the application.
		/// </summary>
		/// <value>
		/// The name of the application.
		/// </value>
		public string AppName { get; }

		/// <summary>
		/// Gets the exception.
		/// </summary>
		/// <value>
		/// The exception.
		/// </value>
		public Exception Exception { get; }
	}
}