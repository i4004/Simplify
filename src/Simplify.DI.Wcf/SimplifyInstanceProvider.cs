using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;

namespace Simplify.DI.Wcf
{
	/// <summary>
	/// Provides Simplify.DI WCF instance provider
	/// </summary>
	public class SimplifyInstanceProvider : IInstanceProvider
	{
		private readonly Type _serviceType;

		/// <summary>
		/// Initializes a new instance of the <see cref="SimplifyInstanceProvider"/> class.
		/// </summary>
		/// <param name="serviceType">Type of the service.</param>
		public SimplifyInstanceProvider(Type serviceType)
		{
			_serviceType = serviceType;
		}

		/// <summary>
		/// Returns a service object given the specified <see cref="T:System.ServiceModel.InstanceContext" /> object.
		/// </summary>
		/// <param name="instanceContext">The current <see cref="T:System.ServiceModel.InstanceContext" /> object.</param>
		/// <returns>
		/// A user-defined service object.
		/// </returns>
		public object GetInstance(InstanceContext instanceContext)
		{
			var scope = instanceContext.BeginScope();

			try
			{
				return scope.Container.Resolve(_serviceType);
			}
			catch
			{
				scope.Dispose();

				throw;
			}
		}

		/// <summary>
		/// Returns a service object given the specified <see cref="T:System.ServiceModel.InstanceContext" /> object.
		/// </summary>
		/// <param name="instanceContext">The current <see cref="T:System.ServiceModel.InstanceContext" /> object.</param>
		/// <param name="message">The message that triggered the creation of a service object.</param>
		/// <returns>
		/// The service object.
		/// </returns>
		public object GetInstance(InstanceContext instanceContext, Message message)
		{
			return GetInstance(instanceContext);
		}

		/// <summary>
		/// Called when an <see cref="T:System.ServiceModel.InstanceContext" /> object recycles a service object.
		/// </summary>
		/// <param name="instanceContext">The service's instance context.</param>
		/// <param name="instance">The service object to be recycled.</param>
		public void ReleaseInstance(InstanceContext instanceContext, object instance)
		{
			instanceContext.GetScope()?.Dispose();
		}
	}
}