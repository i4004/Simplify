using System;
using System.Linq;
using System.Reflection;
using System.ServiceProcess;
using Simplify.DI;
using Simplify.System;

namespace Simplify.WindowsServices
{
	// ReSharper disable once CommentTypo

	/// <summary>
	/// Provides class which runs as non-timer windows service (for constant async operations, like TCP/IP server) and launches specified type instance once
	/// </summary>
	public class BasicServiceHandler<T> : ServiceBase
		where T : class
	{
		private const string InvokeMethodName = "Run";

		private readonly string _serviceName;

		private ILifetimeScope _scope;
		private T _serviceTask;

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
		/// Finalizes an instance of the <see cref="BasicServiceHandler{T}"/> class.
		/// </summary>
		~BasicServiceHandler()
		{
			Dispose(false);
		}

		/// <summary>
		/// Occurs when exception thrown.
		/// </summary>
		public event ServiceExceptionEventHandler OnException;

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
				throw new ServiceInitializationException($"Method {InvokeMethodName} not found in class {taskClassType.Name}");

			var isParameterlessMethod = !invokeMethodInfo.GetParameters().Any();

			try
			{
				_scope = DIContainer.Current.BeginLifetimeScope();

				_serviceTask = _scope.Container.Resolve<T>();

				invokeMethodInfo.Invoke(_serviceTask, isParameterlessMethod ? null : new object[] { _serviceName });
			}
			catch (Exception e)
			{
				if (OnException != null)
					OnException(new ServiceExceptionArgs(ServiceName, e));
				else
					throw;
			}

			base.OnStart(args);
		}

		/// <summary>
		/// Disposes of the resources (other than memory) used by the <see cref="T:System.ServiceProcess.ServiceBase" />.
		/// </summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
		protected override void Dispose(bool disposing)
		{
			var serviceTask = _serviceTask as IDisposable;

			serviceTask?.Dispose();

			_scope?.Dispose();

			base.Dispose(disposing);
		}
	}
}