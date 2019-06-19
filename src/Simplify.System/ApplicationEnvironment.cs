using System;

namespace Simplify.System
{
	/// <summary>
	/// Provides application environment information
	/// </summary>
	public static class ApplicationEnvironment
	{
		/// <summary>
		/// The environment variable name
		/// </summary>
		public const string EnvironmentVariableName = "ASPNETCORE_ENVIRONMENT";

		/// <summary>
		/// The default environment name
		/// </summary>
		public const string DefaultEnvironmentName = "Production";

		private static string _name;

		/// <summary>
		/// Gets or sets the current environment name.
		/// </summary>
		/// <value>
		/// The current environment name.
		/// </value>
		public static string Name
		{
			get
			{
				if (!string.IsNullOrEmpty(_name))
					return _name;

				_name = Environment.GetEnvironmentVariable(EnvironmentVariableName) ?? DefaultEnvironmentName;

				return _name;
			}
			set => _name = value;
		}
	}
}