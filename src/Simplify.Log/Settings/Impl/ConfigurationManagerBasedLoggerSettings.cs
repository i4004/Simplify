using System;
using System.Collections.Specialized;
using System.Configuration;

namespace Simplify.Log.Settings.Impl
{
	/// <summary>
	/// Represent Simplify.Log settings based on ConfigurationManager
	/// </summary>
	public class ConfigurationManagerBasedLoggerSettings : LoggerSettings
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ConfigurationManagerBasedLoggerSettings" /> class.
		/// </summary>
		/// <param name="configSectionName">Name of the logger configuration section in the configuration file.</param>
		public ConfigurationManagerBasedLoggerSettings(string configSectionName = DefaultConfigSectionName)
		{
			FileName = DefaultFileName;
			MaxFileSize = DefaultMaxFileSize;
			PathType = DefaultPathType;
			ShowTraceOutput = false;

			if (!(ConfigurationManager.GetSection(configSectionName) is NameValueCollection config))
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