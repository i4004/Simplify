using System.Reflection;
using Microsoft.Extensions.Configuration;
using Simplify.System;

namespace Simplify.WindowsServices
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
		/// <param name="automaticallyRegisterUserType">if set to <c>true</c> then user type T will be registered in DIContainer with transient lifetime.</param>
		public SingleTaskServiceHandler(bool automaticallyRegisterUserType = false)
		{
			var assemblyInfo = new AssemblyInfo(Assembly.GetCallingAssembly());
			ServiceName = assemblyInfo.Title;

			AddJob<T>("ServiceSettings", "Run", automaticallyRegisterUserType);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="SingleTaskServiceHandler{T}" /> class.
		/// </summary>
		/// <param name="configuration">The configuration.</param>
		/// <param name="automaticallyRegisterUserType">if set to <c>true</c> then user type T will be registered in DIContainer with transient lifetime.</param>
		public SingleTaskServiceHandler(IConfiguration configuration, bool automaticallyRegisterUserType = false)
		{
			var assemblyInfo = new AssemblyInfo(Assembly.GetCallingAssembly());
			ServiceName = assemblyInfo.Title;

			AddJob<T>(configuration, "ServiceSettings", "Run", automaticallyRegisterUserType);
		}
	}
}