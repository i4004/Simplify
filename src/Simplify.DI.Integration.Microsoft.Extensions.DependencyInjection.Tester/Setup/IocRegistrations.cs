namespace Simplify.DI.Integration.Microsoft.Extensions.DependencyInjection.Tester.Setup
{
	public static class IocRegistrations
	{
		public static void Register()
		{
			DIContainer.Current.Register<Dependency>();
		}
	}
}