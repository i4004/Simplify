namespace Simplify.Scheduler.Jobs
{
	/// <summary>
	/// Represents an executing job args
	/// </summary>
	public interface IJobArgs
	{
		/// <summary>
		/// Gets the name of the current service.
		/// </summary>
		/// <value>
		/// The name of the current service.
		/// </value>
		string ServiceName { get; }

		/// <summary>
		/// Gets the job startup arguments.
		/// </summary>
		/// <value>
		/// The job startup arguments.
		/// </value>
		object StartupArgs { get; }
	}
}