using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace Simplify.DI.Wcf
{
	public class SimplifyServiceBehaviour : IServiceBehavior
	{
		public void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
		{
		}

		public void AddBindingParameters(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase, Collection<ServiceEndpoint> endpoints,
			BindingParameterCollection bindingParameters)
		{
		}

		public void ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
		{
			var instanceProvider = new SimplifyInstanceProvider(serviceDescription.ServiceType);
			var endpointDispatchers = GetEndpointDispatchersForImplementedContracts(serviceDescription, serviceHostBase);

			foreach (var endpointDispatcher in endpointDispatchers)
				endpointDispatcher.DispatchRuntime.InstanceProvider = instanceProvider;
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