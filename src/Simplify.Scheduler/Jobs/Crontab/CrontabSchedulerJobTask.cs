using System;
using System.Threading.Tasks;

namespace Simplify.Scheduler.Jobs.Crontab
{
	/// <summary>
	/// Provides crontab scheduler job task
	/// </summary>
	/// <seealso cref="ICrontabSchedulerJobTask" />
	public class CrontabSchedulerJobTask : ICrontabSchedulerJobTask
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="CrontabSchedulerJobTask"/> class.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <param name="job">The job.</param>
		/// <param name="task">The task.</param>
		public CrontabSchedulerJobTask(long id, ICrontabSchedulerJob job, Task task)
		{
			ID = id;

			Job = job ?? throw new ArgumentNullException(nameof(job));
			Task = task ?? throw new ArgumentNullException(nameof(task));
		}

		/// <summary>
		/// Gets the task identifier.
		/// </summary>
		public long ID { get; }

		/// <summary>
		/// Gets the job.
		/// </summary>
		public ICrontabSchedulerJob Job { get; }

		/// <summary>
		/// Gets the task.
		/// </summary>
		public Task Task { get; }
	}
}