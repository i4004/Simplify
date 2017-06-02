using FluentNHibernate.Cfg;
using NHibernate;
using Simplify.FluentNHibernate.Examples.Database.Mappings;

namespace Simplify.FluentNHibernate.Examples.Database
{
	public class ExampleFactoryManager
	{
		public ISessionFactory Instance { get; }

		public ExampleFactoryManager(string configSectionName = "ExampleDatabaseConnectionSettings")
		{
			var configuration = Fluently.Configure();
			configuration.InitializeFromConfigMsSql(configSectionName);
			configuration.AddMappingsFromAssemblyOf<UserMap>();
			Instance = configuration.BuildSessionFactory();
		}
	}
}