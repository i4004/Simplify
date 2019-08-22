using System.Threading.Tasks;

namespace Simplify.Scheduler.Jobs.Crontab
{
	/// <summary>
	/// Represent crontab service job task
	/// </summary>
	public interface ICrontabServiceJobTask
	{
		/// <summary>
		/// Gets the task identifier.
		/// </summary>
		long ID { get; }

		/// <summary>
		/// Gets the job.
		/// </summary>
		ICrontabServiceJob Job { get; }

		/// <summary>
		/// Gets the task.
		/// </summary>
		Task Task { get; }
	}
}