using System.Collections.Specialized;
using System.Configuration;

namespace Simplify.Mail.Settings.Impl
{
	/// <summary>
	/// Represents MailSender settings based on ConfigurationManager
	/// </summary>
	public sealed class ConfigurationManagedBasedMailSenderSettings : MailSenderSettings
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ConfigurationManagedBasedMailSenderSettings"/> class.
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
		public ConfigurationManagedBasedMailSenderSettings(string configSectionName = "MailSenderSettings")
		{
			var configSection = (NameValueCollection)ConfigurationManager.GetSection(configSectionName);

			if (configSection == null)
				throw new MailSenderException("No MailSenderSettings '" + configSectionName + "' section in config file.");

			LoadGeneralSettings(configSection);
			LoadExtraSettings(configSection);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ConfigurationManagedBasedMailSenderSettings"/> class.
		/// </summary>
		/// <param name="smtpServerAddress">The SMTP server address.</param>
		/// <param name="smtpServerPortNumber">The SMTP server port number.</param>
		/// <param name="smtpUserName">Name of the SMTP user.</param>
		/// <param name="smtpUserPassword">The SMTP user password.</param>
		/// <param name="enableSsl">Enables SSL connection.</param>
		/// <param name="antiSpamMessagesPoolOn">Enables anti-spam messages pool.</param>
		/// <param name="antiSpamPoolMessageLifeTime">The anti-spam pool message life time.</param>
		public ConfigurationManagedBasedMailSenderSettings(string smtpServerAddress,
			int smtpServerPortNumber,
			string smtpUserName,
			string smtpUserPassword,
			bool enableSsl = false,
			bool antiSpamMessagesPoolOn = true,
			int antiSpamPoolMessageLifeTime = 125)
			: base(smtpServerAddress,
				smtpServerPortNumber,
				smtpUserName,
				smtpUserPassword,
				enableSsl,
				antiSpamMessagesPoolOn,
				antiSpamPoolMessageLifeTime)
		{
		}

		private void LoadGeneralSettings(NameValueCollection config)
		{
			SmtpServerAddress = config["SmtpServerAddress"];

			if (string.IsNullOrEmpty(SmtpServerAddress))
				throw new MailSenderException("MailSenderSettings SmtpServerAddress is empty or missing from config file.");

			var smtpServerPortNumberString = config["SmtpServerPortNumber"];

			if (!string.IsNullOrEmpty(smtpServerPortNumberString))
				SmtpServerPortNumber = int.Parse(smtpServerPortNumberString);

			SmtpUserName = config["SmtpUserName"];
			SmtpUserPassword = config["SmtpUserPassword"];
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