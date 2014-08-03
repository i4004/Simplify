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
		/// Log file name
		/// </summary>
		public string FileName { get; private set; }

		/// <summary>
		/// Maximum file size (kb)
		/// </summary>
		public int MaxFileSize { get; private set; }

		/// <summary>
		/// Path type
		/// </summary>
		public LoggerPathType PathType { get; private set; }

		/// <summary>
		/// Gets a value indicating whether logger messages should be shown in trace window.
		/// </summary>
		public bool ShowTraceOutput { get; private set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="LoggerSettings" /> class.
		/// </summary>
		/// <param name="configSectionName">Name of the logger configuration section in the configuration file.</param>
		public LoggerSettings(string configSectionName = "Logger")
		{
			FileName = "Logs.log";
			MaxFileSize = 5000;
			PathType = LoggerPathType.Relative;			
			ShowTraceOutput = false;

			var config = ConfigurationManager.GetSection(configSectionName) as NameValueCollection;

			if (config != null)
			{
				if (!string.IsNullOrEmpty(config["FileName"]))
					FileName = config["FileName"];

				if (!string.IsNullOrEmpty(config["DisableAcspInternalExtensions"]))
				{
					int buffer;

					if (int.TryParse(config["MaxFileSize"], out buffer))
						MaxFileSize = buffer;
				}	

				if (!string.IsNullOrEmpty(config["PathType"]))
					PathType =  (LoggerPathType)Enum.Parse(typeof(LoggerPathType), config["PathType"]);

				if (!string.IsNullOrEmpty(config["ShowTraceOutput"]))
				{
					bool buffer;

					if (bool.TryParse(config["ShowTraceOutput"], out buffer))
						ShowTraceOutput = buffer;
				}	
			}
		}
	}
}