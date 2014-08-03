using System;
using System.Collections.Generic;
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
			var config = ConfigurationManager.GetSection("ServiceSettings") as NameValueCollection;

			if (config != null)
			{
				var workingPoints = config["WorkingPoints"];
				var processingInterval = config["ProcessingInterval"];

				if (!string.IsNullOrEmpty(workingPoints))
				{
					WorkingPoints = new List<DateTime>();

					foreach (var item in workingPoints.Replace(" ", "").Split(','))
						WorkingPoints.Add(DateTime.Parse(item));
				}
				else if (!string.IsNullOrEmpty(processingInterval))
					ProcessingInterval = int.Parse(processingInterval);
			}
			else
				ProcessingInterval = 60;
		}

		/// <summary>
		/// Gets the service processing interval (sec).
		/// </summary>
		/// <value>
		/// The service processing interval (sec).
		/// </value>
		public int ProcessingInterval { get; private set; }

		/// <summary>
		/// Gets the service working points.
		/// </summary>
		/// <value>
		/// The service working points.
		/// </value>
		public IList<DateTime> WorkingPoints { get; private set; }
	}
}
