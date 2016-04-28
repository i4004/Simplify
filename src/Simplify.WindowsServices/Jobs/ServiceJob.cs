using System;
using System.Linq;
using System.Reflection;

namespace Simplify.WindowsServices.Jobs
{
	/// <summary>
	/// Provides basic service job
	/// </summary>
	/// <seealso cref="IServiceJob" />
	public class ServiceJob<T> : IServiceJob
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ServiceJob{T}"/> class.
		/// </summary>
		/// <param name="invokeMethodName">Name of the invoke method.</param>
		/// <exception cref="ArgumentNullException"></exception>
		/// <exception cref="ServiceInitializationException"></exception>
		public ServiceJob(string invokeMethodName = "Run")
		{
			if (invokeMethodName == null) throw new ArgumentNullException(nameof(invokeMethodName));

			JobClassType = typeof(T);
			InvokeMethodInfo = JobClassType.GetMethod(invokeMethodName);

			if (InvokeMethodInfo == null)
				throw new ServiceInitializationException($"Method {invokeMethodName} not found in class {JobClassType.Name}");

			IsParameterlessMethod = !InvokeMethodInfo.GetParameters().Any();
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
		public MethodInfo InvokeMethodInfo { get; }

		/// <summary>
		/// Gets a value indicating whether invoke method instance is parameterless method.
		/// </summary>
		/// <value>
		/// <c>true</c> if invoke method is parameterless method; otherwise, <c>false</c>.
		/// </value>
		public bool IsParameterlessMethod { get; }

		/// <summary>
		/// Starts this job timer.
		/// </summary>
		/// <exception cref="ServiceInitializationException"></exception>
		public virtual void Start()
		{
		}

		/// <summary>
		/// Stops and disposes job timer.
		/// </summary>
		public virtual void Stop()
		{
		}
	}
}