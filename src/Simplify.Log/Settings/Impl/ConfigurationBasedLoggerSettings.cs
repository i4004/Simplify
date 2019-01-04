using System;
using System.Linq;
using Microsoft.Extensions.Configuration;

namespace Simplify.Log.Settings.Impl
{
	/// <summary>
	/// Represent Simplify.Log settings based on IConfiguration
	/// </summary>
	public class ConfigurationBasedLoggerSettings : LoggerSettings
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ConfigurationBasedLoggerSettings" /> class.
		/// </summary>
		/// <param name="configuration">The configuration.</param>
		/// <param name="configSectionName">Name of the logger configuration section in the configuration.</param>
		public ConfigurationBasedLoggerSettings(IConfiguration configuration, string configSectionName = DefaultConfigSectionName)
		{
			FileName = DefaultFileName;
			MaxFileSize = DefaultMaxFileSize;
			PathType = DefaultPathType;
			ShowTraceOutput = false;

			var config = configuration.GetSection(configSectionName);

			if (!config.GetChildren().Any())
				return;

			if (!string.IsNullOrEmpty(config["FileName"]))
				FileName = config["FileName"];

			if (!string.IsNullOrEmpty(config["MaxFileSize"]))
			{
				if (int.TryParse(config["MaxFileSize"], out var buffer))
					MaxFileSize = buffer;
			}

			if (!string.IsNullOrEmpty(config["PathType"]))
				PathType = (LoggerPathType)Enum.Parse(typeof(LoggerPathType), config["PathType"]);

			if (!string.IsNullOrEmpty(config["ShowTraceOutput"]))
			{
				if (bool.TryParse(config["ShowTraceOutput"], out var buffer))
					ShowTraceOutput = buffer;
			}
		}
	}
}