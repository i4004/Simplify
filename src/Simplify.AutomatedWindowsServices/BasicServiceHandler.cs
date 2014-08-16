using System.Linq;
using System.Reflection;
using System.ServiceProcess;
using Simplify.DI;
using Simplify.System;

namespace Simplify.AutomatedWindowsServices
{
	/// <summary>
	/// Provides class which runs as non-timer windows service (for constant async operations, like TCP/IP server) and launches specified type instance once
	/// </summary>
	public class BasicServiceHandler<T> : ServiceBase
		where T : class
	{
		private const string InvokeMethodName = "Run";

		private readonly string _serviceName;

		/// <summary>
		/// Initializes a new instance of the <see cref="BasicServiceHandler{T}"/> class.
		/// </summary>
		/// <param name="automaticallyRegisterUserType">if set to <c>true</c> then user type T will be registered in DIContainer with transient lifetime.</param>
		public BasicServiceHandler(bool automaticallyRegisterUserType = false)
		{
			var assemblyInfo = new AssemblyInfo(Assembly.GetCallingAssembly());
			_serviceName = assemblyInfo.Title;

			if (automaticallyRegisterUserType)
				DIContainer.Current.Register<T>(LifetimeType.Transient);
		}

		/// <summary>
		/// When implemented in a derived class, executes when a Start command is sent to the service by the Service Control Manager (SCM) or when the operating system starts (for a service that starts automatically). Specifies actions to take when the service starts.
		/// </summary>
		/// <param name="args">Data passed by the start command.</param>
		/// <exception cref="ServiceInitializationException">
		/// Initialize event not set
		/// or
		/// OnRun event not set
		/// </exception>
		protected override void OnStart(string[] args)
		{
			var taskClassType = typeof(T);

			var invokeMethodInfo = taskClassType.GetMethod(InvokeMethodName);

			if (invokeMethodInfo == null)
				throw new ServiceInitializationException(string.Format("Method {0} not found in class {1}", InvokeMethodName,
					taskClassType.Name));

			var isParameterlessMethod = !invokeMethodInfo.GetParameters().Any();

			using (var scope = DIContainer.Current.BeginLifetimeScope())
			{
				var serviceTask = scope.Container.Resolve<T>();

				invokeMethodInfo.Invoke(serviceTask, isParameterlessMethod ? null : new object[] { _serviceName });
			}

			base.OnStart(args);
		}
	}
}
