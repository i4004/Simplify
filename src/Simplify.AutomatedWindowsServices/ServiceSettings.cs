using System.Collections.Specialized;
using System.Configuration;

namespace Simplify.AutomatedWindowsServices
{
	/// <summary>
	/// Provides windows-service processing settings
	/// </summary>
	public class ServiceSettings : IServiceSettings
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ServiceSettings"/> class.
		/// </summary>
		public ServiceSettings()
		{
			ProcessingInterval = 60;

			var config = ConfigurationManager.GetSection("ServiceSettings") as NameValueCollection;

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
