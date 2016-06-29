using System.ServiceModel;

namespace Simplify.DI.Wcf
{
	internal static class SimplifyInstanceContextExtensions
	{
		public static ILifetimeScope BeginScope(this InstanceContext instanceContext)
		{
			var extension = instanceContext.Extensions.Find<SimplifyInstanceContextExtension>();

			if (extension == null)
				instanceContext.Extensions.Add(extension = new SimplifyInstanceContextExtension());

			return extension.Scope ?? (extension.Scope = DIContainer.Current.BeginLifetimeScope());
		}

		public static ILifetimeScope GetScope(this InstanceContext instanceContext)
		{
			return instanceContext?.Extensions.Find<SimplifyInstanceContextExtension>()?.Scope;
		}

		private sealed class SimplifyInstanceContextExtension : IExtension<InstanceContext>
		{
			public ILifetimeScope Scope { get; set; }

			public void Attach(InstanceContext owner)
			{
			}

			public void Detach(InstanceContext owner)
			{
			}
		}
	}
}