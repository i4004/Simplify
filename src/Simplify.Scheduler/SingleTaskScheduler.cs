using Microsoft.Extensions.Configuration;
using Simplify.System;
using System.Reflection;

namespace Simplify.Scheduler
{
	/// <summary>
	/// Provides class which periodically creates a class instance of specified type and launches it in separated thread
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class SingleTaskScheduler<T> : MultitaskScheduler
		where T : class
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="SingleTaskScheduler{T}" /> class.
		/// </summary>
		/// <param name="configuration">The configuration.</param>
		/// <param name="configurationSectionName">Name of the configuration section.</param>
		/// <param name="invokeMethodName">Name of the invoke method.</param>
		/// <param name="startupArgs">The startup arguments.</param>
		public SingleTaskScheduler(IConfiguration configuration,
			string configurationSectionName = "JobsSettings",
			string invokeMethodName = "Run",
			object startupArgs = null)
		{
			var assemblyInfo = new AssemblyInfo(Assembly.GetCallingAssembly());
			AppName = assemblyInfo.Title;

			AddJob<T>(configuration, configurationSectionName, invokeMethodName, startupArgs);
		}
	}
}