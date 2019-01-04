using Simplify.Log.Settings.Impl;

namespace Simplify.Log.Settings
{
	/// <summary>
	/// Represent Simplify.Log settings
	/// </summary>
	public class LoggerSettings : ILoggerSettings
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
		/// Initializes a new instance of the <see cref="ConfigurationManagerBasedLoggerSettings"/> class.
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
		public string FileName { get; protected set; }

		/// <summary>
		/// Maximum file size (kb)
		/// </summary>
		public int MaxFileSize { get; protected set; }

		/// <summary>
		/// File name path type
		/// </summary>
		public LoggerPathType PathType { get; protected set; }

		/// <summary>
		/// Gets a value indicating whether logger messages should be shown in trace window.
		/// </summary>
		public bool ShowTraceOutput { get; protected set; }
	}
}