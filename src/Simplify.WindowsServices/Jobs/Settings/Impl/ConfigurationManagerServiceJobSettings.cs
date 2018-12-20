using System.Collections.Specialized;
using System.Configuration;

namespace Simplify.WindowsServices.Jobs.Settings.Impl
{
	/// <summary>
	/// Provides service job settings
	/// </summary>
	public class ConfigurationManagerServiceJobSettings : ServiceJobSettings
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ConfigurationManagerServiceJobSettings"/> class.
		/// </summary>
		public ConfigurationManagerServiceJobSettings(string configSectionName = "ServiceSettings")
		{
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

			var maximumParallelTasksCount = config["MaximumParallelTasksCount"];

			if (!string.IsNullOrEmpty(maximumParallelTasksCount))
				MaximumParallelTasksCount = int.Parse(maximumParallelTasksCount);
		}
	}
}