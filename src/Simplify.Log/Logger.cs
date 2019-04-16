using Microsoft.Extensions.Configuration;
using Simplify.Log.Settings;
using Simplify.Log.Settings.Impl;
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
		private static Lazy<ILogger> _defaultLogger = new Lazy<ILogger>(() => new Logger(LoggerSettings.DefaultConfigSectionName));

		private readonly object _locker = new object();
		private string _currentLogFileName;

		/// <summary>
		/// Initializes a new instance of the <see cref="Logger"/> class.
		/// </summary>
		/// <param name="configSectionName">Name of the configuration section in the configuration file.</param>
		public Logger(string configSectionName = LoggerSettings.DefaultConfigSectionName)
		{
			Settings = new ConfigurationManagerBasedLoggerSettings(configSectionName);

			Initialize();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Logger" /> class.
		/// </summary>
		/// <param name="configuration">The configuration.</param>
		/// <param name="configSectionName">Name of the configuration section in the configuration.</param>
		public Logger(IConfiguration configuration, string configSectionName = LoggerSettings.DefaultConfigSectionName)
		{
			Settings = new ConfigurationBasedLoggerSettings(configuration, configSectionName);

			Initialize();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Logger"/> class.
		/// </summary>
		/// <param name="maxFileSize">Maximum size of the file.</param>
		/// <param name="fileName">Name of the file.</param>
		/// <param name="pathType">Type of the path.</param>
		/// <param name="showTraceOutput">if set to <c>true</c> [show trace output].</param>
		public Logger(int maxFileSize = LoggerSettings.DefaultMaxFileSize, string fileName = LoggerSettings.DefaultFileName,
			LoggerPathType pathType = LoggerSettings.DefaultPathType, bool showTraceOutput = false)
		{
			Settings = new LoggerSettings(maxFileSize, fileName, pathType, showTraceOutput);

			Initialize();
		}

		/// <summary>
		/// Gets or sets the default logger instance.
		/// </summary>
		/// <value>
		/// The default logger instance.
		/// </value>
		/// <exception cref="ArgumentNullException">value</exception>
		public static ILogger Default
		{
			get => _defaultLogger.Value;
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
		public ILoggerSettings Settings { get; }

		/// <summary>
		/// Gets or sets the file system for Logger IO operations.
		/// </summary>
		/// <value>
		/// The file system for Logger IO operations.
		/// </value>
		internal static IFileSystem FileSystem
		{
			get => _fileSystem.Value;

			set
			{
				if (value == null)
					throw new ArgumentNullException(nameof(value));

				_fileSystem = new Lazy<IFileSystem>(() => value);
			}
		}

		/// <summary>
		/// Write text message to log file
		/// </summary>
		/// <param name="message">Text message</param>
		/// <returns>Text written to log file (contain time information etc.)</returns>
		public string Write(string message)
		{
			var generatedMessage = Generate(message, false);

			WriteToFile(generatedMessage);

			return generatedMessage;
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

			var message = Generate(e, false);

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

		/// <summary>
		/// Generates the log message (contain time information etc.).
		/// </summary>
		/// <param name="message">Text message</param>
		/// <param name="addTimeInformation">Adds time information prefix to the generated message.</param>
		/// <returns></returns>
		public string Generate(string message, bool addTimeInformation = true)
		{
			var stack = new StackTrace();
			var functionName = stack.GetFrame(1).GetMethod().Name;

			var generatedMessage = $" {functionName} : {message}";

			if (addTimeInformation)
				generatedMessage = AddTimeInformation(generatedMessage);

			return generatedMessage;
		}

		/// <summary>
		/// Generates full stack data of an exception
		/// </summary>
		/// <param name="e">Exception to get data from</param>
		/// <param name="addTimeInformation">Adds time information prefix to the generated message.</param>
		/// <returns></returns>
		public string Generate(Exception e, bool addTimeInformation = true)
		{
			if (e == null)
				return "";

			var trace = new StackTrace(e, true);

			var fileLineNumber = trace.GetFrame(0).GetFileLineNumber();
			var fileColumnNumber = trace.GetFrame(0).GetFileColumnNumber();

			var positionPrefix = fileLineNumber == 0 && fileColumnNumber == 0 ? "" : $"[{fileLineNumber}:{fileColumnNumber}]";

			var generatedMessage = $"{positionPrefix} {e.GetType()} : {e.Message}{Environment.NewLine}{trace}{GetInnerExceptionData(1, e.InnerException)}";

			if (addTimeInformation)
				generatedMessage = AddTimeInformation(generatedMessage);

			return generatedMessage;
		}

		/// <summary>
		/// Generates full stack data of an exception formatted with HTML line breaks
		/// </summary>
		/// <param name="e">Exception to get data from</param>
		/// <param name="addTimeInformation">Adds time information prefix to the generated message.</param>
		/// <returns></returns>
		public string GenerateWeb(Exception e, bool addTimeInformation = true)
		{
			return Generate(e, addTimeInformation).Replace("\r\n", "<br />");
		}

		private static string AddTimeInformation(string message)
		{
			return $"[{DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss:fff", CultureInfo.InvariantCulture)}]{message}";
		}

		private static string GetInnerExceptionData(int currentLevel, Exception e)
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

			return $"[Inner Exception{levelText}]{positionPrefix} {e.GetType()} : {e.Message}{Environment.NewLine}{trace}{GetInnerExceptionData(currentLevel + 1, e.InnerException)}";
		}

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

				var writeMessage = AddTimeInformation(message + Environment.NewLine);

				FileSystem.File.AppendAllText(_currentLogFileName, writeMessage);
			}
		}
	}
}