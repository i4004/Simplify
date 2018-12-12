using System;
using System.Collections.Specialized;
using System.Configuration;

namespace Simplify.FluentNHibernate.Settings.Impl
{
	/// <summary>
	/// Provides ConfigurationManager based connection settings
	/// </summary>
	public class ConfigurationManagerBasedDbConnectionSettings : DbConnectionSettings
	{
		/// <summary>
		/// Loads the specified configuration section name containing data-base connection settings.
		/// </summary>
		/// <param name="configSectionName">Name of the configuration section.</param>
		/// <exception cref="DatabaseConnectionConfigurationException"></exception>
		public ConfigurationManagerBasedDbConnectionSettings(string configSectionName = "DatabaseConnectionSettings")
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
	}
}