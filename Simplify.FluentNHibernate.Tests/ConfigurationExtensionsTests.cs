using FluentNHibernate.Cfg;

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
		
			Assert.Throws<DatabaseConnectionConfigurationException>(() => Fluently.Configure().InitializeFromConfigOracleClient("foo"));
		}
	}
}
