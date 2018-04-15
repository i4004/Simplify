using System;
using System.Collections.Specialized;
using System.Configuration;

namespace Simplify.Log
{
	/// <summary>
	/// Represent logger settings class
	/// </summary>
	public class LoggerSettings
	{
		/// <summary>
		/// The default configuration section name
		/// </summary>
		public const string DefaultConfigSectionName = "Logger";

		/// <summary>
		/// The default file name
		/// </summary>
		public const string DefaultFileName = "Logs.log";

		/// <summary>
		/// The default maximum file size in KB
		/// </summary>
		public const int DefaultMaxFileSize = 5000;

		/// <summary>
		/// The default file name path type
		/// </summary>
		public const LoggerPathType DefaultPathType = LoggerPathType.Relative;

		/// <summary>
		/// Initializes a new instance of the <see cref="LoggerSettings" /> class.
		/// </summary>
		/// <param name="configSectionName">Name of the logger configuration section in the configuration file.</param>
		public LoggerSettings(string configSectionName = DefaultConfigSectionName)
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

		/// <summary>
		/// Initializes a new instance of the <see cref="LoggerSettings"/> class.
		/// </summary>
		/// <param name="maxFileSize">Maximum size of the file.</param>
		/// <param name="fileName">Name of the file.</param>
		/// <param name="pathType">Type of the path.</param>
		/// <param name="showTraceOutput">if set to <c>true</c> [show trace output].</param>
		public LoggerSettings(int maxFileSize = DefaultMaxFileSize, string fileName = DefaultFileName, LoggerPathType pathType = DefaultPathType, bool showTraceOutput = false)
		{
			MaxFileSize = maxFileSize;
			FileName = fileName;
			PathType = pathType;
			ShowTraceOutput = showTraceOutput;
		}

		/// <summary>
		/// Log file name
		/// </summary>
		public string FileName { get; }

		/// <summary>
		/// Maximum file size (kb)
		/// </summary>
		public int MaxFileSize { get; }

		/// <summary>
		/// File name path type
		/// </summary>
		public LoggerPathType PathType { get; }

		/// <summary>
		/// Gets a value indicating whether logger messages should be shown in trace window.
		/// </summary>
		public bool ShowTraceOutput { get; }
	}
}