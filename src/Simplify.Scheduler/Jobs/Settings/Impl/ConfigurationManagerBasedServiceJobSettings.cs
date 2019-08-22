using System.Collections.Specialized;
using System.Configuration;

namespace Simplify.Scheduler.Jobs.Settings.Impl
{
	/// <summary>
	/// Provides service job settings based on ConfigurationManager
	/// </summary>
	public class ConfigurationManagerBasedServiceJobSettings : ServiceJobSettings
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ConfigurationManagerBasedServiceJobSettings"/> class.
		/// </summary>
		public ConfigurationManagerBasedServiceJobSettings(string configSectionName = "ServiceSettings")
		{
			if (!(ConfigurationManager.GetSection(configSectionName) is NameValueCollection config))
				return;

			var cleanupOnTaskFinish = config["CleanupOnTaskFinish"];

			if (!string.IsNullOrEmpty(cleanupOnTaskFinish))
				CleanupOnTaskFinish = bool.Parse(cleanupOnTaskFinish);

			var maximumParallelTasksCount = config["MaximumParallelTasksCount"];

			if (!string.IsNullOrEmpty(maximumParallelTasksCount))
				MaximumParallelTasksCount = int.Parse(maximumParallelTasksCount);

			CrontabExpression = config["CrontabExpression"];

			if (!string.IsNullOrEmpty(CrontabExpression))
				return;

			var processingInterval = config["ProcessingInterval"];

			if (!string.IsNullOrEmpty(processingInterval))
				ProcessingInterval = int.Parse(processingInterval);
		}
	}
}