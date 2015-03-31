namespace Simplify.WindowsServices.Jobs
{
	/// <summary>
	/// Represent windows-service job settings
	/// </summary>
	public interface IServiceJobSettings
	{
		/// <summary>
		/// Gets the crontab expression.
		/// </summary>
		/// <value>
		/// The crontab expression.
		/// </value>
		string CrontabExpression { get; }

		/// <summary>
		/// Gets the service processing interval (sec).
		/// </summary>
		/// <value>
		/// The service processing interval (sec).
		/// </value>
		int ProcessingInterval { get; }
	}
}
