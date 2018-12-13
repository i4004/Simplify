namespace Simplify.FluentNHibernate.Settings
{
	/// <summary>
	/// Provides data-base connection settings class
	/// </summary>
	public class DbConnectionSettings
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="DbConnectionSettings"/> class.
		/// </summary>
		protected DbConnectionSettings()
		{
		}

		/// <summary>
		/// Gets the name of the server.
		/// </summary>
		/// <value>
		/// The name of the server.
		/// </value>
		public string ServerName { get; protected set; }

		/// <summary>
		/// Gets the name of the data base.
		/// </summary>
		/// <value>
		/// The name of the data base.
		/// </value>
		public string DataBaseName { get; protected set; }

		/// <summary>
		/// Gets the name of the user.
		/// </summary>
		/// <value>
		/// The name of the user.
		/// </value>
		public string UserName { get; protected set; }

		/// <summary>
		/// Gets the user password.
		/// </summary>
		/// <value>
		/// The user password.
		/// </value>
		public string UserPassword { get; protected set; }

		/// <summary>
		/// Gets a value indicating whether all executed SQL request should be shown in trace window
		/// </summary>
		public bool ShowSql { get; protected set; }

		/// <summary>
		/// Gets the port number.
		/// </summary>
		public int? Port { get; protected set; }
	}
}