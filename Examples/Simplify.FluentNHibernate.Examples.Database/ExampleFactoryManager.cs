using FluentNHibernate.Cfg;
using NHibernate;
using Simplify.FluentNHibernate.Examples.Database.Mappings;

namespace Simplify.FluentNHibernate.Examples.Database
{
	public class ExampleFactoryManager
	{
		private readonly ISessionFactory _instance;

		public ISessionFactory Instance
		{
			get { return _instance; }
		}

		public ExampleFactoryManager(string configSectionName = "ExampleDatabaseConnectionSettings")
		{
			var configuration = Fluently.Configure();
			configuration.InitializeFromConfigMsSql(configSectionName);
			configuration.AddMappingsFromAssemblyOf<UserMap>();
			_instance = configuration.BuildSessionFactory();
		}
	}
}
