using System;
using System.Linq;
using System.Reflection;

namespace Simplify.Scheduler.Jobs
{
	/// <summary>
	/// Provides basic scheduler job
	/// </summary>
	/// <seealso cref="ISchedulerJob" />
	public class SchedulerJob<T> : ISchedulerJob
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="SchedulerJob{T}" /> class.
		/// </summary>
		/// <param name="invokeMethodName">Name of the invoke method.</param>
		/// <param name="args">The arguments.</param>
		/// <exception cref="ArgumentNullException">invokeMethodName</exception>
		/// <exception cref="SchedulerInitializationException"></exception>
		public SchedulerJob(string invokeMethodName, IJobArgs args)
		{
			if (invokeMethodName == null) throw new ArgumentNullException(nameof(invokeMethodName));

			JobClassType = typeof(T);
			JobArgs = args ?? throw new ArgumentNullException(nameof(args));

			BuildInvokeMethodInfo(invokeMethodName);
		}

		/// <summary>
		/// Gets the type of the job class.
		/// </summary>
		/// <value>
		/// The type of the job class.
		/// </value>
		public Type JobClassType { get; }

		/// <summary>
		/// Gets the invoke method information.
		/// </summary>
		/// <value>
		/// The invoke method information.
		/// </value>
		public MethodInfo InvokeMethodInfo { get; private set; }

		/// <summary>
		/// Gets the type of the invoke method parameter.
		/// </summary>
		/// <value>
		/// The type of the invoke method parameter.
		/// </value>
		public InvokeMethodParameterType InvokeMethodParameterType { get; private set; }

		/// <summary>
		/// Gets the job arguments.
		/// </summary>
		/// <value>
		/// The job arguments.
		/// </value>
		public IJobArgs JobArgs { get; }

		/// <summary>
		/// Starts this job timer.
		/// </summary>
		/// <exception cref="SchedulerInitializationException"></exception>
		public virtual void Start()
		{
		}

		/// <summary>
		/// Stops and disposes job timer.
		/// </summary>
		public virtual void Stop()
		{
		}

		private void BuildInvokeMethodInfo(string invokeMethodName)
		{
			InvokeMethodInfo = JobClassType.GetMethod(invokeMethodName);

			if (InvokeMethodInfo == null)
				throw new SchedulerInitializationException($"Method {invokeMethodName} not found in class {JobClassType.Name}");

			var methodParameters = InvokeMethodInfo.GetParameters();

			if (!methodParameters.Any())
			{
				InvokeMethodParameterType = InvokeMethodParameterType.Parameterless;
				return;
			}

			if (methodParameters[0].ParameterType == typeof(string))
				InvokeMethodParameterType = InvokeMethodParameterType.AppName;

			if (methodParameters[0].ParameterType == typeof(IJobArgs))
				InvokeMethodParameterType = InvokeMethodParameterType.Args;
		}
	}
}