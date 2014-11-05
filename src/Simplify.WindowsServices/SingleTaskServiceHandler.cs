using System;
using System.Linq;
using System.Reflection;
using System.ServiceProcess;
using System.Threading;
using Simplify.DI;
using Simplify.System;

namespace Simplify.WindowsServices
{
	/// <summary>
	/// Provides class which runs as a windows service and periodically creates a class instance of specified type and launches it in separated thread
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class SingleTaskServiceHandler<T> : ServiceBase
		where T : class
	{
		private const string InvokeMethodName = "Run";

		private Timer _timer;
		private ManualResetEvent _waitProcessFinishEvent;
		private MethodInfo _invokeMethodInfo;
		private bool _isParameterlessMethod;

		private IServiceJobSettings _settings;
		private ICrontabProcessor _crontabProcessor;

		/// <summary>
		/// Initializes a new instance of the <see cref="SingleTaskServiceHandler{T}" /> class.
		/// </summary>
		/// <param name="automaticallyRegisterUserType">if set to <c>true</c> then user type T will be registered in DIContainer with transient lifetime.</param>
		public SingleTaskServiceHandler(bool automaticallyRegisterUserType = false)
		{
			if (automaticallyRegisterUserType)
				DIContainer.Current.Register<T>(LifetimeType.Transient);

			var assemblyInfo = new AssemblyInfo(Assembly.GetCallingAssembly());
			ServiceName = assemblyInfo.Title;
		}

		/// <summary>
		/// Occurs when exception thrown.
		/// </summary>
		public event ServiceExceptionEventHandler OnException;

		/// <summary>
		/// Gets or sets the settings.
		/// </summary>
		/// <value>
		/// The settings.
		/// </value>
		/// <exception cref="ArgumentNullException">value</exception>
		public IServiceJobSettings Settings
		{
			get { return _settings ?? (_settings = new ServiceJobSettings()); }
			set
			{
				if (value == null)
					throw new ArgumentNullException("value");

				_settings = value;
			}
		}

		/// <summary>
		/// Gets or sets the crontab processor.
		/// </summary>
		/// <value>
		/// The crontab processor.
		/// </value>
		/// <exception cref="ArgumentNullException">value</exception>
		public ICrontabProcessor CrontabProcessor
		{
			get { return _crontabProcessor ?? (_crontabProcessor = new CrontabProcessor(Settings.CrontabExpression)); }
			set
			{
				if (value == null)
					throw new ArgumentNullException("value");

				_crontabProcessor = value;
			}
		}

		#region Service process control

		/// <summary>
		/// When implemented in a derived class, executes when a Start command is sent to the service by the Service Control Manager (SCM) or when the operating system starts (for a service that starts automatically). Specifies actions to take when the service starts.
		/// </summary>
		/// <param name="args">Data passed by the start command.</param>
		protected override void OnStart(string[] args)
		{
			var taskClassType = typeof (T);

			_invokeMethodInfo = taskClassType.GetMethod(InvokeMethodName);

			if (_invokeMethodInfo == null)
				throw new ServiceInitializationException(string.Format("Method {0} not found in class {1}", InvokeMethodName, taskClassType.Name));

			_isParameterlessMethod = !_invokeMethodInfo.GetParameters().Any();

			if (!string.IsNullOrEmpty(Settings.CrontabExpression))
			{
				CrontabProcessor.CalculateNextOccurrences();

				_timer = new Timer(OnCronTimerTick, null, 1000, 60000);
			}
			else
				_timer = new Timer(OnStartWork, null, 1000, Settings.ProcessingInterval * 1000);

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

		private void OnCronTimerTick(object state)
		{
			if (!CrontabProcessor.IsMatching()) return;

			CrontabProcessor.CalculateNextOccurrences();

			if (_waitProcessFinishEvent != null)
				return;

			OnStartWork(state);
		}

		private void OnStartWork(object state)
		{
			if (_waitProcessFinishEvent != null)
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
					var serviceTask = scope.Container.Resolve<T>();

					_invokeMethodInfo.Invoke(serviceTask, _isParameterlessMethod ? null : new object[] { ServiceName });
				}
			}
			catch (Exception e)
			{
				if (OnException != null)
					OnException(new ServiceExceptionArgs(ServiceName, e));
				else
					throw;
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
