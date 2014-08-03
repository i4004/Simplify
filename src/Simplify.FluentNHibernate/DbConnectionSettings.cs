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
		/// Gets the name of the server.
		/// </summary>
		/// <value>
		/// The name of the server.
		/// </value>
		public string ServerName { get; private set; }
		/// <summary>
		/// Gets the name of the data base.
		/// </summary>
		/// <value>
		/// The name of the data base.
		/// </value>
		public string DataBaseName { get; private set; }
		/// <summary>
		/// Gets the name of the user.
		/// </summary>
		/// <value>
		/// The name of the user.
		/// </value>
		public string UserName { get; private set; }
		/// <summary>
		/// Gets the user password.
		/// </summary>
		/// <value>
		/// The user password.
		/// </value>
		public string UserPassword { get; private set; }
		/// <summary>
		/// Gets a value indicating whether all executed SQL request should be shown in trace window
		/// </summary>
		public bool ShowSql { get; private set; }

		/// <summary>
		/// Loads the specified configuration section name containing data-base connection settings.
		/// </summary>
		/// <param name="configSectionName">Name of the configuration section.</param>
		/// <exception cref="DatabaseConnectionConfigurationException"></exception>
		public DbConnectionSettings(string configSectionName = "DatabaseConnectionSettings")
		{
			if (string.IsNullOrEmpty(configSectionName)) throw new ArgumentNullException("configSectionName");

			var settings = (NameValueCollection)ConfigurationManager.GetSection(configSectionName);

			if (settings == null)
				throw new DatabaseConnectionConfigurationException(string.Format("Database connection section '{0}' was not found", configSectionName));

			ServerName = settings["ServerName"];

			if(string.IsNullOrEmpty(ServerName))
				throw new DatabaseConnectionConfigurationException(string.Format("Database connection section '{0}' ServerName property was not specified", configSectionName));

			DataBaseName = settings["DataBaseName"];

			if (string.IsNullOrEmpty(DataBaseName))
				throw new DatabaseConnectionConfigurationException(string.Format("Database connection section '{0}' DataBaseName property was not specified", configSectionName));

			UserName = settings["UserName"];

			if (string.IsNullOrEmpty(UserName))
				throw new DatabaseConnectionConfigurationException(string.Format("Database connection section '{0}' UserName property was not specified", configSectionName));

			UserPassword = settings["UserPassword"];

			var showSqlText = settings["ShowSql"];

			if (!string.IsNullOrEmpty(showSqlText))
			{
				bool buffer;

				if (bool.TryParse(showSqlText, out buffer))
					ShowSql = buffer;
			}
		}		 
	}
}
