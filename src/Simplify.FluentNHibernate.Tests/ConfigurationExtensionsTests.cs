using FluentNHibernate.Cfg;
using NHibernate.Dialect;
using NUnit.Framework;

namespace Simplify.FluentNHibernate.Tests
{
	[TestFixture]
	public class ConfigurationExtensionsTests
	{
		[Test]
		public void ConfigurationExtensions_InitializeFromConfig_InitializedCorrectly()
		{
			Fluently.Configure().InitializeFromConfigMsSql();
			Fluently.Configure().InitializeFromConfigMySql();
			Fluently.Configure().InitializeFromConfigOracleClient();
			Fluently.Configure().InitializeFromConfigOracleOdpNet();
			Fluently.Configure().InitializeFromConfigOracleOdpNetNative();
			Fluently.Configure().InitializeFromConfigSqLiteInMemory(true);

			Fluently.Configure().InitializeFromConfigMsSql("DatabaseConnectionSettings", c => c.Dialect<MsSql2012Dialect>());
			Fluently.Configure().InitializeFromConfigMySql("DatabaseConnectionSettings", c => c.Dialect<MySQL5Dialect>());
			Fluently.Configure().InitializeFromConfigOracleClient("DatabaseConnectionSettings", c => c.Dialect<Oracle10gDialect>());
			Fluently.Configure().InitializeFromConfigOracleOdpNet("DatabaseConnectionSettings", c => c.Dialect<Oracle10gDialect>());
			Fluently.Configure().InitializeFromConfigOracleOdpNetNative("DatabaseConnectionSettings", c => c.Dialect<Oracle10gDialect>());
			Fluently.Configure().InitializeFromConfigSqLiteInMemory(true, c => c.Dialect<SQLiteDialect>());

			Assert.Throws<DatabaseConnectionConfigurationException>(() => Fluently.Configure().InitializeFromConfigOracleClient("foo"));
		}
	}
}