namespace Simplify.Scheduler.Jobs.Crontab
{
	/// <summary>
	/// Represent crontab processor factory
	/// </summary>
	public interface ICrontabProcessorFactory
	{
		/// <summary>
		/// Creates a crontab processor.
		/// </summary>
		/// <param name="crontabExpression">The crontab expression.</param>
		/// <returns></returns>
		ICrontabProcessor Create(string crontabExpression);
	}
}