using NUnit.Framework;

namespace Simplify.Mail.IntegrationTests
{
	// For test SMTP server you can use https://github.com/rnwood/smtp4dev
	[TestFixture]
	[Category("Integration")]
	public class MailSenderTests
	{
		[Test]
		public void SendSimpleTestEmail()
		{
			MailSender.Default.Send("somesender@somedomain.com", "somereceiver@somedomain.com", "Hello subject", "Hello World!!!");
		}
	}
}