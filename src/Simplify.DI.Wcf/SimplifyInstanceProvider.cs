using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;

namespace Simplify.DI.Wcf
{
	public class SimplifyInstanceProvider : IInstanceProvider
	{
		private readonly Type _serviceType;
		private ILifetimeScope _currentScope;

		public SimplifyInstanceProvider(Type serviceType)
		{
			_serviceType = serviceType;
		}

		public object GetInstance(InstanceContext instanceContext)
		{
			_currentScope = DIContainer.Current.BeginLifetimeScope();

			try
			{
				return _currentScope.Container.Resolve(_serviceType);
			}
			catch
			{
				_currentScope.Dispose();

				throw;
			}
		}

		public object GetInstance(InstanceContext instanceContext, Message message)
		{
			return GetInstance(instanceContext);
		}

		public void ReleaseInstance(InstanceContext instanceContext, object instance)
		{
			if (_currentScope != null)
				_currentScope.Dispose();
		}
	}
}