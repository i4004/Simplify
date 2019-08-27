namespace Simplify.DI.AspNetCore.Tester.Setup
{
	public static class IocRegistrations
	{
		public static void Register()
		{
			DIContainer.Current.Register<Dependency>();
		}
	}
}