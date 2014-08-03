using System;
using System.Data;
using System.Reflection;

using NHibernate;
using NHibernate.AdoNet;
using NHibernate.Driver;
using NHibernate.Engine.Query;
using NHibernate.SqlTypes;
using NHibernate.Util;

namespace Simplify.FluentNHibernate.Drivers
{
	/// <summary>
	/// A NHibernate Driver for using the Oracle.ManagedDataAccess DataProvider
	/// </summary>
	public class OracleManagedDataClientDriver : ReflectionBasedDriver, IEmbeddedBatcherFactoryProvider
	{
		private const string DriverAssemblyName = "Oracle.ManagedDataAccess";
		private const string ConnectionTypeName = "Oracle.ManagedDataAccess.Client.OracleConnection";
		private const string CommandTypeName = "Oracle.ManagedDataAccess.Client.OracleCommand";
		private static readonly SqlType GuidSqlType = new SqlType(DbType.Binary, 16);
		private readonly PropertyInfo _oracleCommandBindByName;
		private readonly PropertyInfo _oracleDbType;
		private readonly object _oracleDbTypeRefCursor;
		private readonly object _oracleDbTypeXmlType;

		/// <summary>
		/// Initializes a new instance of <see cref="OracleDataClientDriver"/>.
		/// </summary>
		/// <exception cref="HibernateException">
		/// Thrown when the <c>Oracle.ManagedDataAccess</c> assembly can not be loaded.
		/// </exception>
		public OracleManagedDataClientDriver()
			: base(
				"Oracle.ManagedDataAccess.Client",
				DriverAssemblyName,
				ConnectionTypeName,
				CommandTypeName)
		{
			var oracleCommandType = ReflectHelper.TypeFromAssembly("Oracle.ManagedDataAccess.Client.OracleCommand", DriverAssemblyName, false);
			_oracleCommandBindByName = oracleCommandType.GetProperty("BindByName");

			var parameterType = ReflectHelper.TypeFromAssembly("Oracle.ManagedDataAccess.Client.OracleParameter", DriverAssemblyName, false);
			_oracleDbType = parameterType.GetProperty("OracleDbType");

			var oracleDbTypeEnum = ReflectHelper.TypeFromAssembly("Oracle.ManagedDataAccess.Client.OracleDbType", DriverAssemblyName, false);
			_oracleDbTypeRefCursor = Enum.Parse(oracleDbTypeEnum, "RefCursor");
			_oracleDbTypeXmlType = Enum.Parse(oracleDbTypeEnum, "XmlType");
		}

		/// <summary></summary>
		public override string NamedPrefix
		{
			get { return ":"; }
		}

		/// <summary></summary>
		public override bool UseNamedPrefixInParameter
		{
			get { return true; }
		}

		/// <summary></summary>
		public override bool UseNamedPrefixInSql
		{
			get { return true; }
		}

		/// <remarks>
		/// This adds logic to ensure that a DbType.Boolean parameter is not created since
		/// ODP.NET doesn't support it.
		/// </remarks>
		protected override void InitializeParameter(IDbDataParameter dbParam, string name, SqlType sqlType)
		{
			// if the parameter coming in contains a boolean then we need to convert it 
			// to another type since ODP.NET doesn't support DbType.Boolean
			switch (sqlType.DbType)
			{
				case DbType.Boolean:
					base.InitializeParameter(dbParam, name, SqlTypeFactory.Int16);
					break;
				case DbType.Guid:
					base.InitializeParameter(dbParam, name, GuidSqlType);
					break;
				case DbType.Xml:
					InitializeParameter(dbParam, name, _oracleDbTypeXmlType);
					break;
				default:
					base.InitializeParameter(dbParam, name, sqlType);
					break;
			}
		}

		private void InitializeParameter(IDbDataParameter dbParam, string name, object sqlType)
		{
			dbParam.ParameterName = FormatNameForParameter(name);
			_oracleDbType.SetValue(dbParam, sqlType, null);
		}

		/// <summary>
		/// Override to make any adjustments to the IDbCommand object.  (e.g., Oracle custom OUT parameter)
		/// Parameters have been bound by this point, so their order can be adjusted too.
		/// This is analagous to the RegisterResultSetOutParameter() function in Hibernate.
		/// </summary>
		/// <param name="command"></param>
		protected override void OnBeforePrepare(IDbCommand command)
		{
			base.OnBeforePrepare(command);

			// need to explicitly turn on named parameter binding
			// http://tgaw.wordpress.com/2006/03/03/ora-01722-with-odp-and-command-parameters/
			_oracleCommandBindByName.SetValue(command, true, null);

			var detail = CallableParser.Parse(command.CommandText);

			if (!detail.IsCallable)
				return;

			command.CommandType = CommandType.StoredProcedure;
			command.CommandText = detail.FunctionName;
			_oracleCommandBindByName.SetValue(command, false, null);

			var outCursor = command.CreateParameter();
			_oracleDbType.SetValue(outCursor, _oracleDbTypeRefCursor, null);

			outCursor.Direction = detail.HasReturn ? ParameterDirection.ReturnValue : ParameterDirection.Output;

			command.Parameters.Insert(0, outCursor);
		}

		Type IEmbeddedBatcherFactoryProvider.BatcherFactoryClass
		{
			get { return typeof (OracleDataClientBatchingBatcherFactory); }
		}
	}
}
