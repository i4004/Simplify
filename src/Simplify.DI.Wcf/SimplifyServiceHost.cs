using System;
using System.ServiceModel;

namespace Simplify.DI.Wcf
{
	public class SimplifyServiceHost : ServiceHost
	{
		public SimplifyServiceHost()
		{
		}

		public SimplifyServiceHost(Type serviceType, params Uri[] baseAddresses)
			: base(serviceType, baseAddresses)
		{
		}

		protected override void OnOpening()
		{
			Description.Behaviors.Add(new SimplifyServiceBehaviour());

			base.OnOpening();
		}
	}
}