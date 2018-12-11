using Microsoft.Extensions.Configuration;

namespace Simplify.Mail.Settings.Impl
{
	/// <summary>
	/// Represents MailSender settings based on IConfiguration
	/// </summary>
	public sealed class ConfigurationBasedMailSenderSettings : MailSenderSettings
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ConfigurationManagedBasedMailSenderSettings" /> class.
		/// </summary>
		/// <param name="configuration">The configuration.</param>
		/// <param name="configSectionName">Name of the configuration section.</param>
		/// <exception cref="MailSenderException">No MailSenderSettings section in config file.
		/// or
		/// MailSenderSettings SmtpServerAddress is empty or missing from config file.
		/// or
		/// MailSenderSettings SmtpUserName is empty or missing from config file.
		/// or
		/// MailSenderSettings SmtpUserPassword is empty or missing from config file.</exception>
		public ConfigurationBasedMailSenderSettings(IConfiguration configuration,
			string configSectionName = "MailSenderSettings")
		{
			var config = configuration.GetSection(configSectionName);

			if (config == null)
				throw new MailSenderException("No MailSenderSettings '" + configSectionName + "' section in config file.");

			LoadGeneralSettings(config);
			LoadExtraSettings(config);
		}

		private void LoadGeneralSettings(IConfiguration config)
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

		private void LoadExtraSettings(IConfiguration config)
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