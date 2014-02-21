using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Threading;
using System.Threading.Tasks;

namespace Simplify.WindowsServices
{
	/// <summary>
	/// Multi-task scheduler windows service base class
	/// </summary>
	public abstract class MultiTaskServiceBase : ServiceBase
	{
		private readonly IList<ServiceJob> _jobsList = new List<ServiceJob>();
		private readonly IDictionary<ServiceJob, Task> _jobsInWork = new Dictionary<ServiceJob, Task>();
		private readonly IList<Timer> _jobsTimers = new List<Timer>();
		private Timer _workingPointsTimer;

		/// <summary>
		/// Event which is called when service should be initialized, subscribe your settings loading function here etc.
		/// </summary>
		protected event InitializeEventHandler Initialize;

		/// <summary>
		/// Multi-task scheduler windows service initialization method delegate
		/// </summary>
		/// <returns></returns>
		protected delegate bool InitializeEventHandler();

		/// <summary>
		/// When implemented in a derived class, executes when a Start command is sent to the service by the Service Control Manager (SCM) or when the operating system starts (for a service that starts automatically). Specifies actions to take when the service starts.
		/// </summary>
		/// <param name="args">Data passed by the start command.</param>
		/// <exception cref="ServiceInitializationException">Initialize event not set</exception>
		protected override void OnStart(string[] args)
		{
			if (Initialize == null)
				throw new ServiceInitializationException("Initialize event not set");

			if (Initialize())
			{
				var runWorkingPointsTimer = false;

				foreach (var job in _jobsList)
				{
					if (job.WorkingPoints != null)
						runWorkingPointsTimer = true;
					else
						_jobsTimers.Add(new Timer(OnJobTimerTick, job, job.TimerDueTime, job.TimerPeriod));
				}

				if (runWorkingPointsTimer)
					_workingPointsTimer = new Timer(OnWorkingPointsTimerTick, null, 1000, 60000);

				base.OnStart(args);
			}
			else
			{
				ExitCode = 14001;	// Configuration is incorrect
				Stop();
			}
		}

		/// <summary>
		/// When implemented in a derived class, executes when a Stop command is sent to the service by the Service Control Manager (SCM). Specifies actions to take when a service stops running.
		/// </summary>
		protected override void OnStop()
		{
			if (_workingPointsTimer != null)
				_workingPointsTimer.Dispose();

			foreach (var timer in _jobsTimers)
				timer.Dispose();

			Task.WaitAll(_jobsInWork.Values.ToArray());

			base.OnStop();
		}

		/// <summary>
		/// Add service job to jobs list for an execution
		/// </summary>
		/// <param name="job">Service job</param>
		protected void AddJob(ServiceJob job)
		{
			_jobsList.Add(job);
		}

		private void OnWorkingPointsTimerTick(object state)
		{
			var currentTime = DateTime.Now;

			foreach (var job in _jobsList
				.Where(job => job.WorkingPoints != null
					&& job.WorkingPoints.Any(item => item.Hour == currentTime.Hour && item.Minute == currentTime.Minute)))
			{
				job.CurrentWorkingPointTime = currentTime;
				RunJob(job);
			}
		}

		private void OnJobTimerTick(object state)
		{
			RunJob((ServiceJob) state);
		}

		private void RunJob(ServiceJob job)
		{
			lock (_jobsInWork)
			{
				if (_jobsInWork.ContainsKey(job))
					return;

				_jobsInWork.Add(job, Task.Factory.StartNew(OnJobExecute, job));
			}
		}

		private void OnJobExecute(object state)
		{
			var job = (ServiceJob)state;

			job.OnExecute(state);

			lock (_jobsInWork)
				_jobsInWork.Remove(job);
		}

		/// <summary>
		/// Signal service what job has been completed
		/// </summary>
		/// <param name="job">Your ServiceJob instance</param>
		public void SetJobFinished(ServiceJob job)
		{
			_jobsInWork.Remove(job);	
		}

		/// <summary>
		/// Signal service what job has been completed
		/// </summary>
		/// <param name="state">Your ServiceJob instance</param>
		public void SetJobFinished(object state)
		{
			_jobsInWork.Remove((ServiceJob)state);
		}
	}
}
