using System;
using System.Data;

using NHibernate.Driver;
using NHibernate.SqlTypes;
using NHibernate.Util;

namespace Simplify.FluentNHibernate.Drivers
{
	/// <summary>
	/// OracleDataClientDriver with NCLOB/CLOBs IDbDataParameter.Value = (string whose length: 4000 > length > 2000 ) bug fix
	/// </summary>
	class OracleDataClientDriverFix : OracleDataClientDriver
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
		protected override void InitializeParameter(IDbDataParameter dbParam, string name, SqlType sqlType)
		{
			base.InitializeParameter(dbParam, name, sqlType);

			if ((sqlType is StringClobSqlType))
			{
				ReflectHelper.TypeFromAssembly(OracleParameterType, DriverAssemblyName, false)
					.GetProperty("OracleDbType")
					.SetValue(dbParam,
						Enum.Parse(ReflectHelper.TypeFromAssembly(OracleDbType, DriverAssemblyName, false), "NClob"), null);
			}
		}

	}
}
