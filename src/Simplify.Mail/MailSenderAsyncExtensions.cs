using System.Collections.Generic;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Simplify.Mail
{
	/// <summary>
	/// Provides MailSender asynchronous operations extensions
	/// </summary>
	public static class MailSenderAsyncExtensions
	{
		/// <summary>
		/// Send single e-mail
		/// </summary>
		/// <param name="mailSender">The mail sender.</param>
		/// <param name="client">Smtp client</param>
		/// <param name="from">From mail address</param>
		/// <param name="to">Recipient e-mail address</param>
		/// <param name="subject">e-mail subject</param>
		/// <param name="body">e-mail body</param>
		/// <param name="bodyForAntiSpam">Part of an e-mail body just for anti-spam checking</param>
		/// <param name="attachments">The attachments to an e-mail.</param>
		/// <returns>
		/// Process status, <see langword="true" /> if message is processed to sent successfully
		/// </returns>
		public static Task SendAsync(this IMailSender mailSender, SmtpClient client, string from, string to, string subject, string body, string bodyForAntiSpam = null, params Attachment[] attachments)
		{
			return Task.Run(() => mailSender.Send(client, from, to, subject, body, bodyForAntiSpam, attachments));
		}

		/// <summary>
		/// Send single e-mail using config SMTP user name and password
		/// </summary>
		/// <param name="mailSender">The mail sender.</param>
		/// <param name="from">From mail address</param>
		/// <param name="to">Recipient e-mail address</param>
		/// <param name="subject">e-mail subject</param>
		/// <param name="body">e-mail body</param>
		/// <param name="bodyForAntiSpam">Part of an e-mail body just for anti-spam checking</param>
		/// <param name="attachments">The attachments to an e-mail.</param>
		public static Task SendAsync(this IMailSender mailSender, string from, string to, string subject, string body, string bodyForAntiSpam = null, params Attachment[] attachments)
		{
			return SendAsync(mailSender, mailSender.SmtpClient, from, to, subject, body, bodyForAntiSpam, attachments);
		}

		/// <summary>
		/// Send e-mail to multiple recipients separately
		/// </summary>
		/// <param name="mailSender">The mail sender.</param>
		/// <param name="client">Smtp client</param>
		/// <param name="fromMailAddress">From mail address</param>
		/// <param name="addresses">Recipients</param>
		/// <param name="subject">e-mail subject</param>
		/// <param name="body">e-mail body</param>
		/// <param name="bodyForAntiSpam">Part of an e-mail body just for anti-spam checking</param>
		/// <param name="attachments">The attachments to an e-mail.</param>
		/// <returns>
		/// Process status, <see langword="true" /> if all messages are processed to sent successfully
		/// </returns>
		public static Task SendSeparatelyAsync(this IMailSender mailSender, SmtpClient client, string fromMailAddress, IList<string> addresses, string subject, string body, string bodyForAntiSpam = null, params Attachment[] attachments)
		{
			return Task.Run(() => mailSender.SendSeparately(client, fromMailAddress, addresses, subject, body, bodyForAntiSpam, attachments));
		}

		/// <summary>
		/// Send e-mail to multiple recipients separately
		/// </summary>
		/// <param name="mailSender">The mail sender.</param>
		/// <param name="fromMailAddress">From mail address</param>
		/// <param name="addresses">Recipients</param>
		/// <param name="subject">e-mail subject</param>
		/// <param name="body">e-mail body</param>
		/// <param name="bodyForAntiSpam">Part of an e-mail body just for anti-spam checking</param>
		/// <param name="attachments">The attachments to an e-mail.</param>
		/// <returns>
		/// Process status, <see langword="true" /> if all messages are processed to sent successfully
		/// </returns>
		public static Task SendSeparatelyAsync(this IMailSender mailSender, string fromMailAddress, IList<string> addresses, string subject, string body, string bodyForAntiSpam = null, params Attachment[] attachments)
		{
			return SendSeparatelyAsync(mailSender, mailSender.SmtpClient, fromMailAddress, addresses, subject, body, bodyForAntiSpam, attachments);
		}

		/// <summary>
		/// Send e-mail to multiple recipients in one e-mail
		/// </summary>
		/// <param name="mailSender">The mail sender.</param>
		/// <param name="client">Smtp client</param>
		/// <param name="fromMailAddress">From mail address</param>
		/// <param name="addresses">Recipients</param>
		/// <param name="subject">e-mail subject</param>
		/// <param name="body">e-mail body</param>
		/// <param name="bodyForAntiSpam">Part of an e-mail body just for anti-spam checking</param>
		/// <param name="attachments">The attachments to an e-mail.</param>
		/// <returns>
		/// Process status, <see langword="true" /> if all messages are processed to sent successfully
		/// </returns>
		public static Task SendAsync(this IMailSender mailSender, SmtpClient client, string fromMailAddress, IList<string> addresses, string subject, string body, string bodyForAntiSpam = null, params Attachment[] attachments)
		{
			return Task.Run(() => mailSender.Send(client, fromMailAddress, addresses, subject, body, bodyForAntiSpam, attachments));
		}

		/// <summary>
		/// Send e-mail to multiple recipients in one e-mail
		/// </summary>
		/// <param name="mailSender">The mail sender.</param>
		/// <param name="fromMailAddress">From mail address</param>
		/// <param name="addresses">Recipients</param>
		/// <param name="subject">e-mail subject</param>
		/// <param name="body">e-mail body</param>
		/// <param name="bodyForAntiSpam">Part of an e-mail body just for anti-spam checking</param>
		/// <param name="attachments">The attachments to an e-mail.</param>
		/// <returns>
		/// Process status, <see langword="true" /> if all messages are processed to sent successfully
		/// </returns>
		public static Task SendAsync(this IMailSender mailSender, string fromMailAddress, IList<string> addresses, string subject, string body, string bodyForAntiSpam = null, params Attachment[] attachments)
		{
			return SendAsync(mailSender, mailSender.SmtpClient, fromMailAddress, addresses, subject, body, bodyForAntiSpam, attachments);
		}

		/// <summary>
		/// Send e-mail to multiple recipients and carbon copy recipients in one e-mail
		/// </summary>
		/// <param name="mailSender">The mail sender.</param>
		/// <param name="client">Smtp client</param>
		/// <param name="fromMailAddress">From mail address</param>
		/// <param name="addresses">Recipients</param>
		/// <param name="ccAddresses">Carbon copy recipients</param>
		/// <param name="subject">e-mail subject</param>
		/// <param name="body">e-mail body</param>
		/// <param name="bodyForAntiSpam">Part of an e-mail body just for anti-spam checking</param>
		/// <param name="attachments">The attachments to an e-mail.</param>
		/// <returns>
		/// Process status, <see langword="true" /> if all messages are processed to sent successfully
		/// </returns>
		public static Task SendAsync(this IMailSender mailSender, SmtpClient client, string fromMailAddress, IList<string> addresses, IList<string> ccAddresses, string subject, string body, string bodyForAntiSpam = null, params Attachment[] attachments)
		{
			return Task.Run(() => mailSender.Send(client, fromMailAddress, addresses, ccAddresses, subject, body, bodyForAntiSpam, attachments));
		}

		/// <summary>
		/// Send e-mail to multiple recipients and carbon copy recipients in one e-mail
		/// </summary>
		/// <param name="mailSender">The mail sender.</param>
		/// <param name="fromMailAddress">From mail address</param>
		/// <param name="addresses">Recipients</param>
		/// <param name="ccAddresses">Carbon copy recipients</param>
		/// <param name="subject">e-mail subject</param>
		/// <param name="body">e-mail body</param>
		/// <param name="bodyForAntiSpam">Part of an e-mail body just for anti-spam checking</param>
		/// <param name="attachments">The attachments to an e-mail.</param>
		/// <returns>
		/// Process status, <see langword="true" /> if all messages are processed to sent successfully
		/// </returns>
		public static Task Send(this IMailSender mailSender, string fromMailAddress, IList<string> addresses, IList<string> ccAddresses, string subject,
			string body, string bodyForAntiSpam = null, params Attachment[] attachments)
		{
			return SendAsync(mailSender, mailSender.SmtpClient, fromMailAddress, addresses, ccAddresses, subject, body, bodyForAntiSpam, attachments);
		} 
	}
}