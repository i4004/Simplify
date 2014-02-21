using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Threading;

namespace Simplify.WindowsServices
{
	/// <summary>
	/// Single-task scheduler windows services base class
	/// </summary>
	public abstract class SingleTaskServiceBase : ServiceBase
	{
		private Timer _timer;
		private ManualResetEvent _waitProcessFinishEvent;

		/// <summary>
		/// Subscrive your service working function here
		/// </summary>
		protected event WaitCallback Work;

		/// <summary>
		/// Subscribe your service initialization function here
		/// </summary>
		protected event InitializeEventHandler Initialize;

		/// <summary>
		/// Single-task scheduler windows services initialization method delegate
		/// </summary>
		/// <returns></returns>
		protected delegate bool InitializeEventHandler();

		private int _timerDueTime = -1;
		private int _timerPeriod = -1;
		private IList<DateTime> _workingPoints;

		/// <summary>
		/// Job execution starting time
		/// </summary>
		public DateTime StartTime;

		/// <summary>
		/// When implemented in a derived class, executes when a Start command is sent to the service by the Service Control Manager (SCM) or when the operating system starts (for a service that starts automatically). Specifies actions to take when the service starts.
		/// </summary>
		/// <param name="args">Data passed by the start command.</param>
		/// <exception cref="ServiceInitializationException">
		/// Initialize event not set
		/// or
		/// Work event not set
		/// or
		/// Time is not initialized
		/// </exception>
		protected override void OnStart(string[] args)
		{
			if (Initialize == null)
				throw new ServiceInitializationException("Initialize event not set");

			if (Work == null)
				throw new ServiceInitializationException("Work event not set");

			if (Initialize())
			{
				if((_timerDueTime == -1 || _timerPeriod == -1) && _workingPoints == null)
					throw new ServiceInitializationException("Time is not initialized");

				_timer = _workingPoints != null
					         ? new Timer(OnTimerTick, null, 1000, 60000)
					         : new Timer(OnTimerTick, null, _timerDueTime, _timerPeriod);

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
			if (_timer != null)
				_timer.Dispose();

			if (_waitProcessFinishEvent != null)
				_waitProcessFinishEvent.WaitOne();

			base.OnStop();
		}

		/// <summary>
		/// Timer initialization
		/// </summary>
		/// <param name="timerDueTime">Timer start delay</param>
		/// <param name="timerPeriod">Timer working interval</param>
		protected void InitializeTimer(int timerDueTime, int timerPeriod)
		{
			_timerDueTime = timerDueTime;
			_timerPeriod = timerPeriod;
		}

		/// <summary>
		/// Timer initialization
		/// </summary>
		/// <param name="workingPoints">Working time points</param>
		protected void InitializeTimer(IList<DateTime> workingPoints)
		{
			if (workingPoints == null) throw new ArgumentNullException("workingPoints");

			_workingPoints = workingPoints;
		}

		/// <summary>
		/// Timer initialization
		/// </summary>
		/// <param name="workingPoints">Working time points comma separated, for example: 12:00, 15:00, 16:25</param>
		protected void InitializeTimer(string workingPoints)
		{
			if (workingPoints == null) throw new ArgumentNullException("workingPoints");

			_workingPoints = new List<DateTime>();
			
			foreach (var item in workingPoints.Replace(" ", "").Split(','))
				_workingPoints.Add(DateTime.Parse(item));
		}

		private void OnTimerTick(object state)
		{
			if (_waitProcessFinishEvent != null)
				return;

			if (_workingPoints == null
				|| (_workingPoints != null
					&& _workingPoints.Any(item => item.Hour == DateTime.Now.Hour && item.Minute == DateTime.Now.Minute)))
			{
				_waitProcessFinishEvent = new ManualResetEvent(false);
				ThreadPool.QueueUserWorkItem(OnWork);
			}
		}

		private void OnWork(object state)
		{
			StartTime = DateTime.Now;

			if (Work != null)
				Work(state);

			OnWorkStop();
		}

		/// <summary>
		/// Indicates to service what Work method is ended his execution, call this method before stopping the service if you are currenly executing your Work method
		/// </summary>
		protected void OnWorkStop()
		{
			if (_waitProcessFinishEvent == null)
				return;

			_waitProcessFinishEvent.Set();	// Signal the stopped event.
			_waitProcessFinishEvent.Close();
			_waitProcessFinishEvent = null;
		}
	}
}
