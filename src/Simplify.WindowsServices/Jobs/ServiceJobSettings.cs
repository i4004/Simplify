using System.Collections.Specialized;
using System.Configuration;

namespace Simplify.WindowsServices.Jobs
{
	/// <summary>
	/// Provides windows-service job settings
	/// </summary>
	public class ServiceJobSettings : IServiceJobSettings
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ServiceJobSettings"/> class.
		/// </summary>
		public ServiceJobSettings(string configSectionName = "ServiceSettings")
		{
			ProcessingInterval = 60;

			var config = ConfigurationManager.GetSection(configSectionName) as NameValueCollection;

			if (config == null) return;

			CrontabExpression = config["CrontabExpression"];

			if (!string.IsNullOrEmpty(CrontabExpression)) return;

			var processingInterval = config["ProcessingInterval"];

			if(!string.IsNullOrEmpty(processingInterval))
				ProcessingInterval = int.Parse(processingInterval);
		}

		/// <summary>
		/// Gets the crontab expression.
		/// </summary>
		/// <value>
		/// The crontab expression.
		/// </value>
		public string CrontabExpression { get; private set; }

		/// <summary>
		/// Gets the service processing interval (sec).
		/// </summary>
		/// <value>
		/// The service processing interval (sec).
		/// </value>
		public int ProcessingInterval { get; private set; }
	}
}
