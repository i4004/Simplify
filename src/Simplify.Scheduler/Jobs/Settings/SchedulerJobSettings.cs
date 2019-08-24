namespace Simplify.Scheduler.Jobs.Settings
{
	/// <summary>
	/// Provides scheduler job settings
	/// </summary>
	public class SchedulerJobSettings : ISchedulerJobSettings
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="SchedulerJobSettings"/> class.
		/// </summary>
		protected SchedulerJobSettings()
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
		/// Gets the scheduler processing interval (sec).
		/// </summary>
		/// <value>
		/// The scheduler processing interval (sec).
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