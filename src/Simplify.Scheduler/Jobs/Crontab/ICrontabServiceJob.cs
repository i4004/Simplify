using Simplify.Scheduler.Jobs.Settings;
using System;
using System.Threading;

namespace Simplify.Scheduler.Jobs.Crontab
{
	/// <summary>
	/// Represent crontab service job
	/// </summary>
	public interface ICrontabServiceJob : IServiceJob, IDisposable
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
		IServiceJobSettings Settings { get; }

		/// <summary>
		/// Gets the crontab processor.
		/// </summary>
		/// <value>
		/// The crontab processor.
		/// </value>
		ICrontabProcessor CrontabProcessor { get; }
	}
}