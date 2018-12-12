using System;
using System.Collections.Specialized;
using System.Configuration;

namespace Simplify.FluentNHibernate
{
	/// <summary>
	/// FluentNHibernate.Extender data-base connection settings class
	/// </summary>
	public class DbConnectionSettings
	{
		/// <summary>
		/// Loads the specified configuration section name containing data-base connection settings.
		/// </summary>
		/// <param name="configSectionName">Name of the configuration section.</param>
		/// <exception cref="DatabaseConnectionConfigurationException"></exception>
		public DbConnectionSettings(string configSectionName = "DatabaseConnectionSettings")
		{
			if (string.IsNullOrEmpty(configSectionName)) throw new ArgumentNullException(nameof(configSectionName));

			var settings = (NameValueCollection)ConfigurationManager.GetSection(configSectionName);

			if (settings == null)
				throw new DatabaseConnectionConfigurationException(
					$"Database connection section '{configSectionName}' was not found");

			ServerName = settings["ServerName"];

			if (string.IsNullOrEmpty(ServerName))
				throw new DatabaseConnectionConfigurationException(
					$"Database connection section '{configSectionName}' ServerName property was not specified");

			DataBaseName = settings["DataBaseName"];

			if (string.IsNullOrEmpty(DataBaseName))
				throw new DatabaseConnectionConfigurationException(
					$"Database connection section '{configSectionName}' DataBaseName property was not specified");

			UserName = settings["UserName"];

			if (string.IsNullOrEmpty(UserName))
				throw new DatabaseConnectionConfigurationException(
					$"Database connection section '{configSectionName}' UserName property was not specified");

			UserPassword = settings["UserPassword"];

			var showSqlText = settings["ShowSql"];

			if (!string.IsNullOrEmpty(showSqlText))
			{
				if (bool.TryParse(showSqlText, out var buffer))
					ShowSql = buffer;
			}

			var port = settings["Port"];

			if (!string.IsNullOrEmpty(port))
			{
				if (int.TryParse(port, out var buffer))
					Port = buffer;
			}
		}

		/// <summary>
		/// Gets the name of the server.
		/// </summary>
		/// <value>
		/// The name of the server.
		/// </value>
		public string ServerName { get; }

		/// <summary>
		/// Gets the name of the data base.
		/// </summary>
		/// <value>
		/// The name of the data base.
		/// </value>
		public string DataBaseName { get; }

		/// <summary>
		/// Gets the name of the user.
		/// </summary>
		/// <value>
		/// The name of the user.
		/// </value>
		public string UserName { get; }

		/// <summary>
		/// Gets the user password.
		/// </summary>
		/// <value>
		/// The user password.
		/// </value>
		public string UserPassword { get; }

		/// <summary>
		/// Gets a value indicating whether all executed SQL request should be shown in trace window
		/// </summary>
		public bool ShowSql { get; }

		/// <summary>
		/// Gets the port number.
		/// </summary>
		public int? Port { get; }
	}
}