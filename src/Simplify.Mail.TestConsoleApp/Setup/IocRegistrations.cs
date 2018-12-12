using Microsoft.Extensions.Configuration;
using Simplify.DI;

namespace Simplify.Mail.TestConsoleApp.Setup
{
	public static class IocRegistrations
	{
		public static void Register()
		{
			DIContainer.Current.Register<IConfiguration>(r => new ConfigurationBuilder()
				.AddJsonFile("appsettings.json", false)
				.Build());

			DIContainer.Current.Register<IMailSender>(r => new MailSender(r.Resolve<IConfiguration>()));
		}
	}
}