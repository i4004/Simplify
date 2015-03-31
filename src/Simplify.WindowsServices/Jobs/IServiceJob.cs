using System;
using System.Reflection;
using System.Threading;
using Simplify.WindowsServices.Jobs.Crontab;

namespace Simplify.WindowsServices.Jobs
{
	/// <summary>
	/// Represent service job
	/// </summary>
	public interface IServiceJob : IDisposable
	{
		/// <summary>
		/// Gets the type of the job class.
		/// </summary>
		/// <value>
		/// The type of the job class.
		/// </value>
		Type JobClassType { get; }

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

		/// <summary>
		/// Gets the invoke method information.
		/// </summary>
		/// <value>
		/// The invoke method information.
		/// </value>
		MethodInfo InvokeMethodInfo { get; }

		/// <summary>
		/// Gets a value indicating whether invoke method instance is parameterless method.
		/// </summary>
		/// <value>
		/// <c>true</c> if invoke method is parameterless method; otherwise, <c>false</c>.
		/// </value>
		bool IsParameterlessMethod { get; }

		/// <summary>
		/// Occurs on cron timer tick.
		/// </summary>
		event TimerCallback OnCronTimerTick;

		/// <summary>
		/// Occurs on interval timer tick.
		/// </summary>
		event TimerCallback OnStartWork;

		/// <summary>
		/// Starts this job timer.
		/// </summary>
		void Start();

		/// <summary>
		/// Stops and disposes job timer.
		/// </summary>
		void Stop();
	}
}