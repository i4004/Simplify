using System;

namespace Simplify.WindowsServices.Jobs
{
	/// <summary>
	/// Provides an executing job arguments
	/// </summary>
	public class JobArgs : IJobArgs
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="JobArgs"/> class.
		/// </summary>
		/// <param name="serviceName">Name of the service.</param>
		/// <param name="startupArgs">The startup arguments.</param>
		public JobArgs(string serviceName, object startupArgs)
		{
			if (string.IsNullOrEmpty(serviceName)) throw new ArgumentException("Value cannot be null or empty.", nameof(serviceName));

			ServiceName = serviceName;
			StartupArgs = startupArgs;
		}

		/// <summary>
		/// Gets the name of the current service.
		/// </summary>
		/// <value>
		/// The name of the current service.
		/// </value>
		public string ServiceName { get; }

		/// <summary>
		/// Gets the job startup arguments.
		/// </summary>
		/// <value>
		/// The job startup arguments.
		/// </value>
		public object StartupArgs { get; }
	}
}