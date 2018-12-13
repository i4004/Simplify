using System;
using System.Linq;
using Microsoft.Extensions.Configuration;

namespace Simplify.FluentNHibernate.Settings.Impl
{
	/// <summary>
	/// Provides IConfiguration based connection settings
	/// </summary>
	public class ConfigurationBasedDbConnectionSettings : DbConnectionSettings
	{
		/// <summary>
		/// Loads the specified configuration section name containing data-base connection settings.
		/// </summary>
		/// <param name="configuration">The configuration.</param>
		/// <param name="configSectionName">Name of the configuration section.</param>
		/// <exception cref="ArgumentNullException">configSectionName</exception>
		/// <exception cref="DatabaseConnectionConfigurationException">
		/// Database connection section '{configSectionName}
		/// </exception>
		public ConfigurationBasedDbConnectionSettings(IConfiguration configuration, string configSectionName = "DatabaseConnectionSettings")
		{
			if (string.IsNullOrEmpty(configSectionName)) throw new ArgumentNullException(nameof(configSectionName));

			var config = configuration.GetSection(configSectionName);

			if (!config.GetChildren().Any())
				throw new DatabaseConnectionConfigurationException(
					$"Database connection section '{configSectionName}' was not found");

			ServerName = config["ServerName"];

			if (string.IsNullOrEmpty(ServerName))
				throw new DatabaseConnectionConfigurationException(
					$"Database connection section '{configSectionName}' ServerName property was not specified");

			DataBaseName = config["DataBaseName"];

			if (string.IsNullOrEmpty(DataBaseName))
				throw new DatabaseConnectionConfigurationException(
					$"Database connection section '{configSectionName}' DataBaseName property was not specified");

			UserName = config["UserName"];

			if (string.IsNullOrEmpty(UserName))
				throw new DatabaseConnectionConfigurationException(
					$"Database connection section '{configSectionName}' UserName property was not specified");

			UserPassword = config["UserPassword"];

			var showSqlText = config["ShowSql"];

			if (!string.IsNullOrEmpty(showSqlText))
			{
				if (bool.TryParse(showSqlText, out var buffer))
					ShowSql = buffer;
			}

			var port = config["Port"];

			if (!string.IsNullOrEmpty(port))
			{
				if (int.TryParse(port, out var buffer))
					Port = buffer;
			}
		}
	}
}