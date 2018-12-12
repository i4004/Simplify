using System;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Conventions;
using Microsoft.Extensions.Configuration;
using NHibernate.Driver;

using Simplify.FluentNHibernate.Drivers;
using Simplify.FluentNHibernate.Settings;
using Simplify.FluentNHibernate.Settings.Impl;

namespace Simplify.FluentNHibernate
{
	/// <summary>
	/// FluentNHibernate.Cfg.FluentConfiguration extensions
	/// </summary>
	public static class ConfigurationExtensions
	{
		#region Oracle Client

		/// <summary>
		/// Initialize Oracle connection using Oracle10 client configuration and using oracle client to connect to database
		/// </summary>
		/// <param name="configuration">The fluentNHibernate configuration.</param>
		/// <param name="configSectionName">Configuration section name in App.config or Web.config file</param>
		/// <param name="additionalClientConfiguration">The additional client configuration.</param>
		/// <returns></returns>
		/// <exception cref="ArgumentNullException"></exception>
		public static FluentConfiguration InitializeFromConfigOracleClient(this FluentConfiguration configuration,
			string configSectionName = "DatabaseConnectionSettings",
			Action<OracleClientConfiguration> additionalClientConfiguration = null)
		{
			if (configuration == null) throw new ArgumentNullException(nameof(configuration));

			return InitializeFromConfigOracleClient(configuration,
				new ConfigurationManagerBasedDbConnectionSettings(configSectionName),
				additionalClientConfiguration);
		}

		/// <summary>
		/// Initialize Oracle connection using Oracle10 client configuration and using oracle client to connect to database
		/// </summary>
		/// <param name="fluentConfiguration">The fluentNHibernate configuration.</param>
		/// <param name="configuration">The database configuration.</param>
		/// <param name="configSectionName">Configuration section name in configuration</param>
		/// <param name="additionalClientConfiguration">The additional client configuration.</param>
		/// <returns></returns>
		/// <exception cref="ArgumentNullException">
		/// fluentConfiguration
		/// or
		/// configuration
		/// </exception>
		public static FluentConfiguration InitializeFromConfigOracleClient(this FluentConfiguration fluentConfiguration,
			IConfiguration configuration,
			string configSectionName = "DatabaseConnectionSettings",
			Action<OracleClientConfiguration> additionalClientConfiguration = null)
		{
			if (fluentConfiguration == null) throw new ArgumentNullException(nameof(fluentConfiguration));
			if (configuration == null) throw new ArgumentNullException(nameof(configuration));

			return InitializeFromConfigOracleClient(fluentConfiguration,
				new ConfigurationBasedDbConnectionSettings(configuration, configSectionName),
				additionalClientConfiguration);
		}

		private static FluentConfiguration InitializeFromConfigOracleClient(
			FluentConfiguration configuration,
			DbConnectionSettings settings,
			Action<OracleClientConfiguration> additionalClientConfiguration = null)
		{
			var clientConfiguration = OracleClientConfiguration.Oracle10.ConnectionString(c =>
				c.Server(settings.ServerName)
					.Port(settings.Port ?? 1521)
					.Instance(settings.DataBaseName)
					.Username(settings.UserName)
					.Password(settings.UserPassword));

			additionalClientConfiguration?.Invoke(clientConfiguration);

			configuration.Database(clientConfiguration);
			configuration.ExposeConfiguration(c => c.Properties.Add("hbm2ddl.keywords", "none"));

			if (settings.ShowSql)
				configuration.ExposeConfiguration(x => x.SetInterceptor(new SqlStatementInterceptor()));

			return configuration;
		}

		#endregion Oracle Client

		/// <summary>
		/// Initialize Oracle connection using Oracle10 client configuration and using Oracle.DataAccess.dll to connect to database
		/// </summary>
		/// <param name="configuration">The fluentNHibernate configuration.</param>
		/// <param name="configSectionName">Configuration section name in App.config or Web.config file</param>
		/// <param name="additionalClientConfiguration">The additional client configuration.</param>
		/// <returns></returns>
		/// <exception cref="ArgumentNullException"></exception>
		public static FluentConfiguration InitializeFromConfigOracleOdpNetNative(this FluentConfiguration configuration,
			string configSectionName = "DatabaseConnectionSettings",
			Action<OracleDataClientConfiguration> additionalClientConfiguration = null)
		{
			if (configuration == null) throw new ArgumentNullException(nameof(configuration));

			var settings = new ConfigurationManagerBasedDbConnectionSettings(configSectionName);

			var clientConfiguration = OracleDataClientConfiguration.Oracle10.ConnectionString(c => c
				.Server(settings.ServerName)
				.Port(settings.Port ?? 1521)
				.Instance(settings.DataBaseName)
				.Username(settings.UserName)
				.Password(settings.UserPassword))
				.Driver<OracleDataClientDriverFix>();

			additionalClientConfiguration?.Invoke(clientConfiguration);

			configuration.Database(clientConfiguration);
			configuration.ExposeConfiguration(c => c.Properties.Add("hbm2ddl.keywords", "none"));

			if (settings.ShowSql)
				configuration.ExposeConfiguration(x => x.SetInterceptor(new SqlStatementInterceptor()));

			return configuration;
		}

		/// <summary>
		/// Initialize Oracle connection using Oracle10 client configuration and using Oracle.ManagedDataAccess.dll to connect to database
		/// </summary>
		/// <param name="configuration">The fluentNHibernate configuration.</param>
		/// <param name="configSectionName">Configuration section name in App.config or Web.config file</param>
		/// <param name="additionalClientConfiguration">The additional client configuration.</param>
		/// <returns></returns>
		/// <exception cref="ArgumentNullException"></exception>
		public static FluentConfiguration InitializeFromConfigOracleOdpNet(this FluentConfiguration configuration,
			string configSectionName = "DatabaseConnectionSettings",
			Action<OracleClientConfiguration> additionalClientConfiguration = null)
		{
			if (configuration == null) throw new ArgumentNullException(nameof(configuration));

			var settings = new ConfigurationManagerBasedDbConnectionSettings(configSectionName);

			var clientConfiguration = OracleClientConfiguration.Oracle10.ConnectionString(c => c
				.Server(settings.ServerName)
				.Port(settings.Port ?? 1521)
				.Instance(settings.DataBaseName)
				.Username(settings.UserName)
				.Password(settings.UserPassword))
				.Driver<OracleManagedDataClientDriver>();

			additionalClientConfiguration?.Invoke(clientConfiguration);

			configuration.Database(clientConfiguration);
			configuration.ExposeConfiguration(c => c.Properties.Add("hbm2ddl.keywords", "none"));

			if (settings.ShowSql)
				configuration.ExposeConfiguration(x => x.SetInterceptor(new SqlStatementInterceptor()));

			return configuration;
		}

		/// <summary>
		/// Initialize MySQL connection using Standard client configuration
		/// </summary>
		/// <param name="configuration">The fluentNHibernate configuration.</param>
		/// <param name="configSectionName">Configuration section name in App.config or Web.config file</param>
		/// <param name="additionalClientConfiguration">The additional client configuration.</param>
		/// <returns></returns>
		/// <exception cref="ArgumentNullException"></exception>
		public static FluentConfiguration InitializeFromConfigMySql(this FluentConfiguration configuration,
			string configSectionName = "DatabaseConnectionSettings",
			Action<MySQLConfiguration> additionalClientConfiguration = null)
		{
			if (configuration == null) throw new ArgumentNullException(nameof(configuration));

			var settings = new ConfigurationManagerBasedDbConnectionSettings(configSectionName);

			var clientConfiguration = MySQLConfiguration.Standard.ConnectionString(c => c
				.Server(settings.ServerName)
				.Database(settings.DataBaseName)
				.Username(settings.UserName)
				.Password(settings.UserPassword));

			additionalClientConfiguration?.Invoke(clientConfiguration);

			configuration.Database(clientConfiguration);
			configuration.ExposeConfiguration(c => c.Properties.Add("hbm2ddl.keywords", "none"));

			if (settings.ShowSql)
				configuration.ExposeConfiguration(x => x.SetInterceptor(new SqlStatementInterceptor()));

			return configuration;
		}

		/// <summary>
		/// Initialize MsSQL connection using MsSql2008 client configuration
		/// </summary>
		/// <param name="configuration">The fluentNHibernate configuration.</param>
		/// <param name="configSectionName">Configuration section name in App.config or Web.config file</param>
		/// <param name="additionalClientConfiguration">The additional client configuration.</param>
		/// <returns></returns>
		/// <exception cref="ArgumentNullException"></exception>
		public static FluentConfiguration InitializeFromConfigMsSql(this FluentConfiguration configuration,
			string configSectionName = "DatabaseConnectionSettings",
			Action<MsSqlConfiguration> additionalClientConfiguration = null)
		{
			if (configuration == null) throw new ArgumentNullException(nameof(configuration));

			var settings = new ConfigurationManagerBasedDbConnectionSettings(configSectionName);

			var clientConfiguration = MsSqlConfiguration.MsSql2008.ConnectionString(c => c
				.Server(settings.ServerName)
				.Database(settings.DataBaseName)
				.Username(settings.UserName)
				.Password(settings.UserPassword));

			additionalClientConfiguration?.Invoke(clientConfiguration);

			configuration.Database(clientConfiguration);
			configuration.ExposeConfiguration(c => c.Properties.Add("hbm2ddl.keywords", "none"));

			if (settings.ShowSql)
				configuration.ExposeConfiguration(x => x.SetInterceptor(new SqlStatementInterceptor()));

			return configuration;
		}

		/// <summary>
		/// Initialize PostgreSQL connection using PostgreSQL82 client configuration
		/// </summary>
		/// <param name="configuration">The fluentNHibernate configuration.</param>
		/// <param name="configSectionName">Configuration section name in App.config or Web.config file</param>
		/// <param name="additionalClientConfiguration">The additional client configuration.</param>
		/// <returns></returns>
		/// <exception cref="ArgumentNullException"></exception>
		public static FluentConfiguration InitializeFromConfigPostgreSql(this FluentConfiguration configuration,
			string configSectionName = "DatabaseConnectionSettings",
			Action<PostgreSQLConfiguration> additionalClientConfiguration = null)
		{
			if (configuration == null) throw new ArgumentNullException(nameof(configuration));

			var settings = new ConfigurationManagerBasedDbConnectionSettings(configSectionName);

			var clientConfiguration = PostgreSQLConfiguration.PostgreSQL82.ConnectionString(c => c
				.Host(settings.ServerName)
				.Port(settings.Port ?? 5432)
				.Database(settings.DataBaseName)
				.Username(settings.UserName)
				.Password(settings.UserPassword));

			additionalClientConfiguration?.Invoke(clientConfiguration);

			configuration.Database(clientConfiguration);
			configuration.ExposeConfiguration(c => c.Properties.Add("hbm2ddl.keywords", "none"));

			if (settings.ShowSql)
				configuration.ExposeConfiguration(x => x.SetInterceptor(new SqlStatementInterceptor()));

			return configuration;
		}

		/// <summary>
		/// Initialize SqLite connection using Standard client configuration
		/// </summary>
		/// <param name="configuration">The fluentNHibernate configuration.</param>
		/// <param name="fileName">Name of the SqLite database file.</param>
		/// <param name="showSql">if set to <c>true</c> then all executed SQL queries will be shown in trace window.</param>
		/// <param name="additionalClientConfiguration">The additional client configuration.</param>
		/// <returns></returns>
		/// <exception cref="ArgumentNullException"></exception>
		public static FluentConfiguration InitializeFromConfigSqLite(this FluentConfiguration configuration,
			string fileName, bool showSql = false,
			Action<SQLiteConfiguration> additionalClientConfiguration = null)
		{
			if (configuration == null) throw new ArgumentNullException(nameof(configuration));

			var clientConfiguration = SQLiteConfiguration.Standard.UsingFile(fileName);

			additionalClientConfiguration?.Invoke(clientConfiguration);

			configuration.Database(clientConfiguration);
			configuration.ExposeConfiguration(c => c.Properties.Add("hbm2ddl.keywords", "none"));

			if (showSql)
				configuration.ExposeConfiguration(x => x.SetInterceptor(new SqlStatementInterceptor()));

			return configuration;
		}

		/// <summary>
		/// Initialize SqLite connection using in memory database
		/// </summary>
		/// <param name="configuration">The fluentNHibernate configuration.</param>
		/// <param name="showSql">if set to <c>true</c> then all executed SQL queries will be shown in trace window.</param>
		/// <param name="additionalClientConfiguration">The additional client configuration.</param>
		/// <returns></returns>
		/// <exception cref="ArgumentNullException"></exception>
		public static FluentConfiguration InitializeFromConfigSqLiteInMemory(this FluentConfiguration configuration,
			bool showSql = false,
			Action<SQLiteConfiguration> additionalClientConfiguration = null)
		{
			if (configuration == null) throw new ArgumentNullException(nameof(configuration));

			var clientConfiguration = SQLiteConfiguration.Standard.InMemory();

			additionalClientConfiguration?.Invoke(clientConfiguration);

			configuration.Database(clientConfiguration);
			configuration.ExposeConfiguration(c => c.Properties.Add("hbm2ddl.keywords", "none"));

			if (showSql)
				configuration.ExposeConfiguration(x => x.SetInterceptor(new SqlStatementInterceptor()));

			return configuration;
		}

		/// <summary>
		/// Adds the mappings from assembly of specified type.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="configuration">The fluentNHibernate configuration.</param>
		/// <param name="conventions">The conventions.</param>
		/// <returns></returns>
		/// <exception cref="System.ArgumentNullException">configuration</exception>
		public static FluentConfiguration AddMappingsFromAssemblyOf<T>(this FluentConfiguration configuration, params IConvention[] conventions)
		{
			if (configuration == null) throw new ArgumentNullException(nameof(configuration));

			configuration.Mappings(m => m.FluentMappings
				.AddFromAssemblyOf<T>()
				.Conventions.Add(conventions));

			return configuration;
		}
	}
}