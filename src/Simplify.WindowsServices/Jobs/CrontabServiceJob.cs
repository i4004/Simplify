using System;
using System.Threading;
using Simplify.WindowsServices.Jobs.Crontab;

namespace Simplify.WindowsServices.Jobs
{
	/// <summary>
	/// Provides crontab service job
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class CrontabServiceJob<T> : ServiceJob<T>, ICrontabServiceJob
	{
		private readonly ICrontabProcessorFactory _crontabProcessorFactory;
		private Timer _timer;

		/// <summary>
		/// Initializes a new instance of the <see cref="CrontabServiceJob{T}" /> class.
		/// </summary>
		/// <param name="settings">The settings.</param>
		/// <param name="crontabProcessorFactory">The crontab processor factory.</param>
		/// <param name="invokeMethodName">Name of the invoke method.</param>
		/// <exception cref="ArgumentNullException">
		/// settings
		/// or
		/// invokeMethodName
		/// </exception>
		/// <exception cref="ServiceInitializationException"></exception>
		/// <exception cref="ArgumentNullException">settings
		/// or
		/// invokeMethodName</exception>
		public CrontabServiceJob(IServiceJobSettings settings, ICrontabProcessorFactory crontabProcessorFactory, string invokeMethodName = "Run")
			: base(invokeMethodName)
		{
			if (settings == null) throw new ArgumentNullException(nameof(settings));
			if (crontabProcessorFactory == null) throw new ArgumentNullException(nameof(crontabProcessorFactory));
			if (invokeMethodName == null) throw new ArgumentNullException(nameof(invokeMethodName));

			Settings = settings;
			_crontabProcessorFactory = crontabProcessorFactory;
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
		public IServiceJobSettings Settings { get; }

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
		/// <exception cref="ServiceInitializationException"></exception>
		public override void Start()
		{
			if (!string.IsNullOrEmpty(Settings.CrontabExpression))
			{
				CrontabProcessor = _crontabProcessorFactory.Create(Settings.CrontabExpression);
				CrontabProcessor.CalculateNextOccurrences();

				_timer = new Timer(OnCronTimerTick, this, 1000, 60000);
			}
			else
				_timer = new Timer(OnStartWork, this, 1000, Settings.ProcessingInterval * 1000);
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