using Simplify.Scheduler.Jobs.Settings;
using System;
using System.Threading;

namespace Simplify.Scheduler.Jobs.Crontab
{
	/// <summary>
	/// Represent crontab based scheduler job
	/// </summary>
	public interface ICrontabSchedulerJob : ISchedulerJob, IDisposable
	{
		/// <summary>
		/// Occurs on cron timer tick.
		/// </summary>
		event TimerCallback OnCronTimerTick;

		/// <summary>
		/// Occurs on interval timer tick.
		/// </summary>
		event TimerCallback OnStartWork;

		/// <summary>
		/// Gets the settings.
		/// </summary>
		/// <value>
		/// The settings.
		/// </value>
		ISchedulerJobSettings Settings { get; }

		/// <summary>
		/// Gets the crontab processor.
		/// </summary>
		/// <value>
		/// The crontab processor.
		/// </value>
		ICrontabProcessor CrontabProcessor { get; }
	}
}