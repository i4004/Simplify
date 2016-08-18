using System;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Conventions;
using NHibernate.Driver;

using Simplify.FluentNHibernate.Drivers;

namespace Simplify.FluentNHibernate
{
	/// <summary>
	/// FluentNHibernate.Cfg.FluentConfiguration extensions
	/// </summary>
	public static class ConfigurationExtensions
	{
		/// <summary>
		/// Initialize Oracle connection using Oracle10 client configuration and using oracle client to connect to database
		/// </summary>
		/// <param name="configuration">The fluentNHibernate configuration.</param>
		/// <param name="configSectionName">Configuration section name in App.config or Web.config file</param>
		public static FluentConfiguration InitializeFromConfigOracleClient(this FluentConfiguration configuration, string configSectionName = "DatabaseConnectionSettings")
		{
			if (configuration == null) throw new ArgumentNullException(nameof(configuration));

			var settings = new DbConnectionSettings(configSectionName);

			configuration.Database(OracleClientConfiguration.Oracle10.ConnectionString(c => c
				.Server(settings.ServerName)
				.Port(1521)
				.Instance(settings.DataBaseName)
				.Username(settings.UserName)
				.Password(settings.UserPassword)));

			configuration.ExposeConfiguration(c => c.Properties.Add("hbm2ddl.keywords", "none"));

			if (settings.ShowSql)
				configuration.ExposeConfiguration(x => x.SetInterceptor(new SqlStatementInterceptor()));

			return configuration;
		}

		/// <summary>
		/// Initialize Oracle connection using Oracle10 client configuration and using Oracle.DataAccess.dll to connect to database
		/// </summary>
		/// <param name="configuration">The fluentNHibernate configuration.</param>
		/// <param name="configSectionName">Configuration section name in App.config or Web.config file</param>
		public static FluentConfiguration InitializeFromConfigOracleOdpNetNative(this FluentConfiguration configuration, string configSectionName = "DatabaseConnectionSettings")
		{
			if (configuration == null) throw new ArgumentNullException(nameof(configuration));

			var settings = new DbConnectionSettings(configSectionName);

			configuration.Database(OracleDataClientConfiguration.Oracle10.ConnectionString(c => c
				.Server(settings.ServerName)
				.Port(1521)
				.Instance(settings.DataBaseName)
				.Username(settings.UserName)
				.Password(settings.UserPassword))
				.Driver<OracleDataClientDriverFix>);

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
		public static FluentConfiguration InitializeFromConfigOracleOdpNet(this FluentConfiguration configuration, string configSectionName = "DatabaseConnectionSettings")
		{
			if (configuration == null) throw new ArgumentNullException(nameof(configuration));

			var settings = new DbConnectionSettings(configSectionName);

			configuration.Database(OracleClientConfiguration.Oracle10.ConnectionString(c => c
				.Server(settings.ServerName)
				.Port(1521)
				.Instance(settings.DataBaseName)
				.Username(settings.UserName)
				.Password(settings.UserPassword))
				.Driver<OracleManagedDataClientDriver>());

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
		public static FluentConfiguration InitializeFromConfigMySql(this FluentConfiguration configuration, string configSectionName = "DatabaseConnectionSettings")
		{
			if (configuration == null) throw new ArgumentNullException(nameof(configuration));

			var settings = new DbConnectionSettings(configSectionName);

			configuration.Database(MySQLConfiguration.Standard.ConnectionString(c => c
				.Server(settings.ServerName)
				.Database(settings.DataBaseName)
				.Username(settings.UserName)
				.Password(settings.UserPassword)));

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
		public static FluentConfiguration InitializeFromConfigMsSql(this FluentConfiguration configuration, string configSectionName = "DatabaseConnectionSettings")
		{
			if (configuration == null) throw new ArgumentNullException(nameof(configuration));

			var settings = new DbConnectionSettings(configSectionName);

			configuration.Database(MsSqlConfiguration.MsSql2008.ConnectionString(c => c
				.Server(settings.ServerName)
				.Database(settings.DataBaseName)
				.Username(settings.UserName)
				.Password(settings.UserPassword)));

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
		public static FluentConfiguration InitializeFromConfigPostgreSql(this FluentConfiguration configuration, string configSectionName = "DatabaseConnectionSettings")
		{
			if (configuration == null) throw new ArgumentNullException(nameof(configuration));

			var settings = new DbConnectionSettings(configSectionName);

			configuration.Database(PostgreSQLConfiguration.PostgreSQL82.ConnectionString(c => c
				.Host(settings.ServerName)
				.Database(settings.DataBaseName)
				.Username(settings.UserName)
				.Password(settings.UserPassword)));

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
		public static FluentConfiguration InitializeFromConfigSqLite(this FluentConfiguration configuration, string fileName, bool showSql = false)
		{
			if (configuration == null) throw new ArgumentNullException(nameof(configuration));

			configuration.Database(SQLiteConfiguration.Standard.UsingFile(fileName));

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
		public static FluentConfiguration InitializeFromConfigSqLiteInMemory(this FluentConfiguration configuration, bool showSql = false)
		{
			if (configuration == null) throw new ArgumentNullException(nameof(configuration));

			configuration.Database(SQLiteConfiguration.Standard.InMemory());

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