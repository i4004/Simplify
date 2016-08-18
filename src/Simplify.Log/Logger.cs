using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.IO.Abstractions;
using System.Reflection;
using System.ServiceModel;
using System.Web;

namespace Simplify.Log
{
	/// <summary>
	/// Log class
	/// </summary>
	public class Logger : ILogger
	{
		private static Lazy<IFileSystem> _fileSystem = new Lazy<IFileSystem>(() => new FileSystem());
		private static Lazy<ILogger> _defaultLogger = new Lazy<ILogger>(() => new Logger());

		private readonly object _locker = new object();
		private string _currentLogFileName;

		/// <summary>
		/// Initializes a new instance of the <see cref="Logger"/> class.
		/// </summary>
		/// <param name="configSectionName">Name of the configuration section in the configuration file.</param>
		public Logger(string configSectionName = "Logger")
		{
			Settings = new LoggerSettings(configSectionName);

			Initialize();
		}

		/// <summary>
		/// Gets or sets the file system for Logger IO operations.
		/// </summary>
		/// <value>
		/// The file system for Logger IO operations.
		/// </value>
		internal static IFileSystem FileSystem
		{
			get
			{
				return _fileSystem.Value;
			}

			set
			{
				if (value == null)
					throw new ArgumentNullException(nameof(value));

				_fileSystem = new Lazy<IFileSystem>(() => value);
			}
		}

		/// <summary>
		/// Gets or sets the default logger instance.
		/// </summary>
		/// <value>
		/// The default logger instance.
		/// </value>
		/// <exception cref="System.ArgumentNullException">value</exception>
		public static ILogger Default
		{
			get { return _defaultLogger.Value; }
			set
			{
				if (value == null)
					throw new ArgumentNullException(nameof(value));

				_defaultLogger = new Lazy<ILogger>(() => value);
			}
		}

		/// <summary>
		/// Gets the logger settings.
		/// </summary>
		/// <value>
		/// The logger settings.
		/// </value>
		public LoggerSettings Settings { get; }

		private void Initialize()
		{
			if (Settings.PathType == LoggerPathType.FullPath)
				_currentLogFileName = Settings.FileName;
			else if (HttpContext.Current != null)
				_currentLogFileName = $"{HttpContext.Current.Request.PhysicalApplicationPath}{Settings.FileName}";
			else if (OperationContext.Current != null)
				_currentLogFileName = $"{System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath}{Settings.FileName}";
			else
				_currentLogFileName = $"{Path.GetDirectoryName(Assembly.GetCallingAssembly().Location)}/{Settings.FileName}";
		}

		/// <summary>
		/// Write text message to log file
		/// </summary>
		/// <param name="message">Text message</param>
		/// <returns>Text written to log file (contain time information etc.)</returns>
		public string Write(string message)
		{
			var stack = new StackTrace();
			var functionName = stack.GetFrame(1).GetMethod().Name;

			WriteToFile($" {functionName} : {message}");

			return message;
		}

		/// <summary>
		/// Write data of an exception to log file
		/// </summary>
		/// <param name="e">Exception to get data from</param>
		/// <returns>Text written to log file (contain time information etc.)</returns>
		public string Write(Exception e)
		{
			if (e == null)
				return "";

			var trace = new StackTrace(e, true);

			var fileLineNumber = trace.GetFrame(0).GetFileLineNumber();
			var fileColumnNumber = trace.GetFrame(0).GetFileColumnNumber();

			var positionPrefix = fileLineNumber == 0 && fileColumnNumber == 0 ? "" : $"[{fileLineNumber}:{fileColumnNumber}]";

			var message =
				$"{positionPrefix} {e.GetType()} : {e.Message}{Environment.NewLine}{trace}{GetInnerExceptionData(1, e.InnerException)}";

			WriteToFile(message);

			return message;
		}

		/// <summary>
		/// Write data of an exception to log file and returns written data formatted with HTML line breaks
		/// </summary>
		/// <param name="e">Exception to get data from</param>
		/// <returns>Text written to log file (contain time information etc.) formatted with HTML line breaks</returns>
		public string WriteWeb(Exception e)
		{
			return Write(e).Replace("\r\n", "<br />");
		}

		private string GetInnerExceptionData(int currentLevel, Exception e)
		{
			if (e == null)
				return null;

			var trace = new StackTrace(e, true);

			if (trace.FrameCount == 0)
				return null;

			var fileLineNumber = trace.GetFrame(0).GetFileLineNumber();
			var fileColumnNumber = trace.GetFrame(0).GetFileColumnNumber();
			var positionPrefix = fileLineNumber == 0 && fileColumnNumber == 0 ? "" : $"[{fileLineNumber}:{fileColumnNumber}]";
			var levelText = currentLevel > 1 ? " " + currentLevel.ToString(CultureInfo.InvariantCulture) : "";

			return
				$"[Inner Exception{levelText}]{positionPrefix} {e.GetType()} : {e.Message}{Environment.NewLine}{trace}{GetInnerExceptionData(currentLevel + 1, e.InnerException)}";
		}

		private void WriteToFile(string message)
		{
			lock (_locker)
			{
				if (Settings.ShowTraceOutput)
					Trace.WriteLine(Environment.NewLine + message);

				if (Settings.MaxFileSize != 0)
				{
					if (FileSystem.File.Exists(_currentLogFileName))
					{
						var fileInfo = FileSystem.FileInfo.FromFileName(_currentLogFileName);

						if (fileInfo.Length > Settings.MaxFileSize * 1024)
							FileSystem.File.Delete(_currentLogFileName);
					}
				}

				var writeMessage =
					$"[{DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss:fff", CultureInfo.InvariantCulture)}]{message}{Environment.NewLine}";

				FileSystem.File.AppendAllText(_currentLogFileName, writeMessage);
			}
		}
	}
}