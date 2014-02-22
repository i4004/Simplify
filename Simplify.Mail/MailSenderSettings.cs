using System.Collections.Specialized;
using System.Configuration;

namespace Simplify.Mail
{
	/// <summary>
	/// Represents MailSender settings
	/// </summary>
	public sealed class MailSenderSettings
	{
		/// <summary>
		/// The SMTP server address
		/// </summary>
		public readonly string SmtpServerAddress;
		/// <summary>
		/// The SMTP server port number
		/// </summary>
		public readonly int SmtpServerPortNumber = 25;

		/// <summary>
		/// The mail sender SMTP user name
		/// </summary>
		public readonly string SmtpUserName;

		/// <summary>
		/// The mail sender SMTP user password
		/// </summary>
		public readonly string SmtpUserPassword;

		/// <summary>
		/// Anti-spam pool message life time (min.)
		/// </summary>
		public readonly int AntiSpamPoolMessageLifeTime = 120;

		/// <summary>
		/// Anit-spam messages pool on
		/// </summary>
		public readonly bool AntiSpamMessagesPoolOn = true;

		/// <summary>
		/// Initializes a new instance of the <see cref="MailSenderSettings"/> class.
		/// </summary>
		/// <param name="configSectionName">Name of the configuration section in the configuration file.</param>
		/// <exception cref="MailSenderException">
		/// No MailSenderSettings section in config file.
		/// or
		/// MailSenderSettings SmtpServerAddress is empty or missing from config file.
		/// or
		/// MailSenderSettings SmtpUserName is empty or missing from config file.
		/// or
		/// MailSenderSettings SmtpUserPassword is empty or missing from config file.
		/// </exception>
		public MailSenderSettings(string configSectionName = "MailSenderSettings")
		{
			var configSection = (NameValueCollection)ConfigurationManager.GetSection(configSectionName);

			if (configSection == null)
				throw new MailSenderException("No MailSenderSettings '" +  configSectionName + "' section in config file.");

			SmtpServerAddress = configSection["SmtpServerAddress"];

			if (string.IsNullOrEmpty(SmtpServerAddress))
				throw new MailSenderException("MailSenderSettings SmtpServerAddress is empty or missing from config file.");

			var smtpServerPortNumberString = configSection["SmtpServerPortNumber"];

			if (!string.IsNullOrEmpty(smtpServerPortNumberString))
				SmtpServerPortNumber = int.Parse(smtpServerPortNumberString);

			SmtpUserName = configSection["SmtpUserName"];

			if (string.IsNullOrEmpty(SmtpUserName))
				throw new MailSenderException("MailSenderSettings SmtpUserName is empty or missing from config file.");
			
			SmtpUserPassword = configSection["SmtpUserPassword"];

			if (string.IsNullOrEmpty(SmtpUserPassword))
				throw new MailSenderException("MailSenderSettings SmtpUserPassword is empty or missing from config file.");

			var antiSpamPoolMessageLifeTimeString = configSection["AntiSpamPoolMessageLifeTime"];

			if (!string.IsNullOrEmpty(antiSpamPoolMessageLifeTimeString))
				AntiSpamPoolMessageLifeTime = int.Parse(antiSpamPoolMessageLifeTimeString);

			var antiSpamMessagesPoolOnString = configSection["AntiSpamMessagesPoolOn"];

			if (!string.IsNullOrEmpty(antiSpamMessagesPoolOnString))
				AntiSpamMessagesPoolOn = bool.Parse(antiSpamMessagesPoolOnString);
		}
	}
}
