using System.Collections.Specialized;
using System.Configuration;

namespace Simplify.WindowsServices.Jobs
{
	/// <summary>
	/// Provides service job settings
	/// </summary>
	public class ServiceJobSettings : IServiceJobSettings
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ServiceJobSettings"/> class.
		/// </summary>
		public ServiceJobSettings(string configSectionName = "ServiceSettings")
		{
			ProcessingInterval = 60;
			CleanupOnTaskFinish = true;

			if (!(ConfigurationManager.GetSection(configSectionName) is NameValueCollection config))
				return;

			CrontabExpression = config["CrontabExpression"];

			if (!string.IsNullOrEmpty(CrontabExpression)) return;

			var processingInterval = config["ProcessingInterval"];

			if (!string.IsNullOrEmpty(processingInterval))
				ProcessingInterval = int.Parse(processingInterval);

			var cleanupOnTaskFinish = config["CleanupOnTaskFinish"];

			if (!string.IsNullOrEmpty(cleanupOnTaskFinish))
				CleanupOnTaskFinish = bool.Parse(cleanupOnTaskFinish);

			var maximumParallerTasksCount = config["MaximumParallerTasksCount"];

			if (!string.IsNullOrEmpty(maximumParallerTasksCount))
				MaximumParallerTasksCount = int.Parse(maximumParallerTasksCount);
		}

		/// <summary>
		/// Gets the crontab expression.
		/// </summary>
		/// <value>
		/// The crontab expression.
		/// </value>
		public string CrontabExpression { get; }

		/// <summary>
		/// Gets the service processing interval (sec).
		/// </summary>
		/// <value>
		/// The service processing interval (sec).
		/// </value>
		public int ProcessingInterval { get; }

		/// <summary>
		/// Gets a value indicating whether GC.Collect will be executed on on task finish.
		/// </summary>
		public bool CleanupOnTaskFinish { get; }

		/// <summary>
		/// Gets the maximum allowed paraller tasks of this job.
		/// </summary>
		public int MaximumParallerTasksCount { get; } = 1;
	}
}