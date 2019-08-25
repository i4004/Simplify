using Simplify.DI;
using Simplify.Mail.TestConsoleApp.Setup;

namespace Simplify.Mail.TestConsoleApp
{
	internal class Program
	{
		private static void Main()
		{
			IocRegistrations.Register();
			DIContainer.Current.Verify();

			using (var scope = DIContainer.Current.BeginLifetimeScope())
			{
				var sender = scope.Resolver.Resolve<IMailSender>();

				sender.Send("somesender@somedomain.com", "somereceiver@somedomain.com", "Hello subject",
					"Hello World!!!");
			}
		}
	}
}