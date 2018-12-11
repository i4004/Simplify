namespace Simplify.Mail.Settings
{
	/// <summary>
	/// Represents MailSender settings group
	/// </summary>
	public class MailSenderSettings : IMailSenderSettings
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="MailSenderSettings"/> class.
		/// </summary>
		/// <param name="smtpServerAddress">The SMTP server address.</param>
		/// <param name="smtpServerPortNumber">The SMTP server port number.</param>
		/// <param name="smtpUserName">Name of the SMTP user.</param>
		/// <param name="smtpUserPassword">The SMTP user password.</param>
		/// <param name="enableSsl">Enables SSL connection.</param>
		/// <param name="antiSpamMessagesPoolOn">Enables anti-spam messages pool.</param>
		/// <param name="antiSpamPoolMessageLifeTime">The anti-spam pool message life time.</param>
		public MailSenderSettings(string smtpServerAddress, int smtpServerPortNumber,
			string smtpUserName, string smtpUserPassword,
			bool enableSsl = false, bool antiSpamMessagesPoolOn = true, int antiSpamPoolMessageLifeTime = 125)
		{
			SmtpServerAddress = smtpServerAddress;
			SmtpServerPortNumber = smtpServerPortNumber;
			SmtpUserName = smtpUserName;
			SmtpUserPassword = smtpUserPassword;
			EnableSsl = enableSsl;
			AntiSpamMessagesPoolOn = antiSpamMessagesPoolOn;
			AntiSpamPoolMessageLifeTime = antiSpamPoolMessageLifeTime;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="MailSenderSettings"/> class.
		/// </summary>
		protected MailSenderSettings()
		{
		}

		/// <summary>
		/// The SMTP server address
		/// </summary>
		public string SmtpServerAddress { get; protected set; }

		/// <summary>
		/// The SMTP server port number
		/// </summary>
		public int SmtpServerPortNumber { get; protected set; } = 25;

		/// <summary>
		/// The mail sender SMTP user name
		/// </summary>
		public string SmtpUserName { get; protected set; }

		/// <summary>
		/// The mail sender SMTP user password
		/// </summary>
		public string SmtpUserPassword { get; protected set; }

		/// <summary>
		/// Anti-spam pool message life time (min.)
		/// </summary>
		public int AntiSpamPoolMessageLifeTime { get; protected set; } = 125;

		/// <summary>
		/// Anti-spam messages pool on
		/// </summary>
		public bool AntiSpamMessagesPoolOn { get; protected set; } = true;

		/// <summary>
		/// Gets a value indicating whether SSL is enabled for connection.
		/// </summary>
		/// <value>
		/// <c>true</c> if SSL is enabled for connection; otherwise, <c>false</c>.
		/// </value>
		public bool EnableSsl { get; protected set; }
	}
}