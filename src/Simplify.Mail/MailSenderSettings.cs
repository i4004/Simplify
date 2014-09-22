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
			SmtpServerPortNumber = 25;
			AntiSpamPoolMessageLifeTime = 125;
			AntiSpamMessagesPoolOn = true;

			var configSection = (NameValueCollection)ConfigurationManager.GetSection(configSectionName);

			if (configSection == null)
				throw new MailSenderException("No MailSenderSettings '" + configSectionName + "' section in config file.");

			LoadGeneralSettings(configSection);
			LoadExtraSettings(configSection);
		}

		/// <summary>
		/// The SMTP server address
		/// </summary>
		public string SmtpServerAddress { get; private set; }

		/// <summary>
		/// The SMTP server port number
		/// </summary>
		public int SmtpServerPortNumber { get; private set; }

		/// <summary>
		/// The mail sender SMTP user name
		/// </summary>
		public string SmtpUserName { get; private set; }

		/// <summary>
		/// The mail sender SMTP user password
		/// </summary>
		public string SmtpUserPassword { get; private set; }

		/// <summary>
		/// Anti-spam pool message life time (min.)
		/// </summary>
		public int AntiSpamPoolMessageLifeTime { get; private set; }

		/// <summary>
		/// Anit-spam messages pool on
		/// </summary>
		public bool AntiSpamMessagesPoolOn { get; private set; }

		/// <summary>
		/// Gets a value indicating whether SSL is enabled for connection.
		/// </summary>
		/// <value>
		/// <c>true</c> if SSL is enabled for connection; otherwise, <c>false</c>.
		/// </value>
		public bool EnableSsl { get; private set; }

		private void LoadGeneralSettings(NameValueCollection config)
		{
			SmtpServerAddress = config["SmtpServerAddress"];

			if (string.IsNullOrEmpty(SmtpServerAddress))
				throw new MailSenderException("MailSenderSettings SmtpServerAddress is empty or missing from config file.");

			var smtpServerPortNumberString = config["SmtpServerPortNumber"];

			if (!string.IsNullOrEmpty(smtpServerPortNumberString))
				SmtpServerPortNumber = int.Parse(smtpServerPortNumberString);

			SmtpUserName = config["SmtpUserName"];

			if (string.IsNullOrEmpty(SmtpUserName))
				throw new MailSenderException("MailSenderSettings SmtpUserName is empty or missing from config file.");

			SmtpUserPassword = config["SmtpUserPassword"];

			if (string.IsNullOrEmpty(SmtpUserPassword))
				throw new MailSenderException("MailSenderSettings SmtpUserPassword is empty or missing from config file.");
		}
		
		private void LoadExtraSettings(NameValueCollection config)
		{
			var antiSpamPoolMessageLifeTimeString = config["AntiSpamPoolMessageLifeTime"];

			if (!string.IsNullOrEmpty(antiSpamPoolMessageLifeTimeString))
				AntiSpamPoolMessageLifeTime = int.Parse(antiSpamPoolMessageLifeTimeString);

			var antiSpamMessagesPoolOnString = config["AntiSpamMessagesPoolOn"];

			if (!string.IsNullOrEmpty(antiSpamMessagesPoolOnString))
				AntiSpamMessagesPoolOn = bool.Parse(antiSpamMessagesPoolOnString);

			var enableSsl = config["EnableSsl"];

			if (!string.IsNullOrEmpty(enableSsl))
				EnableSsl = bool.Parse(enableSsl);
		}
	}
}
