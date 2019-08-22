using System;
using System.Reflection;

namespace Simplify.Scheduler.Jobs
{
	/// <summary>
	/// Represent basic service job
	/// </summary>
	public interface IServiceJob
	{
		/// <summary>
		/// Gets the type of the job class.
		/// </summary>
		/// <value>
		/// The type of the job class.
		/// </value>
		Type JobClassType { get; }

		/// <summary>
		/// Gets the invoke method information.
		/// </summary>
		/// <value>
		/// The invoke method information.
		/// </value>
		MethodInfo InvokeMethodInfo { get; }

		/// <summary>
		/// Gets the type of the invoke method parameter.
		/// </summary>
		/// <value>
		/// The type of the invoke method parameter.
		/// </value>
		InvokeMethodParameterType InvokeMethodParameterType { get; }

		/// <summary>
		/// Gets the job arguments.
		/// </summary>
		/// <value>
		/// The job arguments.
		/// </value>
		IJobArgs JobArgs { get; }

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