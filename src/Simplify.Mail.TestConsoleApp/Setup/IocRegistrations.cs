using Microsoft.Extensions.Configuration;
using Simplify.DI;

namespace Simplify.Mail.TestConsoleApp.Setup
{
	public static class IocRegistrations
	{
		public static void Register()
		{
			DIContainer.Current.Register<IConfiguration>(p => new ConfigurationBuilder()
				.AddJsonFile("appsettings.json", true, true)
				.Build());
		}
	}
}