namespace Simplify.Scheduler.Jobs.Crontab
{
	/// <summary>
	/// Provides crontab processor factory
	/// </summary>
	public class CrontabProcessorFactory : ICrontabProcessorFactory
	{
		/// <summary>
		/// Creates a crontab processor.
		/// </summary>
		/// <param name="crontabExpression">The crontab expression.</param>
		/// <returns></returns>
		public ICrontabProcessor Create(string crontabExpression)
		{
			return new CrontabProcessor(crontabExpression);
		}
	}
}