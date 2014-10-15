using System;
using System.Linq;
using System.Reflection;
using System.Threading;
using NCrontab;
using Simplify.System;

namespace Simplify.WindowsServices
{
	/// <summary>
	/// Provides service job
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class ServiceJob<T> : IServiceJob
	{
		private Timer _timer;

		/// <summary>
		/// Initializes a new instance of the <see cref="ServiceJob{T}"/> class.
		/// </summary>
		/// <param name="settings">The settings.</param>
		/// <param name="invokeMethodName">Name of the invoke method.</param>
		/// <exception cref="ArgumentNullException">
		/// settings
		/// or
		/// invokeMethodName
		/// </exception>
		/// <exception cref="ServiceInitializationException"></exception>
		public ServiceJob(IServiceJobSettings settings, string invokeMethodName = "Run")
		{
			if (settings == null) throw new ArgumentNullException("settings");
			if (invokeMethodName == null) throw new ArgumentNullException("invokeMethodName");

			Settings = settings;

			JobClassType = typeof(T);
			InvokeMethodInfo = JobClassType.GetMethod(invokeMethodName);

			if (InvokeMethodInfo == null)
				throw new ServiceInitializationException(string.Format("Method {0} not found in class {1}", invokeMethodName, JobClassType.Name));

			IsParameterlessMethod = !InvokeMethodInfo.GetParameters().Any();

		}

		/// <summary>
		/// Gets the type of the job class.
		/// </summary>
		/// <value>
		/// The type of the job class.
		/// </value>
		public Type JobClassType { get; private set; }

		/// <summary>
		/// Gets the settings.
		/// </summary>
		/// <value>
		/// The settings.
		/// </value>
		public IServiceJobSettings Settings { get; private set; }

		/// <summary>
		/// Gets the invoke method information.
		/// </summary>
		/// <value>
		/// The invoke method information.
		/// </value>
		public MethodInfo InvokeMethodInfo { get; private set; }

		/// <summary>
		/// Gets a value indicating whether invoke method instance is parameterless method.
		/// </summary>
		/// <value>
		/// <c>true</c> if invoke method is parameterless method; otherwise, <c>false</c>.
		/// </value>
		public bool IsParameterlessMethod { get; private set; }

		/// <summary>
		/// Gets the schedule.
		/// </summary>
		/// <value>
		/// The schedule.
		/// </value>
		public CrontabSchedule Schedule { get; private set; }

		/// <summary>
		/// Gets or sets the next occurrence.
		/// </summary>
		/// <value>
		/// The next occurrence.
		/// </value>
		public DateTime NextOccurrence { get; set; }

		/// <summary>
		/// Occurs on cron timer tick.
		/// </summary>
		public event TimerCallback OnCronTimerTick;

		/// <summary>
		/// Occurs on interval timer tick.
		/// </summary>
		public event TimerCallback OnStartWork;

		/// <summary>
		/// Starts this job timer.
		/// </summary>
		/// <exception cref="ServiceInitializationException"></exception>
		public void Start()
		{
			if (!string.IsNullOrEmpty(Settings.CrontabExpression))
			{
				Schedule = CrontabSchedule.TryParse(Settings.CrontabExpression);

				if (Schedule == null)
					throw new ServiceInitializationException(string.Format("Crontab expression parsing failed, expression: '{0}'",
						Settings.CrontabExpression));

				NextOccurrence = Schedule.GetNextOccurrence(TimeProvider.Current.Now);

				_timer = new Timer(OnCronTimerTick, this, 1000, 60000);
			}
			else
				_timer = new Timer(OnStartWork, this, 1000, Settings.ProcessingInterval * 1000);
		}

		/// <summary>
		/// Stops and disposes job timer.
		/// </summary>
		public void Stop()
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