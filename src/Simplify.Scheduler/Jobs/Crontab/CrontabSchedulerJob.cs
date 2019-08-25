using Simplify.Scheduler.Jobs.Settings;
using System;
using System.Threading;

namespace Simplify.Scheduler.Jobs.Crontab
{
	/// <summary>
	/// Provides crontab based scheduler job
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class CrontabSchedulerJob<T> : SchedulerJob<T>, ICrontabSchedulerJob
	{
		private readonly ICrontabProcessorFactory _crontabProcessorFactory;
		private Timer _timer;

		/// <summary>
		/// Initializes a new instance of the <see cref="CrontabSchedulerJob{T}" /> class.
		/// </summary>
		/// <param name="settings">The settings.</param>
		/// <param name="crontabProcessorFactory">The crontab processor factory.</param>
		/// <param name="invokeMethodName">Name of the invoke method.</param>
		/// <param name="args">The job arguments.</param>
		/// <exception cref="ArgumentNullException">settings
		/// or
		/// invokeMethodName</exception>
		/// <exception cref="SchedulerInitializationException"></exception>
		/// <exception cref="ArgumentNullException">settings
		/// or
		/// invokeMethodName</exception>
		public CrontabSchedulerJob(ISchedulerJobSettings settings,
			ICrontabProcessorFactory crontabProcessorFactory,
			string invokeMethodName,
			IJobArgs args)
			: base(invokeMethodName, args)
		{
			if (invokeMethodName == null) throw new ArgumentNullException(nameof(invokeMethodName));

			Settings = settings ?? throw new ArgumentNullException(nameof(settings));
			_crontabProcessorFactory = crontabProcessorFactory ?? throw new ArgumentNullException(nameof(crontabProcessorFactory));
		}

		/// <summary>
		/// Occurs on cron timer tick.
		/// </summary>
		public event TimerCallback OnCronTimerTick;

		/// <summary>
		/// Occurs on interval timer tick.
		/// </summary>
		public event TimerCallback OnStartWork;

		/// <summary>
		/// Gets the settings.
		/// </summary>
		/// <value>
		/// The settings.
		/// </value>
		public ISchedulerJobSettings Settings { get; }

		/// <summary>
		/// Gets the crontab processor.
		/// </summary>
		/// <value>
		/// The crontab processor.
		/// </value>
		public ICrontabProcessor CrontabProcessor { get; private set; }

		/// <summary>
		/// Starts this job timer.
		/// </summary>
		/// <exception cref="SchedulerInitializationException"></exception>
		public override void Start()
		{
			if (!string.IsNullOrEmpty(Settings.CrontabExpression))
			{
				CrontabProcessor = _crontabProcessorFactory.Create(Settings.CrontabExpression);
				CrontabProcessor.CalculateNextOccurrences();

				_timer = new Timer(OnCronTimerTick ?? throw new InvalidOperationException("OnCronTimerTick is not assigned"), this, 1000, 60000);
			}
			else
				_timer = new Timer(OnStartWork ?? throw new InvalidOperationException("OnStartWork is not assigned"), this, 1000, Settings.ProcessingInterval * 1000);
		}

		/// <summary>
		/// Stops and disposes job timer.
		/// </summary>
		public override void Stop()
		{
			Dispose();
		}

		/// <summary>
		/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
		/// </summary>
		public void Dispose()
		{
			if (_timer == null) return;

			_timer.Dispose();
			_timer = null;
		}
	}
}