using System;

namespace Simplify.Scheduler.Jobs
{
	/// <summary>
	/// Provides an executing job arguments
	/// </summary>
	public class JobArgs : IJobArgs
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="JobArgs"/> class.
		/// </summary>
		/// <param name="appName">Name of the application.</param>
		/// <param name="startupArgs">The startup arguments.</param>
		public JobArgs(string appName, object startupArgs)
		{
			if (string.IsNullOrEmpty(appName)) throw new ArgumentException("Value cannot be null or empty.", nameof(appName));

			AppName = appName;
			StartupArgs = startupArgs;
		}

		/// <summary>
		/// Gets the name of the current application.
		/// </summary>
		/// <value>
		/// The name of the current application.
		/// </value>
		public string AppName { get; }

		/// <summary>
		/// Gets the job startup arguments.
		/// </summary>
		/// <value>
		/// The job startup arguments.
		/// </value>
		public object StartupArgs { get; }
	}
}