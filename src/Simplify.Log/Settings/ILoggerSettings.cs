namespace Simplify.Log.Settings
{
	/// <summary>
	/// Represent Simplify.Log settings
	/// </summary>
	public interface ILoggerSettings
	{
		/// <summary>
		/// Log file name
		/// </summary>
		string FileName { get; }

		/// <summary>
		/// Maximum file size (kb)
		/// </summary>
		int MaxFileSize { get; }

		/// <summary>
		/// File name path type
		/// </summary>
		LoggerPathType PathType { get; }

		/// <summary>
		/// Gets a value indicating whether logger messages should be shown in trace window.
		/// </summary>
		bool ShowTraceOutput { get; }
	}
}