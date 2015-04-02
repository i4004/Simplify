using System;
using System.ServiceModel;

namespace Simplify.DI.Wcf
{
	/// <summary>
	/// Provides Simplify.DI WCF service host
	/// </summary>
	public class SimplifyServiceHost : ServiceHost
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="SimplifyServiceHost"/> class.
		/// </summary>
		public SimplifyServiceHost()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="SimplifyServiceHost"/> class.
		/// </summary>
		/// <param name="serviceType">The type of hosted service.</param>
		/// <param name="baseAddresses">An array of type <see cref="T:System.Uri" /> that contains the base addresses for the hosted service.</param>
		public SimplifyServiceHost(Type serviceType, params Uri[] baseAddresses)
			: base(serviceType, baseAddresses)
		{
		}

		/// <summary>
		/// Invoked during the transition of a communication object into the opening state.
		/// </summary>
		protected override void OnOpening()
		{
			Description.Behaviors.Add(new SimplifyServiceBehaviour());

			base.OnOpening();
		}
	}
}