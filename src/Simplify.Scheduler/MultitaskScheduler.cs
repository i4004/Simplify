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
	/// Provides class which periodically creates a class instances specified in added jobs and launches them in separated thread, optimized to work as a console application
	/// </summary>
	public class MultitaskScheduler : IDisposable
	{
		private readonly AutoResetEvent _closing = new AutoResetEvent(false);

		private readonly IList<ISchedulerJob> _jobs = new List<ISchedulerJob>();
		private readonly IList<ICrontabSchedulerJobTask> _workingJobsTasks = new List<ICrontabSchedulerJobTask>();
		private readonly IDictionary<object, ILifetimeScope> _workingBasicJobs = new Dictionary<object, ILifetimeScope>();

		private long _jobTaskID;
		private bool _shutdownInProcess;

		private ISchedulerJobFactory _schedulerJobFactory;
		private ICommandLineProcessor _commandLineProcessor;

		/// <summary>
		/// Initializes a new instance of the <see cref="MultitaskScheduler" /> class.
		/// </summary>
		public MultitaskScheduler()
		{
			var assemblyInfo = new AssemblyInfo(Assembly.GetCallingAssembly());
			AppName = assemblyInfo.Title;
			Console.CancelKeyPress += StopJobs;
		}

		/// <summary>
		/// Occurs when exception thrown.
		/// </summary>
		public event SchedulerExceptionEventHandler OnException;

		/// <summary>
		/// Gets the name of the application.
		/// </summary>
		/// <value>
		/// The name of the application.
		/// </value>
		public string AppName { get; protected set; }

		/// <summary>
		/// Gets or sets the scheduler job factory.
		/// </summary>
		/// <value>
		/// The scheduler job factory.
		/// </value>
		/// <exception cref="ArgumentNullException">value</exception>
		public ISchedulerJobFactory SchedulerJobFactory
		{
			get => _schedulerJobFactory ?? (_schedulerJobFactory = new SchedulerJobFactory(AppName));
			set => _schedulerJobFactory = value ?? throw new ArgumentNullException(nameof(value));
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
			var job = SchedulerJobFactory.CreateCrontabJob<T>(configuration, configurationSectionName, invokeMethodName, startupArgs);

			InitializeJob(job);
		}

		/// <summary>
		/// Adds the basic scheduler job.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="invokeMethodName">Name of the invoke method.</param>
		/// <param name="startupArgs">The startup arguments.</param>
		public void AddBasicJob<T>(string invokeMethodName = "Run",
			object startupArgs = null)
			where T : class
		{
			var job = SchedulerJobFactory.CreateJob<T>(invokeMethodName, startupArgs);

			_jobs.Add(job);
		}

		/// <summary>
		/// Starts the scheduler
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
					StartAndWait();
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
		protected void StartJobs()
		{
			Console.WriteLine("Starting Scheduler jobs...");

			foreach (var job in _jobs)
			{
				job.Start();

				if (!(job is ICrontabSchedulerJob))
					RunBasicJob(job);
			}

			Console.WriteLine("Scheduler jobs started.");
		}

		/// <summary>
		/// Called when scheduler is about to stop, main stopping point
		/// </summary>
		protected void StopJobs(object sender, ConsoleCancelEventArgs args)
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
		/// Releases unmanaged and - optionally - managed resources.
		/// </summary>
		/// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
				foreach (var jobObject in _workingBasicJobs.Select(item => item.Key as IDisposable))
					jobObject?.Dispose();
		}

		private void StartAndWait()
		{
			StartJobs();

			Console.WriteLine("Scheduler started. Press Ctrl + C to shut down.");

			_closing.WaitOne();
		}

		private void InitializeJob(ICrontabSchedulerJob job)
		{
			job.OnCronTimerTick += OnCronTimerTick;
			job.OnStartWork += OnStartWork;

			_jobs.Add(job);
		}

		private void OnCronTimerTick(object state)
		{
			var job = (ICrontabSchedulerJob)state;

			if (!job.CrontabProcessor.IsMatching())
				return;

			job.CrontabProcessor.CalculateNextOccurrences();

			OnStartWork(state);
		}

		private void OnStartWork(object state)
		{
			var job = (ICrontabSchedulerJob)state;

			lock (_workingJobsTasks)
			{
				if (_shutdownInProcess || _workingJobsTasks.Count(x => x.Job == job) >= job.Settings.MaximumParallelTasksCount)
					return;

				_jobTaskID++;

				_workingJobsTasks.Add(new CrontabSchedulerJobTask(_jobTaskID, job,
					Task.Factory.StartNew(Run, new Tuple<long, ICrontabSchedulerJob>(_jobTaskID, job))));
			}
		}

		private void Run(object state)
		{
			var (jobTaskID, job) = (Tuple<long, ICrontabSchedulerJob>)state;

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
					OnException(new SchedulerExceptionArgs(AppName, e));
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

		private void RunBasicJob(ISchedulerJob job)
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
					OnException(new SchedulerExceptionArgs(AppName, e));
				else
					throw;
			}
		}

		private void InvokeJobMethod(ISchedulerJob job, object jobObject)
		{
			switch (job.InvokeMethodParameterType)
			{
				case InvokeMethodParameterType.Parameterless:
					job.InvokeMethodInfo.Invoke(jobObject, null);
					break;

				case InvokeMethodParameterType.AppName:
					job.InvokeMethodInfo.Invoke(jobObject, new object[] { AppName });
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