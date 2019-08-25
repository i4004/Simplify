using Microsoft.Extensions.Configuration;
using System.Linq;

namespace Simplify.Scheduler.Jobs.Settings.Impl
{
	/// <summary>
	/// Provides scheduler job settings based on IConfiguration
	/// </summary>
	public class ConfigurationBasedSchedulerJobSetting : SchedulerJobSettings
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ConfigurationBasedSchedulerJobSetting"/> class.
		/// </summary>
		public ConfigurationBasedSchedulerJobSetting(IConfiguration configuration, string configSectionName = "JobSettings")
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