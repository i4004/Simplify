using Microsoft.Extensions.Configuration;
using Simplify.System;
using System.Reflection;

namespace Simplify.Scheduler
{
	/// <summary>
	/// Provides class which runs as a windows service and periodically creates a class instance of specified type and launches it in separated thread
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class SingleTaskServiceHandler<T> : MultitaskServiceHandler
		where T : class
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="SingleTaskServiceHandler{T}" /> class.
		/// </summary>
		/// <param name="configuration">The configuration.</param>
		/// <param name="automaticallyRegisterUserType">if set to <c>true</c> then user type T will be registered in DIContainer with transient lifetime.</param>
		/// <param name="configurationSectionName">Name of the configuration section.</param>
		/// <param name="invokeMethodName">Name of the invoke method.</param>
		/// <param name="startupArgs">The startup arguments.</param>
		public SingleTaskServiceHandler(IConfiguration configuration,
			bool automaticallyRegisterUserType = false,
			string configurationSectionName = "ServiceSettings",
			string invokeMethodName = "Run",
			object startupArgs = null)
		{
			var assemblyInfo = new AssemblyInfo(Assembly.GetCallingAssembly());
			ServiceName = assemblyInfo.Title;

			AddJob<T>(configuration, configurationSectionName, invokeMethodName, automaticallyRegisterUserType, startupArgs);
		}
	}
}