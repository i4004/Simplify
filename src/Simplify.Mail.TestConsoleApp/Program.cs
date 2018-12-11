using Simplify.Mail.TestConsoleApp.Setup;

namespace Simplify.Mail.TestConsoleApp
{
	internal class Program
	{
		private static void Main()
		{
			IocRegistrations.Register();

			MailSender.Default.Send("somesender@somedomain.com", "somereceiver@somedomain.com", "Hello subject", "Hello World!!!");
		}
	}
}