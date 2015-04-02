using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace Simplify.DI.Wcf
{
	/// <summary>
	/// Provides Simplify.DI WCF service behaviour
	/// </summary>
	public class SimplifyServiceBehaviour : IServiceBehavior
	{
		/// <summary>
		/// Provides the ability to inspect the service host and the service description to confirm that the service can run successfully.
		/// </summary>
		/// <param name="serviceDescription">The service description.</param>
		/// <param name="serviceHostBase">The service host that is currently being constructed.</param>
		public void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
		{
		}

		/// <summary>
		/// Provides the ability to pass custom data to binding elements to support the contract implementation.
		/// </summary>
		/// <param name="serviceDescription">The service description of the service.</param>
		/// <param name="serviceHostBase">The host of the service.</param>
		/// <param name="endpoints">The service endpoints.</param>
		/// <param name="bindingParameters">Custom objects to which binding elements have access.</param>
		public void AddBindingParameters(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase, Collection<ServiceEndpoint> endpoints,
			BindingParameterCollection bindingParameters)
		{
		}

		/// <summary>
		/// Provides the ability to change run-time property values or insert custom extension objects such as error handlers, message or parameter interceptors, security extensions, and other custom extension objects.
		/// </summary>
		/// <param name="serviceDescription">The service description.</param>
		/// <param name="serviceHostBase">The host that is currently being built.</param>
		public void ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
		{
			var endpointDispatchers = GetEndpointDispatchersForImplementedContracts(serviceDescription, serviceHostBase);

			foreach (var endpointDispatcher in endpointDispatchers)
				endpointDispatcher.DispatchRuntime.InstanceProvider = new SimplifyInstanceProvider(serviceDescription.ServiceType);
		}

		private static IEnumerable<EndpointDispatcher> GetEndpointDispatchersForImplementedContracts(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
		{
			var implementedContracts = (
				from serviceEndpoint in serviceDescription.Endpoints
				where serviceEndpoint.Contract.ContractType.IsAssignableFrom(serviceDescription.ServiceType)
				select serviceEndpoint.Contract.Name)
				.ToList();

			return
				from channelDispatcher in serviceHostBase.ChannelDispatchers.OfType<ChannelDispatcher>()
				from endpointDispatcher in channelDispatcher.Endpoints
				where implementedContracts.Contains(endpointDispatcher.ContractName)
				select endpointDispatcher;
		}
	}
}