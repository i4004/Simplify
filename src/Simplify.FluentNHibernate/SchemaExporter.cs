using System;

using FluentNHibernate.Cfg;

using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;

namespace Simplify.FluentNHibernate
{
	/// <summary>
	/// Entities to database exporter
	/// </summary>
	public static class SchemaExporter
	{
		/// <summary>
		/// Create database structure from entities
		/// </summary>
		/// <param name="configuration">The configuration.</param>
		public static void Export(FluentConfiguration configuration)
		{
			if (configuration == null) throw new ArgumentNullException("configuration");

			Configuration config = null;
			configuration.ExposeConfiguration(c => config = c);
			var factory = configuration.BuildSessionFactory();

			var export = new SchemaExport(config);
			export.Execute(false, true, false, factory.OpenSession().Connection, null);
		}
	}
}