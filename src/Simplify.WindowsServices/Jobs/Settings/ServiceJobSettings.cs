namespace Simplify.WindowsServices.Jobs.Settings
{
	/// <summary>
	/// Provides service job settings
	/// </summary>
	public class ServiceJobSettings : IServiceJobSettings
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ServiceJobSettings"/> class.
		/// </summary>
		protected ServiceJobSettings()
		{
		}

		/// <summary>
		/// Gets the crontab expression.
		/// </summary>
		/// <value>
		/// The crontab expression.
		/// </value>
		public string CrontabExpression { get; protected set; }

		/// <summary>
		/// Gets the service processing interval (sec).
		/// </summary>
		/// <value>
		/// The service processing interval (sec).
		/// </value>
		public int ProcessingInterval { get; protected set; } = 60;

		/// <summary>
		/// Gets a value indicating whether GC.Collect will be executed on on task finish.
		/// </summary>
		public bool CleanupOnTaskFinish { get; protected set; } = true;

		/// <summary>
		/// Gets the maximum allowed parallel tasks of this job.
		/// </summary>
		public int MaximumParallelTasksCount { get; protected set; } = 1;
	}
}