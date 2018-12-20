using Microsoft.Extensions.Configuration;
using Simplify.WindowsServices.Jobs.Crontab;
using Simplify.WindowsServices.Jobs.Settings.Impl;

namespace Simplify.WindowsServices.Jobs
{
	/// <summary>
	/// Provides service jobs factory
	/// </summary>
	public class ServiceJobFactory : IServiceJobFactory
	{
		/// <summary>
		/// Creates the basic service job.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="invokeMethodName">Name of the invoke method.</param>
		/// <returns></returns>
		public IServiceJob CreateServiceJob<T>(string invokeMethodName = "Run")
		{
			return new ServiceJob<T>(invokeMethodName);
		}

		/// <summary>
		/// Creates the service job.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="configurationSectionName">Name of the configuration section.</param>
		/// <param name="invokeMethodName">Name of the invoke method.</param>
		/// <returns></returns>
		public ICrontabServiceJob CreateCrontabServiceJob<T>(string configurationSectionName = null, string invokeMethodName = "Run")
		{
			return new CrontabServiceJob<T>(
				new ConfigurationManagerBasedServiceJobSettings(
					FormatConfigurationSectionName<T>(configurationSectionName)),
				new CrontabProcessorFactory(), invokeMethodName);
		}

		/// <summary>
		/// Creates the service job.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="configuration">The configuration.</param>
		/// <param name="configurationSectionName">Name of the configuration section.</param>
		/// <param name="invokeMethodName">Name of the invoke method.</param>
		/// <returns></returns>
		public ICrontabServiceJob CreateCrontabServiceJob<T>(IConfiguration configuration, string configurationSectionName = null,
			string invokeMethodName = "Run")
		{
			return new CrontabServiceJob<T>(new ConfigurationBasedServiceJobSetting(configuration,
					FormatConfigurationSectionName<T>(configurationSectionName)),
				new CrontabProcessorFactory(), invokeMethodName);
		}

		private static string FormatConfigurationSectionName<T>(string configurationSectionName)
		{
			if (configurationSectionName != null)
				return configurationSectionName;

			var type = typeof(T);
			return type.Name + "Settings";
		}
	}
}