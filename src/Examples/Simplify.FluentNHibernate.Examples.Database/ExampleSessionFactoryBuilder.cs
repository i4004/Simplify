using FluentNHibernate.Cfg;
using NHibernate;
using Simplify.FluentNHibernate.Examples.Database.Mappings;

namespace Simplify.FluentNHibernate.Examples.Database
{
	public class ExampleSessionFactoryBuilder
	{
		public ExampleSessionFactoryBuilder(string configSectionName = "ExampleDatabaseConnectionSettings")
		{
			var configuration = Fluently.Configure();
			configuration.InitializeFromConfigMsSql(configSectionName);
			configuration.AddMappingsFromAssemblyOf<UserMap>();
			Instance = configuration.BuildSessionFactory();
		}

		public ISessionFactory Instance { get; }
	}
}