using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.ServiceProcess;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Simplify.DI;
using Simplify.System;
using Simplify.WindowsServices.CommandLine;
using Simplify.WindowsServices.Jobs;
using Simplify.WindowsServices.Jobs.Crontab;

namespace Simplify.WindowsServices
{
	/// <summary>
	/// Provides class which runs as a windows service and periodically creates a class instances specified in added jobs and launches them in separated thread
	/// </summary>
	public class MultitaskServiceHandler : ServiceBase
	{
		private readonly IList<IServiceJob> _jobs = new List<IServiceJob>();
		private readonly IList<ICrontabServiceJobTask> _workingJobsTasks = new List<ICrontabServiceJobTask>();
		private readonly IDictionary<object, ILifetimeScope> _workingBasicJobs = new Dictionary<object, ILifetimeScope>();

		private long _jobTaskID;
		private bool _shutdownInProcess;

		private IServiceJobFactory _serviceJobFactory;
		private ICommandLineProcessor _commandLineProcessor;

		/// <summary>
		/// Initializes a new instance of the <see cref="MultitaskServiceHandler" /> class.
		/// </summary>
		public MultitaskServiceHandler()
		{
			var assemblyInfo = new AssemblyInfo(Assembly.GetCallingAssembly());
			ServiceName = assemblyInfo.Title;
		}

		/// <summary>
		/// Occurs when exception thrown.
		/// </summary>
		public event ServiceExceptionEventHandler OnException;

		/// <summary>
		/// Gets or sets the service job factory.
		/// </summary>
		/// <value>
		/// The service job factory.
		/// </value>
		/// <exception cref="ArgumentNullException">value</exception>
		public IServiceJobFactory ServiceJobFactory
		{
			get => _serviceJobFactory ?? (_serviceJobFactory = new ServiceJobFactory());
			set => _serviceJobFactory = value ?? throw new ArgumentNullException(nameof(value));
		}

		/// <summary>
		/// Gets or sets the current command line processor.
		/// </summary>
		/// <exception cref="ArgumentNullException"></exception>
		public ICommandLineProcessor CommandLineProcessor
		{
			get => _commandLineProcessor ?? (_commandLineProcessor = new CommandLineProcessor());
			set => _commandLineProcessor = value ?? throw new ArgumentNullException(nameof(value));
		}

		/// <summary>
		/// Adds the job.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="configurationSectionName">Name of the configuration section.</param>
		/// <param name="invokeMethodName">Name of the invoke method.</param>
		/// <param name="automaticallyRegisterUserType">if set to <c>true</c> then user type T will be registered in DIContainer with transient lifetime.</param>
		public void AddJob<T>(string configurationSectionName = null, string invokeMethodName = "Run",
			bool automaticallyRegisterUserType = false)
			where T : class
		{
			if (automaticallyRegisterUserType)
				DIContainer.Current.Register<T>(LifetimeType.Transient);

			var job = ServiceJobFactory.CreateCrontabServiceJob<T>(configurationSectionName, invokeMethodName);

			InitializeJob(job);
		}

		/// <summary>
		/// Adds the job.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="configuration">The configuration.</param>
		/// <param name="configurationSectionName">Name of the configuration section.</param>
		/// <param name="invokeMethodName">Name of the invoke method.</param>
		/// <param name="automaticallyRegisterUserType">if set to <c>true</c> then user type T will be registered in DIContainer with transient lifetime.</param>
		public void AddJob<T>(IConfiguration configuration, string configurationSectionName = null, string invokeMethodName = "Run",
			bool automaticallyRegisterUserType = false)
			where T : class
		{
			if (automaticallyRegisterUserType)
				DIContainer.Current.Register<T>(LifetimeType.Transient);

			var job = ServiceJobFactory.CreateCrontabServiceJob<T>(configuration, configurationSectionName, invokeMethodName);

			InitializeJob(job);
		}

		/// <summary>
		/// Adds the service job.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="automaticallyRegisterUserType">if set to <c>true</c> then user type T will be registered in DIContainer with transient lifetime.</param>
		public void AddJob<T>(bool automaticallyRegisterUserType)
			where T : class
		{
			AddJob<T>(null, "Run", automaticallyRegisterUserType);
		}

		/// <summary>
		/// Adds the service job.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="configuration">The configuration.</param>
		/// <param name="automaticallyRegisterUserType">if set to <c>true</c> then user type T will be registered in DIContainer with transient lifetime.</param>
		public void AddJob<T>(IConfiguration configuration, bool automaticallyRegisterUserType)
			where T : class
		{
			AddJob<T>(configuration, null, "Run", automaticallyRegisterUserType);
		}

		/// <summary>
		/// Adds the basic service job.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="automaticallyRegisterUserType">if set to <c>true</c> then user type T will be registered in DIContainer with transient lifetime.</param>
		/// <param name="invokeMethodName">Name of the invoke method.</param>
		public void AddBasicJob<T>(bool automaticallyRegisterUserType = false, string invokeMethodName = "Run")
			where T : class
		{
			if (automaticallyRegisterUserType)
				DIContainer.Current.Register<T>(LifetimeType.Transient);

			var job = ServiceJobFactory.CreateServiceJob<T>(invokeMethodName);

			_jobs.Add(job);
		}

		/// <summary>
		/// Starts the windows-service.
		/// </summary>
		/// <param name="args">The arguments.</param>
		public bool Start(string[] args = null)
		{
			var commandLineProcessResult = CommandLineProcessor.ProcessCommandLineArguments(args);

			switch (commandLineProcessResult)
			{
				case ProcessCommandLineResult.SkipServiceStart:
					return false;

				case ProcessCommandLineResult.NoArguments:
					ServiceBase.Run(this);
					break;
			}

			return true;
		}

		/// <summary>
		/// Disposes of the resources (other than memory) used by the <see cref="T:System.ServiceProcess.ServiceBase" />.
		/// </summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				foreach (var jobObject in _workingBasicJobs.Select(item => item.Key as IDisposable))
					jobObject?.Dispose();
			}

			base.Dispose(disposing);
		}

		/// <summary>
		/// When implemented in a derived class, executes when a Start command is sent to the service by the Service Control Manager (SCM) or when the operating system starts (for a service that starts automatically). Specifies actions to take when the service starts.
		/// </summary>
		/// <param name="args">Data passed by the start command.</param>
		protected override void OnStart(string[] args)
		{
			foreach (var job in _jobs)
			{
				job.Start();

				if (!(job is ICrontabServiceJob))
					RunBasicJob(job);
			}

			base.OnStart(args);
		}

		/// <summary>
		/// When implemented in a derived class, executes when a Stop command is sent to the service by the Service Control Manager (SCM). Specifies actions to take when a service stops running.
		/// </summary>
		protected override void OnStop()
		{
			_shutdownInProcess = true;
			Task[] itemsToWait;

			lock (_workingJobsTasks)
				itemsToWait = _workingJobsTasks.Select(x => x.Task).ToArray();

			Task.WaitAll(itemsToWait);

			base.OnStop();
		}

		private void InitializeJob(ICrontabServiceJob job)
		{
			job.OnCronTimerTick += OnCronTimerTick;
			job.OnStartWork += OnStartWork;

			_jobs.Add(job);
		}

		private void OnCronTimerTick(object state)
		{
			var job = (ICrontabServiceJob)state;

			if (!job.CrontabProcessor.IsMatching())
				return;

			job.CrontabProcessor.CalculateNextOccurrences();

			OnStartWork(state);
		}

		private void OnStartWork(object state)
		{
			var job = (ICrontabServiceJob)state;

			lock (_workingJobsTasks)
			{
				if (_shutdownInProcess || _workingJobsTasks.Count(x => x.Job == job) >= job.Settings.MaximumParallelTasksCount)
					return;

				_jobTaskID++;

				_workingJobsTasks.Add(new CrontabServiceJobTask(_jobTaskID, job,
					Task.Factory.StartNew(Run, new Tuple<long, ICrontabServiceJob>(_jobTaskID, job))));
			}
		}

		private void Run(object state)
		{
			var job = (Tuple<long, ICrontabServiceJob>)state;

			try
			{
				using (var scope = DIContainer.Current.BeginLifetimeScope())
				{
					var jobObject = scope.Resolver.Resolve(job.Item2.JobClassType);

					job.Item2.InvokeMethodInfo.Invoke(jobObject, job.Item2.IsParameterlessMethod ? null : new object[] { ServiceName });
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
				if (job.Item2.Settings.CleanupOnTaskFinish)
					GC.Collect();

				lock (_workingJobsTasks)
					_workingJobsTasks.Remove(_workingJobsTasks.Single(x => x.ID == job.Item1));
			}
		}

		private void RunBasicJob(IServiceJob job)
		{
			try
			{
				var scope = DIContainer.Current.BeginLifetimeScope();

				var jobObject = scope.Resolver.Resolve(job.JobClassType);

				job.InvokeMethodInfo.Invoke(jobObject, job.IsParameterlessMethod ? null : new object[] { ServiceName });

				_workingBasicJobs.Add(jobObject, scope);
			}
			catch (Exception e)
			{
				if (OnException != null)
					OnException(new ServiceExceptionArgs(ServiceName, e));
				else
					throw;
			}
		}
	}
}