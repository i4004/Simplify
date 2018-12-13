using FluentNHibernate.Cfg;
using Microsoft.Extensions.Configuration;
using NHibernate.Dialect;
using NUnit.Framework;

namespace Simplify.FluentNHibernate.Tests
{
	[TestFixture]
	public class ConfigurationExtensionsTests
	{
		private IConfiguration _configuration;

		[SetUp]
		public void Initialize()
		{
			_configuration = new ConfigurationBuilder()
				.AddJsonFile("appsettings.json", false)
				.Build();
		}

		[Test]
		public void ConfigurationExtensions_InitializeFromConfigOracleClient_InitializedCorrectly()
		{
			Fluently.Configure().InitializeFromConfigOracleClient();
			Fluently.Configure().InitializeFromConfigOracleClient("DatabaseConnectionSettings", c => c.Dialect<Oracle10gDialect>());

			Fluently.Configure().InitializeFromConfigOracleClient(_configuration);

			Assert.Throws<DatabaseConnectionConfigurationException>(() => Fluently.Configure().InitializeFromConfigOracleClient("foo"));
		}

		[Test]
		public void ConfigurationExtensions_InitializeFromConfigOracleOdpNetNative_InitializedCorrectly()
		{
			Fluently.Configure().InitializeFromConfigOracleOdpNetNative();
			Fluently.Configure().InitializeFromConfigOracleOdpNetNative("DatabaseConnectionSettings", c => c.Dialect<Oracle10gDialect>());

			Fluently.Configure().InitializeFromConfigOracleOdpNetNative(_configuration);
		}

		[Test]
		public void ConfigurationExtensions_InitializeFromConfigOracleOdpNet_InitializedCorrectly()
		{
			Fluently.Configure().InitializeFromConfigOracleOdpNet();
			Fluently.Configure().InitializeFromConfigOracleOdpNet("DatabaseConnectionSettings", c => c.Dialect<Oracle10gDialect>());

			Fluently.Configure().InitializeFromConfigOracleOdpNet(_configuration);
		}

		[Test]
		public void ConfigurationExtensions_InitializeFromConfigMySql_InitializedCorrectly()
		{
			Fluently.Configure().InitializeFromConfigMySql();
			Fluently.Configure().InitializeFromConfigMySql("DatabaseConnectionSettings", c => c.Dialect<MySQL5Dialect>());

			Fluently.Configure().InitializeFromConfigMySql(_configuration);
		}

		[Test]
		public void ConfigurationExtensions_InitializeFromConfigMsSql_InitializedCorrectly()
		{
			Fluently.Configure().InitializeFromConfigMsSql();
			Fluently.Configure().InitializeFromConfigMsSql("DatabaseConnectionSettings", c => c.Dialect<MsSql2012Dialect>());

			Fluently.Configure().InitializeFromConfigMsSql(_configuration);
		}

		[Test]
		public void ConfigurationExtensions_InitializeFromConfig_InitializedCorrectly()
		{
			Fluently.Configure().InitializeFromConfigSqLiteInMemory(true);
			Fluently.Configure().InitializeFromConfigSqLiteInMemory(true, c => c.Dialect<SQLiteDialect>());
		}
	}
}