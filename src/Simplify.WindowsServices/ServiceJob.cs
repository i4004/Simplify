using System;
using System.Collections.Generic;
using System.Threading;

namespace Simplify.WindowsServices
{
	/// <summary>
	/// MultiTaskServiceBase service job
	/// </summary>
	public class ServiceJob
	{
		internal int TimerDueTime = -1;
		internal int TimerPeriod = -1;

		internal event WaitCallback Execute;
		
		internal virtual void OnExecute(object state)
		{
			var handler = Execute;
			if (handler != null) handler(state);
		}

		/// <summary>
		/// Timer initialization
		/// </summary>
		/// <param name="workFunction">Job working function</param>
		/// <param name="timerDueTime">Timer start delay</param>
		/// <param name="timerPeriod">Timer working interval</param>
		public ServiceJob(WaitCallback workFunction, int timerDueTime, int timerPeriod)
		{
			Execute = workFunction;

			TimerDueTime = timerDueTime;
			TimerPeriod = timerPeriod;
		}

		/// <summary>
		/// Timer initialization
		/// </summary>
		/// <param name="workFunction">Job working function</param>
		/// <param name="workingPoints">Working time points</param>
		public ServiceJob(WaitCallback workFunction, IList<DateTime> workingPoints)
		{
			if (workingPoints == null) throw new ArgumentNullException("workingPoints");

			Execute = workFunction;
			WorkingPoints = workingPoints;
		}

		/// <summary>
		/// Timer initialization
		/// </summary>
		/// <param name="workFunction">Job working function</param>
		/// <param name="workingPoints">Working time points comma separated, for example: 12:00, 15:00, 16:25</param>
		public ServiceJob(WaitCallback workFunction, string workingPoints)
		{
			if (workingPoints == null) throw new ArgumentNullException("workingPoints");

			Execute = workFunction;

			WorkingPoints = new List<DateTime>();

			if(string.IsNullOrEmpty(workingPoints))
				return;

			foreach (var item in workingPoints.Replace(" ", "").Split(','))
				WorkingPoints.Add(DateTime.Parse(item));
		}

		/// <summary>
		/// The job working points
		/// </summary>
		public readonly IList<DateTime> WorkingPoints;

		/// <summary>
		/// Gets the current executing working point time.
		/// </summary>
		/// <value>
		/// The current executing working point time.
		/// </value>
		public DateTime CurrentWorkingPointTime { get; internal set; }
	}
}
