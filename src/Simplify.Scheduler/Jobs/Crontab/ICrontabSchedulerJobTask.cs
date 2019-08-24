using System.Threading.Tasks;

namespace Simplify.Scheduler.Jobs.Crontab
{
	/// <summary>
	/// Represent crontab scheduler job task
	/// </summary>
	public interface ICrontabSchedulerJobTask
	{
		/// <summary>
		/// Gets the task identifier.
		/// </summary>
		long ID { get; }

		/// <summary>
		/// Gets the job.
		/// </summary>
		ICrontabSchedulerJob Job { get; }

		/// <summary>
		/// Gets the task.
		/// </summary>
		Task Task { get; }
	}
}