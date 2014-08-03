using FluentNHibernate.Cfg;

using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;

using NUnit.Framework;

using Simplify.FluentNHibernate.Examples.Database.Mappings;
using Simplify.FluentNHibernate.Examples.Domain.Entities;

namespace Simplify.FluentNHibernate.Tests
{
	[TestFixture]
	public class SessionExtensionsTests
    {
		[Test]
		public void SessionExtensions_Usage_WorkingIsCorrect()
		{
			var configuration = Fluently.Configure();
			configuration.InitializeFromConfigSqLiteInMemory(true);
			configuration.AddMappingsFromAssemblyOf<UserMap>();

			Configuration config = null;
			configuration.ExposeConfiguration(c => config = c);
			var factory = configuration.BuildSessionFactory();
			var session = factory.OpenSession();

			var export = new SchemaExport(config);
			export.Execute(false, true, false, session.Connection, null);

			Assert.IsNull(session.GetObject<User>(x => x.Name == "test"));

			session.Save(new User { Name = "test" });
			session.Flush();

			var user = session.GetObject<User>(x => x.Name == "test");
			Assert.IsNotNull(user);

			user.Name = "foo";
			session.Update(user);
			session.Flush();

			user = session.GetObject<User>(x => x.Name == "foo");
			Assert.IsNotNull(user);

			session.Delete(user);
			session.Flush();
			Assert.IsNull(session.GetObject<User>(x => x.Name == "foo"));
		}
	}
}
