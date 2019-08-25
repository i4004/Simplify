using Simplify.System;
using System.Reflection;

namespace Simplify.Scheduler
{
	/// <summary>
	/// Provides scheduler which runs a non-timer job (for constant async operations, like TCP/IP server) and launches specified type instance once
	/// </summary>
	public class BasicScheduler<T> : MultitaskScheduler
		where T : class
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="BasicScheduler{T}" /> class.
		/// </summary>
		/// <param name="invokeMethodName">Name of the invoke method.</param>
		/// <param name="startupArgs">The startup arguments.</param>
		public BasicScheduler(string invokeMethodName = "Run", object startupArgs = null)
		{
			var assemblyInfo = new AssemblyInfo(Assembly.GetCallingAssembly());
			AppName = assemblyInfo.Title;

			AddBasicJob<T>(invokeMethodName, startupArgs);
		}
	}
}