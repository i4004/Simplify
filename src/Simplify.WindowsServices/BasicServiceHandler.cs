using System.Reflection;
using Simplify.System;

namespace Simplify.WindowsServices
{
	// ReSharper disable once CommentTypo

	/// <summary>
	/// Provides class which runs as non-timer windows service (for constant async operations, like TCP/IP server) and launches specified type instance once
	/// </summary>
	public class BasicServiceHandler<T> : MultitaskServiceHandler
		where T : class
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="BasicServiceHandler{T}"/> class.
		/// </summary>
		/// <param name="automaticallyRegisterUserType">if set to <c>true</c> then user type T will be registered in DIContainer with transient lifetime.</param>
		public BasicServiceHandler(bool automaticallyRegisterUserType = false)
		{
			var assemblyInfo = new AssemblyInfo(Assembly.GetCallingAssembly());
			ServiceName = assemblyInfo.Title;

			AddBasicJob<T>(automaticallyRegisterUserType);
		}
	}
}