using System.Linq;
using Microsoft.Extensions.Configuration;

namespace Simplify.WindowsServices.Jobs.Settings.Impl
{
	/// <summary>
	/// Provides service job settings based on IConfiguration
	/// </summary>
	public class ConfigurationBasedServiceJobSetting : ServiceJobSettings
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ConfigurationBasedServiceJobSetting"/> class.
		/// </summary>
		public ConfigurationBasedServiceJobSetting(IConfiguration configuration, string configSectionName = "ServiceSettings")
		{
			var config = configuration.GetSection(configSectionName);

			if (!config.GetChildren().Any())
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