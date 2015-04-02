using System;
using System.ServiceModel;
using System.ServiceModel.Activation;

namespace Simplify.DI.Wcf
{
	public class SimplifyServiceHostFactory : ServiceHostFactory
	{
		protected override ServiceHost CreateServiceHost(Type serviceType, Uri[] baseAddresses)
		{
			return new SimplifyServiceHost(serviceType, baseAddresses);
		}
	}
}