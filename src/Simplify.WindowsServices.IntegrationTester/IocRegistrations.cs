using Simplify.DI;

namespace Simplify.WindowsServices.IntegrationTester
{
	public static class IocRegistrations
	{
		public static void Register()
		{
			DIContainer.Current.Register<Dependency1>();

			DIContainer.Current.Register<TaskProcessor1>();
			DIContainer.Current.Register<BasicTaskProcessor>();
		}
	}
}