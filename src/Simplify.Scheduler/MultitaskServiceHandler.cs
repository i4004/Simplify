using Microsoft.Extensions.Configuration;
using Simplify.DI;
using Simplify.Scheduler.CommandLine;
using Simplify.Scheduler.Jobs;
using Simplify.Scheduler.Jobs.Crontab;
using Simplify.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Simplify.Scheduler
{
	/// <summary>
	/// Provides class which runs as a windows service and periodically creates a class instances specified in added jobs and launches them in separated thread
	/// </summary>
	public class MultitaskServiceHandler : IDisposable
	{
		private readonly AutoResetEvent _closing = new AutoResetEvent(false);

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
			Console.CancelKeyPress += OnStop;
		}

		/// <summary>
		/// Occurs when exception thrown.
		/// </summary>
		public event ServiceExceptionEventHandler OnException;

		/// <summary>
		/// Gets the name of the service.
		/// </summary>
		/// <value>
		/// The name of the service.
		/// </value>
		public string ServiceName { get; protected set; }

		/// <summary>
		/// Gets or sets the service job factory.
		/// </summary>
		/// <value>
		/// The service job factory.
		/// </value>
		/// <exception cref="ArgumentNullException">value</exception>
		public IServiceJobFactory ServiceJobFactory
		{
			get => _serviceJobFactory ?? (_serviceJobFactory = new ServiceJobFactory(ServiceName));
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
		/// <param name="configuration">The configuration.</param>
		/// <param name="configurationSectionName">Name of the configuration section.</param>
		/// <param name="invokeMethodName">Name of the invoke method.</param>
		/// <param name="startupArgs">The startup arguments.</param>
		public void AddJob<T>(IConfiguration configuration,
			string configurationSectionName = null,
			string invokeMethodName = "Run",
			object startupArgs = null)
			where T : class
		{
			var job = ServiceJobFactory.CreateCrontabServiceJob<T>(configuration, configurationSectionName, invokeMethodName, startupArgs);

			InitializeJob(job);
		}

		/// <summary>
		/// Adds the basic service job.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="invokeMethodName">Name of the invoke method.</param>
		/// <param name="startupArgs">The startup arguments.</param>
		public void AddBasicJob<T>(string invokeMethodName = "Run",
			object startupArgs = null)
			where T : class
		{
			var job = ServiceJobFactory.CreateServiceJob<T>(invokeMethodName, startupArgs);

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
				case ProcessCommandLineResult.SkipSchedulerStart:
					return false;

				case ProcessCommandLineResult.NoArguments:
					ConsoleStart();
					break;
			}

			return true;
		}

		/// <summary>
		/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
		/// </summary>
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>
		/// Called when scheduler is started, main execution starting point.
		/// </summary>
		protected void OnStart()
		{
			Console.WriteLine("Starting Scheduler jobs...");

			foreach (var job in _jobs)
			{
				job.Start();

				if (!(job is ICrontabServiceJob))
					RunBasicJob(job);
			}

			Console.WriteLine("Scheduler jobs started.");
		}

		/// <summary>
		/// Called when scheduler is about to stop, main stopping point
		/// </summary>
		protected void OnStop(object sender, ConsoleCancelEventArgs args)
		{
			Console.WriteLine("Scheduler stopping, waiting for jobs to finish...");

			_shutdownInProcess = true;
			Task[] itemsToWait;

			lock (_workingJobsTasks)
				itemsToWait = _workingJobsTasks.Select(x => x.Task).ToArray();

			Task.WaitAll(itemsToWait);

			Console.WriteLine("All jobs finished.");

			args.Cancel = true;
			_closing.Set();
		}

		/// <summary>
		/// Disposes of the resources (other than memory) used by the <see cref="T:System.ServiceProcess.ServiceBase" />.
		/// </summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
		// ReSharper disable once FlagArgument
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
				foreach (var jobObject in _workingBasicJobs.Select(item => item.Key as IDisposable))
					jobObject?.Dispose();
		}

		private void ConsoleStart()
		{
			OnStart();

			Console.WriteLine("Scheduler started. Press Ctrl + C to shut down.");

			_closing.WaitOne();
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
			var (jobTaskID, job) = (Tuple<long, ICrontabServiceJob>)state;

			try
			{
				using (var scope = DIContainer.Current.BeginLifetimeScope())
				{
					var jobObject = scope.Resolver.Resolve(job.JobClassType);

					InvokeJobMethod(job, jobObject);
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
				if (job.Settings.CleanupOnTaskFinish)
					GC.Collect();

				lock (_workingJobsTasks)
					_workingJobsTasks.Remove(_workingJobsTasks.Single(x => x.ID == jobTaskID));
			}
		}

		private void RunBasicJob(IServiceJob job)
		{
			try
			{
				var scope = DIContainer.Current.BeginLifetimeScope();

				var jobObject = scope.Resolver.Resolve(job.JobClassType);

				InvokeJobMethod(job, jobObject);

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

		private void InvokeJobMethod(IServiceJob job, object jobObject)
		{
			switch (job.InvokeMethodParameterType)
			{
				case InvokeMethodParameterType.Parameterless:
					job.InvokeMethodInfo.Invoke(jobObject, null);
					break;

				case InvokeMethodParameterType.ServiceName:
					job.InvokeMethodInfo.Invoke(jobObject, new object[] { ServiceName });
					break;

				case InvokeMethodParameterType.Args:
					job.InvokeMethodInfo.Invoke(jobObject, new object[] { job.JobArgs });
					break;

				default:
					throw new ArgumentOutOfRangeException();
			}
		}
	}
}