using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.ServiceProcess;
using System.Threading.Tasks;
using Simplify.DI;
using Simplify.System;

namespace Simplify.AutomatedWindowsServices
{
	/// <summary>
	/// Provides class which runs as a windows service and periodically creates a class instances specified in added jobs and launches them in separated thread
	/// </summary>
	public class MultitaskTaskServiceHandler : ServiceBase
	{
		private readonly IList<IServiceJob> _jobsList = new List<IServiceJob>();
		private readonly IDictionary<IServiceJob, Task> _jobsInWork = new Dictionary<IServiceJob, Task>();

		private IServiceJobFactory _serviceJobFactory;

		private readonly string _serviceName;

		/// <summary>
		/// Initializes a new instance of the <see cref="MultitaskTaskServiceHandler" /> class.
		/// </summary>
		public MultitaskTaskServiceHandler()
		{
			var assemblyInfo = new AssemblyInfo(Assembly.GetCallingAssembly());
			_serviceName = assemblyInfo.Title;
		}

		/// <summary>
		/// Gets or sets the service job factory.
		/// </summary>
		/// <value>
		/// The service job factory.
		/// </value>
		/// <exception cref="ArgumentNullException">value</exception>
		public IServiceJobFactory ServiceJobFactory
		{
			get
			{
				return _serviceJobFactory ?? (_serviceJobFactory = new ServiceJobFactory());
			}
			set
			{
				if (value == null)
					throw new ArgumentNullException("value");

				_serviceJobFactory = value;
			}
		}

		/// <summary>
		/// Occurs when exception thrown.
		/// </summary>
		public event ServiceExceptionEventHandler OnException;

		#region Service process control

		/// <summary>
		/// When implemented in a derived class, executes when a Start command is sent to the service by the Service Control Manager (SCM) or when the operating system starts (for a service that starts automatically). Specifies actions to take when the service starts.
		/// </summary>
		/// <param name="args">Data passed by the start command.</param>
		protected override void OnStart(string[] args)
		{
			foreach (var job in _jobsList)
				job.Start();

			base.OnStart(args);
		}

		/// <summary>
		/// When implemented in a derived class, executes when a Stop command is sent to the service by the Service Control Manager (SCM). Specifies actions to take when a service stops running.
		/// </summary>
		protected override void OnStop()
		{
			Task.WaitAll(_jobsInWork.Values.ToArray());

			base.OnStop();
		}

		#endregion

		private void OnCronTimerTick(object state)
		{
			var job = (IServiceJob)state;
			var currentTime = TimeProvider.Current.Now;

			if (job.NextOccurrence.Year != currentTime.Year || job.NextOccurrence.Month != currentTime.Month ||
				job.NextOccurrence.Day != currentTime.Day || job.NextOccurrence.Hour != currentTime.Hour ||
				job.NextOccurrence.Minute != currentTime.Minute) return;

			job.NextOccurrence = job.Schedule.GetNextOccurrence(currentTime);

			if (_jobsInWork.ContainsKey(job))
				return;

			OnStartWork(state);
		}

		private void OnStartWork(object state)
		{
			var job = (IServiceJob)state;

			lock (_jobsInWork)
			{
				if (_jobsInWork.ContainsKey(job))
					return;

				_jobsInWork.Add(job, Task.Factory.StartNew(Run, job));
			}
		}

		private void Run(object state)
		{
			var job = (IServiceJob)state;

			try
			{
				using (var scope = DIContainer.Current.BeginLifetimeScope())
				{
					var serviceTask = scope.Container.Resolve(job.JobClassType);

					job.InvokeMethodInfo.Invoke(serviceTask, job.IsParameterlessMethod ? null : new object[] { _serviceName });
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
				lock (_jobsInWork)
					_jobsInWork.Remove(job);
			}
		}

		/// <summary>
		/// Adds the job.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="configurationSectionName">Name of the configuration section.</param>
		/// <param name="invokeMethodName">Name of the invoke method.</param>
		/// <param name="automaticallyRegisterUserType">if set to <c>true</c> then user type T will be registered in DIContainer with transient lifetime.</param>
		public void AddJob<T>(string configurationSectionName = null, string invokeMethodName = "Run", bool automaticallyRegisterUserType = false)
			where T : class
		{
			if (automaticallyRegisterUserType)
				DIContainer.Current.Register<T>(LifetimeType.Transient);

			var job = ServiceJobFactory.CreateServiceJob<T>(configurationSectionName, invokeMethodName);

			job.OnCronTimerTick += OnCronTimerTick;
			job.OnStartWork += OnStartWork;

			_jobsList.Add(job);
		}

		/// <summary>
		/// Adds the job.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="automaticallyRegisterUserType">if set to <c>true</c> then user type T will be registered in DIContainer with transient lifetime.</param>
		public void AddJob<T>(bool automaticallyRegisterUserType)
			where T : class
		{
			AddJob<T>(null, "Run", automaticallyRegisterUserType);
		}
	}
}
