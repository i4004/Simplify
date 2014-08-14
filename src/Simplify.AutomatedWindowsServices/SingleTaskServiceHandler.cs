using System;
using System.Linq;
using System.Reflection;
using System.ServiceProcess;
using System.Threading;
using Simplify.DI;
using Simplify.System;

namespace Simplify.AutomatedWindowsServices
{
	/// <summary>
	/// Provides class which runs as a windows service and periodically creates a class instance of specified type and launches it in separated thread
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class SingleTaskServiceHandler<T> : ServiceBase
		where T : class
	{
		private const string InvokeMethodName = "Run";

		private readonly string _serviceName;

		private Timer _timer;
		private ManualResetEvent _waitProcessFinishEvent;
		private MethodInfo _invokeMethodInfo;
		private bool _isParameterlessMethod;

		private IServiceSettings _settings;

		/// <summary>
		/// Initializes a new instance of the <see cref="SingleTaskServiceHandler{T}"/> class.
		/// </summary>
		public SingleTaskServiceHandler()
		{
			var assemblyInfo = new AssemblyInfo(Assembly.GetCallingAssembly());
			_serviceName = assemblyInfo.Title;
		}

		/// <summary>
		/// Gets or sets the settings.
		/// </summary>
		/// <value>
		/// The settings.
		/// </value>
		/// <exception cref="ArgumentNullException">value</exception>
		public IServiceSettings Settings
		{
			get { return _settings ?? (_settings = new ServiceSettings()); }
			set
			{
				if (value == null)
					throw new ArgumentNullException("value");

				_settings = value;
			}
		}

		#region Service process control

		/// <summary>
		/// When implemented in a derived class, executes when a Start command is sent to the service by the Service Control Manager (SCM) or when the operating system starts (for a service that starts automatically). Specifies actions to take when the service starts.
		/// </summary>
		/// <param name="args">Data passed by the start command.</param>
		protected override void OnStart(string[] args)
		{
			_timer = Settings.WorkingPoints != null
			 ? new Timer(OnTimerTick, null, 1000, 60000)
			 : new Timer(OnTimerTick, null, 1000, Settings.ProcessingInterval * 1000);

			var taskClassType = typeof (T);

			_invokeMethodInfo = taskClassType.GetMethod(InvokeMethodName);

			if (_invokeMethodInfo == null)
				throw new ServiceInitializationException(string.Format("Method {0} not found in class {1}", InvokeMethodName, taskClassType.Name));

			_isParameterlessMethod = !_invokeMethodInfo.GetParameters().Any();

			base.OnStart(args);
		}

		/// <summary>
		/// When implemented in a derived class, executes when a Stop command is sent to the service by the Service Control Manager (SCM). Specifies actions to take when a service stops running.
		/// </summary>
		protected override void OnStop()
		{
			if (_timer != null)
				_timer.Dispose();

			if (_waitProcessFinishEvent != null)
				_waitProcessFinishEvent.WaitOne();

			base.OnStop();
		}

		#endregion

		private void OnTimerTick(object state)
		{
			if (_waitProcessFinishEvent != null)
				return;

			if (Settings.WorkingPoints != null &&
				!Settings.WorkingPoints.Any(item => item.Hour == DateTime.Now.Hour && item.Minute == DateTime.Now.Minute))
				return;

			_waitProcessFinishEvent = new ManualResetEvent(false);

			ThreadPool.QueueUserWorkItem(Run);
		}

		private void Run(object state)
		{
			try
			{
				using (var scope = DIContainer.Current.BeginLifetimeScope())
				{
					var serviceTask = scope.Container.Resolve(typeof(T));

					_invokeMethodInfo.Invoke(serviceTask, _isParameterlessMethod ? null : new object[] { _serviceName });				
				}
			}
			finally
			{
				OnWorkStop();				
			}
		}

		/// <summary>
		/// Indicates to service what Work method is ended his execution, call this method before stopping the service if you are currently executing your Work method
		/// </summary>
		private void OnWorkStop()
		{
			if (_waitProcessFinishEvent == null)
				return;

			_waitProcessFinishEvent.Set();	// Signal the stopped event.
			_waitProcessFinishEvent.Close();
			_waitProcessFinishEvent = null;
		}
	}
}
