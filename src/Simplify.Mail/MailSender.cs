using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Simplify.Mail
{
	/// <summary>
	/// E-mail sending class
	/// </summary>
	public class MailSender : IMailSender
	{
		private static IMailSender _defaultInstance;

		/// <summary>
		/// Default MailSender instance
		/// </summary>
		public static IMailSender Default
		{
			get
			{
				return _defaultInstance ?? (_defaultInstance = new MailSender());
			}
			set
			{
				if (value == null)
					throw new ArgumentNullException("value");

				_defaultInstance = value;
			}
		}

		private readonly object _locker = new object();

		private SmtpClient _smtpClientInstance;
		private SmtpClient _fullSmtpClientInstance;

		private readonly Dictionary<string, DateTime> _antiSpamPool = new Dictionary<string, DateTime>();

		/// <summary>
		/// Initializes a new instance of the <see cref="MailSender"/> class.
		/// </summary>
		/// <param name="configurationSectionName">Name of the configuration section in the configuration file.</param>
		public MailSender(string configurationSectionName = "MailSenderSettings")
		{
			Settings = new MailSenderSettings(configurationSectionName);
		}

		/// <summary>
		/// MailSender settings
		/// </summary>
		public MailSenderSettings Settings { get; private set; }

		/// <summary>
		/// Get SMTP client with server parameters from config file and current user credentials
		/// </summary>
		/// <returns></returns>
		public SmtpClient SmtpClientCurrentUser
		{
			get
			{
				return _smtpClientInstance ??
				       (_smtpClientInstance = new SmtpClient(Settings.SmtpServerAddress, Settings.SmtpServerPortNumber)
				       {
					       EnableSsl = Settings.EnableSsl
				       });
			}
		}

		/// <summary>
		/// Get SMTP client with credentials and server parameters from config file
		/// </summary>
		/// <returns></returns>
		public SmtpClient SmtpClient
		{
			get
			{
				lock (_locker)
				{
					if (_fullSmtpClientInstance == null)
					{
						var basicCredential = new NetworkCredential(Settings.SmtpUserName, Settings.SmtpUserPassword);

						_fullSmtpClientInstance = new SmtpClient(Settings.SmtpServerAddress, Settings.SmtpServerPortNumber)
						{
							UseDefaultCredentials = false,
							Credentials = basicCredential,
							EnableSsl = Settings.EnableSsl
						};
					}
				}

				return _fullSmtpClientInstance;
			}
		}

		/// <summary>
		/// Send single e-mail.
		/// </summary>
		/// <param name="client">Smtp client.</param>
		/// <param name="mailMessage">The mail message.</param>
		/// <param name="bodyForAntiSpam">Part of an e-mail body just for anti-spam checking</param>
		public void Send(SmtpClient client, MailMessage mailMessage, string bodyForAntiSpam = null)
		{
			lock (_locker)
			{
				if (CheckAntiSpamPool(bodyForAntiSpam ?? mailMessage.Body))
					return;

				client.Send(mailMessage);
			}
		}

		/// <summary>
		/// Send single e-mail
		/// </summary>
		/// <param name="mailMessage">The mail message.</param>
		/// <param name="bodyForAntiSpam">Part of an e-mail body just for anti-spam checking</param>
		public void Send(MailMessage mailMessage, string bodyForAntiSpam = null)
		{
			Send(SmtpClient, mailMessage, bodyForAntiSpam);
		}

		/// <summary>
		/// Send single e-mail
		/// </summary>
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
		public void Send(SmtpClient client, string from, string to, string subject, string body, string bodyForAntiSpam = null, params Attachment[] attachments)
		{
			lock (_locker)
			{
				if (CheckAntiSpamPool(bodyForAntiSpam ?? body))
					return;

				var mm = new MailMessage(from, to, subject, body)
					{
						BodyEncoding = Encoding.UTF8,
						IsBodyHtml = true,
						DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure,
					};

				if (attachments != null)
					foreach (var attachment in attachments)
						mm.Attachments.Add(attachment);

				client.Send(mm);
			}
		}

		/// <summary>
		/// Send single e-mail
		/// </summary>
		/// <param name="from">From mail address</param>
		/// <param name="to">Recipient e-mail address</param>
		/// <param name="subject">e-mail subject</param>
		/// <param name="body">e-mail body</param>
		/// <param name="bodyForAntiSpam">Part of an e-mail body just for anti-spam checking</param>
		/// <param name="attachments">The attachments to an e-mail.</param>
		/// <returns>Process status, <see langword="true"/> if message is processed to sent successfully</returns>
		public void Send(string from, string to, string subject, string body, string bodyForAntiSpam = null, params Attachment[] attachments)
		{
			Send(SmtpClient, from, to, subject, body, bodyForAntiSpam, attachments);
		}

		/// <summary>
		/// Send e-mail to multiple recipients separately
		/// </summary>
		/// <param name="client">Smtp client</param>
		/// <param name="fromMailAddress">From mail address</param>
		/// <param name="addresses">Recipients</param>
		/// <param name="subject">e-mail subject</param>
		/// <param name="body">e-mail body</param>
		/// <param name="bodyForAntiSpam">Part of an e-mail body just for anti-spam checking</param>
		/// <param name="attachments">The attachments to an e-mail.</param>
		/// <returns>Process status, <see langword="true"/> if all messages are processed to sent successfully</returns>
		public void SendSeparately(SmtpClient client, string fromMailAddress, IList<string> addresses, string subject, string body, string bodyForAntiSpam = null, params Attachment[] attachments)
		{
			if (addresses.Count == 0)
				return;

			lock (_locker)
			{
				if (CheckAntiSpamPool(bodyForAntiSpam ?? body))
					return;

				foreach (var item in addresses)
				{
					var mm = new MailMessage(fromMailAddress, item, subject, body)
						{
							BodyEncoding = Encoding.UTF8,
							IsBodyHtml = true,
							DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure
						};

					if (attachments != null)
						foreach (var attachment in attachments)
							mm.Attachments.Add(attachment);

					client.Send(mm);
				}
			}
		}

		/// <summary>
		/// Send e-mail to multiple recipients separately
		/// </summary>
		/// <param name="fromMailAddress">From mail address</param>
		/// <param name="addresses">Recipients</param>
		/// <param name="subject">e-mail subject</param>
		/// <param name="body">e-mail body</param>
		/// <param name="bodyForAntiSpam">Part of an e-mail body just for anti-spam checking</param>
		/// <param name="attachments">The attachments to an e-mail.</param>
		/// <returns>Process status, <see langword="true"/> if all messages are processed to sent successfully</returns>
		public void SendSeparately(string fromMailAddress, IList<string> addresses, string subject, string body, string bodyForAntiSpam = null, params Attachment[] attachments)
		{
			SendSeparately(SmtpClient, fromMailAddress, addresses, subject, body, bodyForAntiSpam, attachments);
		}

		/// <summary>
		/// Send e-mail to multiple recipients in one e-mail
		/// </summary>
		/// <param name="client">Smtp client</param>
		/// <param name="fromMailAddress">From mail address</param>
		/// <param name="addresses">Recipients</param>
		/// <param name="subject">e-mail subject</param>
		/// <param name="body">e-mail body</param>
		/// <param name="bodyForAntiSpam">Part of an e-mail body just for anti-spam checking</param>
		/// <param name="attachments">The attachments to an e-mail.</param>
		/// <returns>Process status, <see langword="true"/> if all messages are processed to sent successfully</returns>
		public void Send(SmtpClient client, string fromMailAddress, IList<string> addresses, string subject, string body, string bodyForAntiSpam = null, params Attachment[] attachments)
		{
			if (addresses.Count == 0)
				return;

			lock (_locker)
			{
				if (CheckAntiSpamPool(bodyForAntiSpam ?? body))
					return;


				var mm = new MailMessage
				{
					From = new MailAddress(fromMailAddress),
					Subject = subject,
					BodyEncoding = Encoding.UTF8,
					IsBodyHtml = true,
					Body = body,
					DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure
				};

				foreach (var item in addresses)
					mm.To.Add(item);

				if (attachments != null)
					foreach (var attachment in attachments)
						mm.Attachments.Add(attachment);

				client.Send(mm);

			}
		}

		/// <summary>
		/// Send e-mail to multiple recipients in one e-mail
		/// </summary>
		/// <param name="fromMailAddress">From mail address</param>
		/// <param name="addresses">Recipients</param>
		/// <param name="subject">e-mail subject</param>
		/// <param name="body">e-mail body</param>
		/// <param name="bodyForAntiSpam">Part of an e-mail body just for anti-spam checking</param>
		/// <param name="attachments">The attachments to an e-mail.</param>
		/// <returns>Process status, <see langword="true"/> if all messages are processed to sent successfully</returns>
		public void Send(string fromMailAddress, IList<string> addresses, string subject, string body, string bodyForAntiSpam = null, params Attachment[] attachments)
		{
			Send(SmtpClient, fromMailAddress, addresses, subject, body, bodyForAntiSpam, attachments);
		}

		/// <summary>
		/// Send e-mail to multiple recipients and carbon copy recipients in one e-mail
		/// </summary>
		/// <param name="client">Smtp client</param>
		/// <param name="fromMailAddress">From mail address</param>
		/// <param name="addresses">Recipients</param>
		/// <param name="ccAddresses">Carbon copy recipients</param>
		/// <param name="subject">e-mail subject</param>
		/// <param name="body">e-mail body</param>
		/// <param name="bodyForAntiSpam">Part of an e-mail body just for anti-spam checking</param>
		/// <param name="attachments">The attachments to an e-mail.</param>
		/// <returns>Process status, <see langword="true"/> if all messages are processed to sent successfully</returns>
		public void Send(SmtpClient client, string fromMailAddress, IList<string> addresses, IList<string> ccAddresses, string subject, string body, string bodyForAntiSpam = null, params Attachment[] attachments)
		{
			if (addresses.Count == 0)
				return;

			lock (_locker)
			{
				if (CheckAntiSpamPool(bodyForAntiSpam ?? body))
					return;

				var mm = new MailMessage
				{
					From = new MailAddress(fromMailAddress),
					Subject = subject,
					BodyEncoding = Encoding.UTF8,
					IsBodyHtml = true,
					Body = body,
					DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure
				};

				foreach (var item in addresses)
					mm.To.Add(item);

				foreach (var item in ccAddresses)
					mm.CC.Add(item);

				if (attachments != null)
					foreach (var attachment in attachments)
						mm.Attachments.Add(attachment);

				client.Send(mm);
			}
		}

		/// <summary>
		/// Send e-mail to multiple recipients and carbon copy recipients in one e-mail
		/// </summary>
		/// <param name="fromMailAddress">From mail address</param>
		/// <param name="addresses">Recipients</param>
		/// <param name="ccAddresses">Carbon copy recipients</param>
		/// <param name="subject">e-mail subject</param>
		/// <param name="body">e-mail body</param>
		/// <param name="bodyForAntiSpam">Part of an e-mail body just for anti-spam checking</param>
		/// <param name="attachments">The attachments to an e-mail.</param>
		/// <returns>Process status, <see langword="true"/> if all messages are processed to sent successfully</returns>
		public void Send(string fromMailAddress, IList<string> addresses, IList<string> ccAddresses, string subject,
			string body, string bodyForAntiSpam = null, params Attachment[] attachments)
		{
			Send(SmtpClient, fromMailAddress, addresses, ccAddresses, subject, body, bodyForAntiSpam, attachments);
		}

		private bool CheckAntiSpamPool(string messageBody)
		{
			if (!Settings.AntiSpamMessagesPoolOn || string.IsNullOrEmpty(messageBody))
				return false;

			var itemsToRemove = (from item in _antiSpamPool
								 where (DateTime.Now - item.Value).TotalMinutes > Settings.AntiSpamPoolMessageLifeTime
								 select item.Key).ToList();

			foreach (var item in itemsToRemove)
				_antiSpamPool.Remove(item);

			if (_antiSpamPool.ContainsKey(messageBody))
				return true;

			_antiSpamPool.Add(messageBody, DateTime.Now);

			return false;
		}
	}
}
