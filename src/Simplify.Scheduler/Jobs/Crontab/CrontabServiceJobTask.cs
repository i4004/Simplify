using System;
using System.Threading.Tasks;

namespace Simplify.WindowsServices.Jobs.Crontab
{
	/// <summary>
	/// Provides crontab service job task
	/// </summary>
	/// <seealso cref="ICrontabServiceJobTask" />
	public class CrontabServiceJobTask : ICrontabServiceJobTask
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="CrontabServiceJobTask"/> class.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <param name="job">The job.</param>
		/// <param name="task">The task.</param>
		public CrontabServiceJobTask(long id, ICrontabServiceJob job, Task task)
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
		public ICrontabServiceJob Job { get; }

		/// <summary>
		/// Gets the task.
		/// </summary>
		public Task Task { get; }
	}
}