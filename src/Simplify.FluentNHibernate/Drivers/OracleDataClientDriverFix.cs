using System;
using System.Data.Common;
using NHibernate.Driver;
using NHibernate.SqlTypes;
using NHibernate.Util;

namespace Simplify.FluentNHibernate.Drivers
{
	/// <summary>
	/// OracleDataClientDriver with NCLOB/CLOBs IDbDataParameter.Value = (string whose length: 4000 > length > 2000 ) bug fix
	/// </summary>
	internal class OracleDataClientDriverFix : OracleDataClientDriver
	{
		public const string DriverAssemblyName = "Oracle.DataAccess";
		private const string OracleParameterType = "Oracle.DataAccess.Client.OracleParameter";
		private const string OracleDbType = "Oracle.DataAccess.Client.OracleDbType";

		/// <summary>
		///  Initializes the parameter.
		/// </summary>
		/// <param name="dbParam">The db param.</param>
		/// <param name="name">The name.</param>
		/// <param name="sqlType">Type of the SQL.</param>
		protected override void InitializeParameter(DbParameter dbParam, string name, SqlType sqlType)
		{
			base.InitializeParameter(dbParam, name, sqlType);

			if (!(sqlType is StringClobSqlType))
				return;

			var property = ReflectHelper.TypeFromAssembly(OracleParameterType, DriverAssemblyName, false)
				.GetProperty("OracleDbType");

			if (property == null)
				throw new SimplifyFluentNHibernateException("OracleDbType property is not found");

			property.SetValue(dbParam,
				Enum.Parse(ReflectHelper.TypeFromAssembly(OracleDbType, DriverAssemblyName, false), "NClob"), null);
		}
	}
}