using Microsoft.Extensions.Configuration;
using Simplify.DI;

namespace Simplify.WindowsServices.MultitaskExample.Setup
{
	public static class IocRegistrations
	{
		public static void Register()
		{
			DIContainer.Current.Register<IConfiguration>(r => new ConfigurationBuilder()
				.AddJsonFile("appsettings.json", false)
				.Build());
		}
	}
}