namespace Simplify.AutomatedWindowsServices
{
	/// <summary>
	/// Represent windows-service processing settings
	/// </summary>
	public interface IServiceSettings
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
